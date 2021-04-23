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
    public partial class client_name_prompt : Form
    {
        public client_name_prompt()
        {
            InitializeComponent();
        }

        private void client_name_prompt_Load(object sender, EventArgs e)
        {

        }

        private void client_name_submit_Click(object sender, EventArgs e)
        {
            string name;

            if (this.client_name_text_box.Text == "")
            {
                MessageBox.Show("You have not inserted a name. Please insert a name in the text field");
            }
            else
            {
                name = this.client_name_text_box.Text;

                Global.client.client_name = name;

                this.Close();
            }
        }
    }
}
