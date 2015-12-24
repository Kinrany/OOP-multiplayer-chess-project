using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientNamespace {
	partial class ChatRoom : Form {
		public int selected_player = 0;

		private void showchess(string a) {
			//Program.ChessForm.Visible = true;
		}

		private void updateuserbox() {
			userbox.Items.Clear();
			for (int i = 0; i < userData.Players.Count; ++i) {
				userbox.Items.Add(userData.Players[i]);
			}
		}

		public ChatRoom(UserData userData) {
			this.userData = userData;
			InitializeComponent();
			//userData.OnChallengedMessage += showchess;
			userData.OnSayMessage += addmsg;
			userData.OnUserJoinedMessage += userjoin;
			userData.OnUserLeftMessage += userleft;
			userData.OnChallengedMessage += challengehandler;
			userData.OnChallengeRevokedMessage += challendgerevoked;
			userData.OnGameStartedMessage += UserData_OnGameStartedMessage;
		}

		private void challendgerevoked(string username) {
			chatbox.Text += "Player " + username + @" revoked his challenge.
";
		}

		private void UserData_OnGameStartedMessage() {
			//Program.ChessForm = new ChessForm(userData);
			Program.ChessForm.Show();
			//Program.ChessForm.Visible = true;
		}

		private void challengehandler(string username) {
			chatbox.Text += "Player " + username + @" challenged you.
";
		}

		private void userleft(string username) {
			updateuserbox();
			chatbox.Text += "Player " + username + @" disconnected.
";
		}

		private void userjoin(string username) {
			updateuserbox();
			chatbox.Text += "Player " + username + @" connected.
";
		}

		private void addmsg(string playername, string text) {
			chatbox.Text += "[" + playername + "]: " + text + @"
";
		}

		private void send_button_Click(object sender, EventArgs e) {
			userData.SayMessage(mesbox.Text);
			mesbox.Text = "Enter your message here....";
		}
		private void userbox_MouseDoubleClick(object sender, MouseEventArgs e) {
			string opplogin = userbox.Items[userbox.SelectedIndex].ToString();
			userData.ChallengePlayer(opplogin);
		}

		

		private void userbox_SelectedIndexChanged(object sender, EventArgs e) {
			selected_player = userbox.SelectedIndex;
		}
		private void ChatRoom_FormClosing(object sender, FormClosingEventArgs e) {
			Application.Exit();
		}

		private UserData userData;
	}
}
