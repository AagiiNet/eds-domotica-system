using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edge.IPCameras
{
    /// <summary>
    /// Foscam IP Camera
    /// </summary>
    public class Foscam : IPCamera
    {
        public override string SnapshotURL
        {
            get
            {
                return String.Format("{0}/snapshot.cgi?{1}", this.BaseURL, UsrPwdForURL);
            }
        }

        public override string MJPEGStreamURL
        {
            get
            {
                return String.Format("{0}/video.cgi?{1}", this.BaseURL, UsrPwdForURL);
            }
        }

        private string UsrPwdForURL { get { return String.Format("user={0}&pwd={1}", this.Username, this.Password); } }

    }

}
