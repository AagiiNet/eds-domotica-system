using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video;
using AForge.Imaging.Filters;
using AForge.Vision.Motion;

namespace Surveillance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PictureBox[] pb;
        Edge.IPCameras.IPCamera[] cams;
        MJPEGStream[] streams;
        MotionDetector[] detectors;

        // Filters
        ResizeNearestNeighbor filter = new ResizeNearestNeighbor(100, 100);

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO get cams from DB
            pb = new PictureBox[2];
            cams = new Edge.IPCameras.IPCamera[2];
            streams = new MJPEGStream[2];
            detectors = new MotionDetector[2];

            // TODO determine width/height based on number of cams
            int width = 480, height = 360;

            // Reset filters with determined width/height
            filter = new ResizeNearestNeighbor(width, height);
            
            // Add picturebox for each cam
            pb[0] = new PictureBox() { Width = width, Height = height, ImageLocation = "Images/testbeeld.png" };
            flowLayoutPanel1.Controls.Add(pb[0]);
            pb[1] = new PictureBox() { Width = width, Height = height, ImageLocation = "Images/testbeeld.png" };
            flowLayoutPanel1.Controls.Add(pb[1]);

            // Add cams to array
            cams[0] = new Edge.IPCameras.Foscam()
            {
                Description = "Woonkamer",
                Host = "10.30.59.81",
                Port = 8081,
                Username = "netduino",
                Password = "duinonet",
            };
            cams[1] = new Edge.IPCameras.Foscam()
            {
                Description = "Buiten",
                Host = "10.30.59.82",
                Port = 8082,
                Username = "netduino",
                Password = "duinonet",
            };

            // Setup motion detection
            for (int i = 0; i < cams.Count(); i++)
            {
                detectors[i] = new MotionDetector(
                                   new SimpleBackgroundModelingDetector(),
                                   new BlobCountingObjectsProcessing());
            }

            // Start streaming
            for (int i = 0; i < cams.Count(); i++)
            {
                streams[i] = new MJPEGStream(cams[i].MJPEGStreamURL);
                streams[i].NewFrame += new NewFrameEventHandler(Form1_NewFrame);
                streams[i].Start();
            }
        }

        void Form1_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Get destination picturebox using cam index
            Edge.IPCameras.IPCamera cam = cams.First(u => u.MJPEGStreamURL.Equals((sender as MJPEGStream).Source));
            PictureBox p = (PictureBox)flowLayoutPanel1.Controls[cam.Index];

            // Fit image to picturebox
            Bitmap image = eventArgs.Frame.Clone() as Bitmap;
            
            // Add cam name
            using (Graphics g = Graphics.FromImage(image))
            {
                g.DrawString(cam.Description, new Font("Arial", 10), new SolidBrush(Color.Yellow), new PointF(5, p.Height - 20));
            }

            // Render image
            float movement = detectors[cam.Index].ProcessFrame( image );
            p.Image = image;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (MJPEGStream m in streams)
                m.Stop();
        }
    }
}
