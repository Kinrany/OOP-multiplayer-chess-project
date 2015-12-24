using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientNamespace
{
    public partial class ChatRoom : Form
    {
        public int selected_player = 0;
        LogInForm logform;
        ChessForm chessform;

        public void setlogform(LogInForm loginform)
        {
            this.logform = loginform;
        }

        public void setchessform(ChessForm chess)
        {
            this.chessform = chess;
        }

        public ChatRoom()
        {
            InitializeComponent();
        }

        private void send_button_Click(object sender, EventArgs e)
        {
            Command.say(mesbox.Text);
            mesbox.Text = "Enter your message here....";
        }

        private void userbox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string opplogin = userbox.Items[userbox.SelectedIndex].ToString();
            Command.challendge(opplogin);
            this.chessform.Visible = true;
        }

        private void userbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected_player = userbox.SelectedIndex;
        }

        private void ChatRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            Command.disconnect();
            Application.Exit();
        }
    }
}
