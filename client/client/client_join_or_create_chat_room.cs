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
using message;

namespace client
{
    public partial class client_join_or_create_chat_room : Form
    {
        public client_join_or_create_chat_room()
        {
            InitializeComponent();
        }

        private void client_join_or_create_chat_room_Load(object sender, EventArgs e)
        {
            Global.next_form = -1;
        }

        string mode;

        private void create_room_button_Click(object sender, EventArgs e)
        {
            mode = "create_room";

            this.create_room_button.BackColor = Color.AntiqueWhite;
            this.join_room_button.BackColor = default(Color);

            this.join_room_button.UseVisualStyleBackColor = true;
        }

        private void join_room_button_Click(object sender, EventArgs e)
        {
            mode = "join_room";

            this.join_room_button.BackColor = Color.AntiqueWhite;
            this.create_room_button.BackColor = default(Color);

            this.create_room_button.UseVisualStyleBackColor = true;
        }

        private void submit_button_Click(object sender, EventArgs e)
        {
            if (this.create_or_join_room_text_box.Text != "")
            {
                if (mode != null)
                {
                    message.Message new_message = new message.Message(mode, Global.client.client_name, this.create_or_join_room_text_box.Text);
                    message.Message received_message;

                    Global.client.send_message(new_message);

                    received_message = Global.client.accept_message(0);

                    if (check_for_errors(received_message))
                    {
                        return;
                    }

                    if (mode == "create_room")
                    {
                        new_message.type = "join_room";
                        Global.client.send_message(new_message);

                        received_message = Global.client.accept_message(0);

                        if (check_for_errors(received_message))
                        {
                            return;
                        }
                    }

                    Global.client.room_code = this.create_or_join_room_text_box.Text;

                    Global.next_form = 2;

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please choose an action");
                }
            }
            else
            {
                MessageBox.Show("Please insert a room code");
            }
        }

        bool check_for_errors(message.Message received_message)
        {
            if (received_message.type == "error")
            {
                MessageBox.Show(received_message.content);

                return true;
            }

            return false;
        }

        private void change_user_button_Click(object sender, EventArgs e)
        {
            Global.next_form = 0;

            this.Close();
        }
    }
}