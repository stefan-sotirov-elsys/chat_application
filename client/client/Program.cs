using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using message;

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
            Application.Run(new client_name_prompt());

            set_chat_room:
            if (Global.client.client_name != null)
            {
                Application.Run(new client_join_or_create_chat_room());
            }

            if (Global.client.room_code != null)
            {
                Application.Run(new client_chat_room_interface());
            }

            if (Global.client.room_code == null)
            {
                goto set_chat_room;
            }
        }
    }

    static class Global
    {
        public static Client client = new Client();
    }
}
