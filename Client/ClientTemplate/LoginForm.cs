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
    public partial class LogInForm : Form
    {
        ChatRoom chatform;
        public LogInForm()
        {
            InitializeComponent();
        }

        public void setchat(ChatRoom chat)
        {
            this.chatform = chat;
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Command.disconnect();
            Application.Exit();
        }

        private void connect_button_Click(object sender, EventArgs e)
        {
            try
            {
                Command.connect(loginbox.Text);
            }
            catch
            {}
            if (Command.connected())
            {
                try
                {
                    Command.joinroom(roombox.Text);
                }
                catch
                {}
                if (Command.joined())
                {
                    this.Visible = false;
                    this.chatform.Visible = true;
                }
            }
        }

        private void LogInForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Command.disconnect();
            Application.Exit();
        }
    }
}
