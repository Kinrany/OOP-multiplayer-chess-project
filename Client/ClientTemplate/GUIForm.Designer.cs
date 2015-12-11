namespace ClientNamespace
{
	partial class GUIForm
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
			this.Exit = new System.Windows.Forms.Button();
			this.ConnectionLabel = new System.Windows.Forms.Label();
			this.NameBox = new System.Windows.Forms.TextBox();
			this.Connection = new System.Windows.Forms.Button();
			this.UsersBox = new System.Windows.Forms.ListBox();
			this.Chat = new System.Windows.Forms.ListBox();
			this.SayLabel = new System.Windows.Forms.Label();
			this.SayBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// Exit
			// 
			this.Exit.Location = new System.Drawing.Point(630, 400);
			this.Exit.Name = "Exit";
			this.Exit.Size = new System.Drawing.Size(101, 37);
			this.Exit.TabIndex = 0;
			this.Exit.Text = "Exit";
			this.Exit.UseVisualStyleBackColor = true;
			this.Exit.Click += new System.EventHandler(this.Exit_Click);
			// 
			// ConnectionLabel
			// 
			this.ConnectionLabel.AutoSize = true;
			this.ConnectionLabel.Location = new System.Drawing.Point(223, 347);
			this.ConnectionLabel.Name = "ConnectionLabel";
			this.ConnectionLabel.Size = new System.Drawing.Size(81, 17);
			this.ConnectionLabel.TabIndex = 1;
			this.ConnectionLabel.Text = "Enter name";
			// 
			// NameBox
			// 
			this.NameBox.Location = new System.Drawing.Point(310, 344);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(143, 22);
			this.NameBox.TabIndex = 2;
			this.NameBox.Text = "Forgetable Frank";
			// 
			// Connection
			// 
			this.Connection.Location = new System.Drawing.Point(327, 372);
			this.Connection.Name = "Connection";
			this.Connection.Size = new System.Drawing.Size(103, 32);
			this.Connection.TabIndex = 3;
			this.Connection.Text = "Connect";
			this.Connection.UseVisualStyleBackColor = true;
			this.Connection.Click += new System.EventHandler(this.Connection_Click);
			// 
			// UsersBox
			// 
			this.UsersBox.FormattingEnabled = true;
			this.UsersBox.ItemHeight = 16;
			this.UsersBox.Location = new System.Drawing.Point(458, 13);
			this.UsersBox.Name = "UsersBox";
			this.UsersBox.Size = new System.Drawing.Size(272, 148);
			this.UsersBox.TabIndex = 4;
			this.UsersBox.Visible = false;
			// 
			// Chat
			// 
			this.Chat.FormattingEnabled = true;
			this.Chat.ItemHeight = 16;
			this.Chat.Location = new System.Drawing.Point(13, 13);
			this.Chat.Name = "Chat";
			this.Chat.Size = new System.Drawing.Size(425, 196);
			this.Chat.TabIndex = 5;
			this.Chat.Visible = false;
			// 
			// SayLabel
			// 
			this.SayLabel.AutoSize = true;
			this.SayLabel.Location = new System.Drawing.Point(13, 216);
			this.SayLabel.Name = "SayLabel";
			this.SayLabel.Size = new System.Drawing.Size(36, 17);
			this.SayLabel.TabIndex = 6;
			this.SayLabel.Text = "Say:";
			this.SayLabel.Visible = false;
			// 
			// SayBox
			// 
			this.SayBox.Location = new System.Drawing.Point(55, 213);
			this.SayBox.Name = "SayBox";
			this.SayBox.Size = new System.Drawing.Size(383, 22);
			this.SayBox.TabIndex = 7;
			this.SayBox.Visible = false;
			// 
			// GUIForm
			// 
			this.ClientSize = new System.Drawing.Size(751, 454);
			this.Controls.Add(this.SayBox);
			this.Controls.Add(this.SayLabel);
			this.Controls.Add(this.Chat);
			this.Controls.Add(this.UsersBox);
			this.Controls.Add(this.Connection);
			this.Controls.Add(this.NameBox);
			this.Controls.Add(this.ConnectionLabel);
			this.Controls.Add(this.Exit);
			this.Name = "GUIForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ExitButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.Button ConnectionButton;
		private System.Windows.Forms.ListBox USSSERSBox;
		private System.Windows.Forms.ListBox ChatBox;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button Exit;
		private System.Windows.Forms.Label ConnectionLabel;
		private System.Windows.Forms.TextBox NameBox;
		private System.Windows.Forms.Button Connection;
		private System.Windows.Forms.ListBox UsersBox;
		private System.Windows.Forms.ListBox Chat;
		private System.Windows.Forms.Label SayLabel;
		private System.Windows.Forms.TextBox SayBox;
	}
}