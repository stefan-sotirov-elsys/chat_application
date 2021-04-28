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

        private void client_chat_room_interface_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            Thread thread = new Thread(new ThreadStart(handle_messages));

            thread.Start();
        }

        void handle_messages()
        {
            while (true)
            {
                byte[] buf = new byte[256];
                message.Message received_message;

                Global.client.socket.Receive(buf);

                received_message = message.Message.byte_array_to_message(buf);

                switch (received_message.type)
                {
                    case "error":
                        MessageBox.Show(received_message.content);

                        if (received_message.room_name == Global.client.room_code)
                        {
                            Global.client.room_code = null;
                        }



                        this.Close();

                        break;

                    case "content":
                        this.text_screen.Text += received_message.content + Environment.NewLine;

                        break;
                }
            }
        }

        private void submit_content_Click(object sender, EventArgs e)
        {
            string content = Global.client.client_name + ": " + this.content_input.Text;

            message.Message new_message = new message.Message("content", content, Global.client.room_code);

            this.content_input.Clear();

            Global.client.socket.Send(message.Message.message_to_byte_array(new_message));
        }

        private void change_room_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
