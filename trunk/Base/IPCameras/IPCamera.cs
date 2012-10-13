using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Edge.IPCameras
{
    /// <summary>
    /// IP Camera vendors
    /// </summary>
    public enum Vendors // TODO add models with IPCAM SDK
    {
        [Description("Foscam")] 
        Foscam = 1,

        [Description("Dericam 8 Series")]
        Dericam = 2,

        [Description("EyeSight ES-IP Series")]
        EyeSight = 3
    }

    /// <summary>
    /// IP Camera interface
    /// </summary>
    public interface IIPCamera
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        String Description { get; set; }

        /// <summary>
        /// Gets or sets the vendor.
        /// </summary>
        /// <value>
        /// The vendor.
        /// </value>
        Vendors Vendor { get; set; }

        /// <summary>
        /// Gets or sets the hostname or ip address.
        /// </summary>
        /// <value>
        /// The hostname or ip address.
        /// </value>
        String Host { get; set; }
        
        /// <summary>
        /// Gets or sets the port number.
        /// </summary>
        /// <value>
        /// The port number.
        /// </value>
        Int32 Port { get; set; }

        /// <summary>
        /// Gets or sets wether to use SSL.
        /// </summary>
        /// <value>
        /// Indicates is SSL shoud be used.
        /// </value>
        Boolean UseSSL { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        String Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        String Password { get; set; }

        /// <summary>
        /// Gets the base URL.
        /// </summary>
        /// <value>
        /// The base URL, ie http://host:port/.
        /// </value>
        String BaseURL { get; }

        /// <summary>
        /// Gets the URL for a snapshot image.
        /// </summary>
        /// <value>
        /// The snapshot URL.
        /// </value>
        String SnapshotURL { get; }

        /// <summary>
        /// Gets the URL of a MJPEG stream.
        /// </summary>
        /// <value>
        /// The MJPEG stream URL.
        /// </value>
        String MJPEGStreamURL { get; }
    }

    /// <summary>
    /// IP Camera Base Class
    /// </summary>
    public abstract class IPCamera : IIPCamera
    {
        static int counter;
        int _index;

        public IPCamera()
        {
            this._index = counter++;
        }

        public int Index { get { return _index; } }

        // Properties

        private String _desc = "";

        /// <inheritDoc/>
        [Required]
        public string Description
        {
            get
            {
                return _desc;
            }
            set
            {
                _desc = value;
            }
        }

        private Vendors _vendor = Vendors.Foscam;

        /// <inheritDoc/>
        public Vendors Vendor
        {
            get
            {
                return _vendor;
            }
            set
            {
                _vendor = value;
            }
        }

        private string _host = "";

        /// <inheritDoc/>
        [Required]
        public string Host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
            }
        }

        private int _port = 80;

        /// <inheritDoc/>
        [Required]
        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        private bool _ssl = false;

        /// <inheritDoc/>
        public bool UseSSL
        {
            get
            {
                return _ssl;
            }
            set
            {
                _ssl = value;
            }
        }

        private string _username = "admin";

        /// <inheritDoc/>
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        private string _pwd = "";

        /// <inheritDoc/>
        [DataType(DataType.Password)]
        public string Password
        {
            get
            {
                return _pwd;
            }
            set
            {
                _pwd = value;
            }
        }

        /// <inheritDoc/>
        public string BaseURL
        {
            get 
            { 
                String url = this.Host + (this.Port == 80 ? "" : ":" + this.Port);
                return String.Format("{0}://{1}", (this.UseSSL ? "https" : "http"), url);
            }
        }

        /// <inheritDoc/>
        [DataType(DataType.Url)]
        public abstract string SnapshotURL
        {
            get;
        }

        /// <inheritDoc/>
        [DataType(DataType.Url)]
        public abstract string MJPEGStreamURL
        {
            get;
        }
    }
}
