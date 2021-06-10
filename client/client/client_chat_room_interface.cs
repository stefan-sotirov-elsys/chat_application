using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;

namespace client
{
    public partial class client_chat_room_interface : Form
    {
        public client_chat_room_interface()
        {
            InitializeComponent();
        }

        Thread accepter_thread;

        private void client_chat_room_interface_Load(object sender, EventArgs e)
        {
            Global.next_form = -1;

            Control.CheckForIllegalCrossThreadCalls = false;

            accepter_thread = new Thread(new ParameterizedThreadStart(accept_messages));

            accepter_thread.Start(0);
        }

        void accept_messages(object thread_sleep_time)
        {
            while (true)
            {
                message.Message accepted_message = Global.client.accept_message((int)thread_sleep_time);

                if (accepted_message.type == "content")
                {
                    int i;

                    for (i = 0; i < accepted_message.content.Length; i++)
                    {
                        if (accepted_message.content[i] == '\n')
                        {
                            this.text_screen.Text += Environment.NewLine;
                        }
                        else
                        {
                            this.text_screen.Text += accepted_message.content[i];
                        }
                    }
                }
            }
        }

        private void submit_content_Click(object sender, EventArgs e)
        {
            message.Message new_message = new message.Message("content", Global.client.client_name + ": " + this.content_input.Text, Global.client.room_code);

            Global.client.send_message(new_message);

            this.content_input.Clear();
        }

        private void change_room_Click(object sender, EventArgs e)
        {
            Global.next_form = 1;

            this.Close();
        }

        private void client_chat_room_interface_FormClosing(object sender, FormClosingEventArgs e)
        {
            accepter_thread.Abort();
        }
    }
}
