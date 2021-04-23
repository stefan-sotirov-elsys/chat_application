using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace client
{
    public partial class client_user_interface : Form
    {
        public client_user_interface()
        {
            InitializeComponent();
        }

        private void client_user_interface_Load(object sender, EventArgs e)
        {
            IPHostEntry host = Dns.GetHostEntry("localhost"); // note(Stefan): Only for testing
            IPAddress ip = host.AddressList[1];

            Global.client.remote_end_point = new IPEndPoint(ip, 13000);
            Global.client.socket = new Socket(Global.client.remote_end_point.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                Global.client.connect(Global.client.remote_end_point);
            }
            catch (Exception exception)
            {
                if (exception.Message == "connect")
                {
                    MessageBox.Show("Cannot connect to the server at this time");
                }

                this.Close();
            }
        }
    }
}
