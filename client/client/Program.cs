using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;

namespace client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IPHostEntry host = Dns.GetHostEntry("localhost"); // note(Stefan): Only for testing
            IPAddress ip = host.AddressList[1];

            IPEndPoint server_endpoint = new IPEndPoint(ip, 13000);

            Thread listener_thread = new Thread(new ThreadStart(listen_for_messages));

            try
            {
                Global.client.connect(server_endpoint);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }

            listener_thread.Start();

            while (true)
            {
                switch (Global.next_form)
                {
                    case 0:
                        Application.Run(new client_name_prompt());

                        break;

                    case 1:
                        Application.Run(new client_join_or_create_chat_room());

                        break;

                    case 2:
                        Application.Run(new client_chat_room_interface());

                        break;

                    case -1:
                        System.Environment.Exit(0);

                        return;
                }
            }
        }

        static void listen_for_messages()
        {
            while (true)
            {
                Global.client.receive_message();
            }
        }
    }

    static class Global
    {
        public static Client client = new Client(1024);
        public static int next_form = 0;
    }
}
