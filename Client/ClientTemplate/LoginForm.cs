using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientNamespace;

namespace ClientNamespace
{
    partial class LogInForm : Form
    {
		UserData userData;
        public LogInForm(UserData userData)
        {
			this.userData = userData;
			InitializeComponent();
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void connect_button_Click(object sender, EventArgs e)
        {
            try
            {
				userData.Name = loginbox.Text;
				userData.Connect();
            }
            catch
            {}
            if (userData.IsConnected)
            {
                try
                {
					userData.RoomType = roombox.Text;
					userData.JoinRoom();
                }
                catch
                {}
                if (userData.IsInRoom)
                {
                    this.Visible = false;
					Program.ChatForm.Visible = true;
                }
            }
        }

        private void LogInForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
