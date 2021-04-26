
namespace client
{
    partial class client_chat_room_interface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.text_screen = new System.Windows.Forms.TextBox();
            this.content_input = new System.Windows.Forms.TextBox();
            this.submit_content = new System.Windows.Forms.Button();
            this.change_room = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // text_screen
            // 
            this.text_screen.Location = new System.Drawing.Point(30, 48);
            this.text_screen.Multiline = true;
            this.text_screen.Name = "text_screen";
            this.text_screen.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_screen.Size = new System.Drawing.Size(490, 203);
            this.text_screen.TabIndex = 0;
            // 
            // content_input
            // 
            this.content_input.Location = new System.Drawing.Point(30, 336);
            this.content_input.Name = "content_input";
            this.content_input.Size = new System.Drawing.Size(490, 20);
            this.content_input.TabIndex = 1;
            // 
            // submit_content
            // 
            this.submit_content.Location = new System.Drawing.Point(551, 332);
            this.submit_content.Name = "submit_content";
            this.submit_content.Size = new System.Drawing.Size(64, 32);
            this.submit_content.TabIndex = 2;
            this.submit_content.Text = "submit";
            this.submit_content.UseVisualStyleBackColor = true;
            this.submit_content.Click += new System.EventHandler(this.submit_content_Click);
            // 
            // change_room
            // 
            this.change_room.Location = new System.Drawing.Point(551, 48);
            this.change_room.Name = "change_room";
            this.change_room.Size = new System.Drawing.Size(124, 32);
            this.change_room.TabIndex = 3;
            this.change_room.Text = "change chat room";
            this.change_room.UseVisualStyleBackColor = true;
            this.change_room.Click += new System.EventHandler(this.change_room_Click);
            // 
            // client_chat_room_interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.change_room);
            this.Controls.Add(this.submit_content);
            this.Controls.Add(this.content_input);
            this.Controls.Add(this.text_screen);
            this.Name = "client_chat_room_interface";
            this.Text = "client_chat_room_interface";
            this.Load += new System.EventHandler(this.client_chat_room_interface_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_screen;
        private System.Windows.Forms.TextBox content_input;
        private System.Windows.Forms.Button submit_content;
        private System.Windows.Forms.Button change_room;
    }
}