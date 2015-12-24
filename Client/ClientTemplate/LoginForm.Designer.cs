namespace ClientNamespace
{
    partial class LogInForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.connect_button = new System.Windows.Forms.Button();
            this.exit_button = new System.Windows.Forms.Button();
            this.loginbox = new System.Windows.Forms.TextBox();
            this.roombox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // connect_button
            // 
            this.connect_button.Location = new System.Drawing.Point(12, 97);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(148, 32);
            this.connect_button.TabIndex = 0;
            this.connect_button.Text = "Connect";
            this.connect_button.UseVisualStyleBackColor = true;
            this.connect_button.Click += new System.EventHandler(this.connect_button_Click);
            // 
            // exit_button
            // 
            this.exit_button.Location = new System.Drawing.Point(166, 97);
            this.exit_button.Name = "exit_button";
            this.exit_button.Size = new System.Drawing.Size(106, 32);
            this.exit_button.TabIndex = 1;
            this.exit_button.Text = "Exit";
            this.exit_button.UseVisualStyleBackColor = true;
            this.exit_button.Click += new System.EventHandler(this.exit_button_Click);
            // 
            // loginbox
            // 
            this.loginbox.Location = new System.Drawing.Point(15, 12);
            this.loginbox.Name = "loginbox";
            this.loginbox.Size = new System.Drawing.Size(257, 20);
            this.loginbox.TabIndex = 3;
            this.loginbox.Text = "Your login...";
            // 
            // roombox
            // 
            this.roombox.Location = new System.Drawing.Point(15, 50);
            this.roombox.Name = "roombox";
            this.roombox.Size = new System.Drawing.Size(257, 20);
            this.roombox.TabIndex = 5;
            this.roombox.Text = "Room...";
            // 
            // LogInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 141);
            this.Controls.Add(this.roombox);
            this.Controls.Add(this.loginbox);
            this.Controls.Add(this.exit_button);
            this.Controls.Add(this.connect_button);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 180);
            this.MinimumSize = new System.Drawing.Size(300, 180);
            this.Name = "LogInForm";
            this.Text = "Autorization";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogInForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connect_button;
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.TextBox loginbox;
        private System.Windows.Forms.TextBox roombox;
    }
}

