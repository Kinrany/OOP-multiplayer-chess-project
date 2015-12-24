namespace ClientNamespace
{
    partial class ChatRoom
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
            this.send_button = new System.Windows.Forms.Button();
            this.userbox = new System.Windows.Forms.ListBox();
            this.mesbox = new System.Windows.Forms.TextBox();
            this.chatbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // send_button
            // 
            this.send_button.Location = new System.Drawing.Point(373, 292);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(75, 23);
            this.send_button.TabIndex = 0;
            this.send_button.Text = "Send";
            this.send_button.UseVisualStyleBackColor = true;
            this.send_button.Click += new System.EventHandler(this.send_button_Click);
            // 
            // userbox
            // 
            this.userbox.FormattingEnabled = true;
            this.userbox.Items.AddRange(new object[] {
            "username1",
            "username2",
            "username3",
            "username4"});
            this.userbox.Location = new System.Drawing.Point(454, 12);
            this.userbox.Name = "userbox";
            this.userbox.Size = new System.Drawing.Size(120, 303);
            this.userbox.TabIndex = 1;
            this.userbox.SelectedIndexChanged += new System.EventHandler(this.userbox_SelectedIndexChanged);
            this.userbox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.userbox_MouseDoubleClick);
            // 
            // mesbox
            // 
            this.mesbox.Location = new System.Drawing.Point(12, 292);
            this.mesbox.Name = "mesbox";
            this.mesbox.Size = new System.Drawing.Size(355, 20);
            this.mesbox.TabIndex = 2;
            this.mesbox.Text = "Enter your message here...";
            // 
            // chatbox
            // 
            this.chatbox.Enabled = false;
            this.chatbox.Location = new System.Drawing.Point(12, 12);
            this.chatbox.Multiline = true;
            this.chatbox.Name = "chatbox";
            this.chatbox.Size = new System.Drawing.Size(436, 274);
            this.chatbox.TabIndex = 3;
            // 
            // ChatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 329);
            this.Controls.Add(this.chatbox);
            this.Controls.Add(this.mesbox);
            this.Controls.Add(this.userbox);
            this.Controls.Add(this.send_button);
            this.Name = "ChatRoom";
            this.Text = "ChatRoom";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatRoom_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button send_button;
        private System.Windows.Forms.ListBox userbox;
        private System.Windows.Forms.TextBox mesbox;
        private System.Windows.Forms.TextBox chatbox;
    }
}