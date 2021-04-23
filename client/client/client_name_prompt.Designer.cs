
namespace client
{
    partial class client_name_prompt
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
            this.client_name_text_box = new System.Windows.Forms.TextBox();
            this.client_name_label = new System.Windows.Forms.Label();
            this.client_name_submit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // client_name_text_box
            // 
            this.client_name_text_box.AccessibleDescription = "";
            this.client_name_text_box.AccessibleName = "";
            this.client_name_text_box.Location = new System.Drawing.Point(32, 93);
            this.client_name_text_box.Name = "client_name_text_box";
            this.client_name_text_box.Size = new System.Drawing.Size(256, 20);
            this.client_name_text_box.TabIndex = 0;
            this.client_name_text_box.Tag = "";
            // 
            // client_name_label
            // 
            this.client_name_label.AutoSize = true;
            this.client_name_label.Location = new System.Drawing.Point(32, 74);
            this.client_name_label.Name = "client_name_label";
            this.client_name_label.Size = new System.Drawing.Size(117, 13);
            this.client_name_label.TabIndex = 1;
            this.client_name_label.Text = "Please input your name";
            // 
            // client_name_submit
            // 
            this.client_name_submit.Location = new System.Drawing.Point(305, 90);
            this.client_name_submit.Name = "client_name_submit";
            this.client_name_submit.Size = new System.Drawing.Size(75, 23);
            this.client_name_submit.TabIndex = 2;
            this.client_name_submit.Text = "submit";
            this.client_name_submit.UseVisualStyleBackColor = true;
            this.client_name_submit.Click += new System.EventHandler(this.client_name_submit_Click);
            // 
            // client_name_prompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 217);
            this.Controls.Add(this.client_name_submit);
            this.Controls.Add(this.client_name_label);
            this.Controls.Add(this.client_name_text_box);
            this.Name = "client_name_prompt";
            this.Text = "client_name_prompt";
            this.Load += new System.EventHandler(this.client_name_prompt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox client_name_text_box;
        private System.Windows.Forms.Label client_name_label;
        private System.Windows.Forms.Button client_name_submit;
    }
}

