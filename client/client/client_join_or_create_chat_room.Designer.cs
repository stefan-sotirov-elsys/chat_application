
namespace client
{
    partial class client_join_or_create_chat_room
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
            this.create_room_button = new System.Windows.Forms.Button();
            this.join_room_button = new System.Windows.Forms.Button();
            this.create_or_join_room_text_box = new System.Windows.Forms.TextBox();
            this.submit_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // create_room_button
            // 
            this.create_room_button.Location = new System.Drawing.Point(12, 63);
            this.create_room_button.Name = "create_room_button";
            this.create_room_button.Size = new System.Drawing.Size(124, 32);
            this.create_room_button.TabIndex = 0;
            this.create_room_button.Text = "create room";
            this.create_room_button.UseVisualStyleBackColor = true;
            this.create_room_button.Click += new System.EventHandler(this.create_room_button_Click);
            // 
            // join_room_button
            // 
            this.join_room_button.Location = new System.Drawing.Point(249, 63);
            this.join_room_button.Name = "join_room_button";
            this.join_room_button.Size = new System.Drawing.Size(124, 32);
            this.join_room_button.TabIndex = 1;
            this.join_room_button.Text = "join room";
            this.join_room_button.UseVisualStyleBackColor = true;
            this.join_room_button.Click += new System.EventHandler(this.join_room_button_Click);
            // 
            // create_or_join_room_text_box
            // 
            this.create_or_join_room_text_box.Location = new System.Drawing.Point(12, 155);
            this.create_or_join_room_text_box.Name = "create_or_join_room_text_box";
            this.create_or_join_room_text_box.Size = new System.Drawing.Size(351, 20);
            this.create_or_join_room_text_box.TabIndex = 2;
            // 
            // submit_button
            // 
            this.submit_button.Location = new System.Drawing.Point(386, 151);
            this.submit_button.Name = "submit_button";
            this.submit_button.Size = new System.Drawing.Size(75, 23);
            this.submit_button.TabIndex = 3;
            this.submit_button.Text = "submit";
            this.submit_button.UseVisualStyleBackColor = true;
            this.submit_button.Click += new System.EventHandler(this.submit_button_Click);
            // 
            // client_join_or_create_chat_room
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 217);
            this.Controls.Add(this.submit_button);
            this.Controls.Add(this.create_or_join_room_text_box);
            this.Controls.Add(this.join_room_button);
            this.Controls.Add(this.create_room_button);
            this.Name = "client_join_or_create_chat_room";
            this.Text = "client_join_or_create_chat_room";
            this.Load += new System.EventHandler(this.client_join_or_create_chat_room_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button create_room_button;
        private System.Windows.Forms.Button join_room_button;
        private System.Windows.Forms.TextBox create_or_join_room_text_box;
        private System.Windows.Forms.Button submit_button;
    }
}