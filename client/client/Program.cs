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
            }
        }
    }

    static class Global
    {
        public static Client client = new Client();
        public static int next_form = 0;
    }
}
