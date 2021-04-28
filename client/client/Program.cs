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

            if (Global.next_form == 1)
            {
                Application.Run(new client_join_or_create_chat_room());
            }
        }
    }

    static class Global
    {
        public static Client client = new Client();
        public static int next_form = 0;
    }
}
