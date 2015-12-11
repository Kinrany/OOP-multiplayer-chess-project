using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientNamespace
{
    partial class GUIForm : Form
    {
        public GUIForm(GUI gui, UserData userData)
        {
            this.gui = gui;
            this.userData = userData;

            InitializeComponent();
        }

        UserData userData;
        GUI gui;

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            gui.ProcessCommand("esc");
        }

        private void Connection_Click(object sender, EventArgs e)
        {
            if (!userData.IsConnected)
            {
                if (NameBox.Text != String.Empty && NameBox.Text != "Unavailable name!")
                {
                    userData.Name = NameBox.Text;
                    gui.ProcessCommand("connect");
                }
                else
                {
                    NameBox.Text = "Unavailable name!";
                }
            }
            else if (!userData.IsInRoom)
                gui.ProcessCommand("join");

            if (userData.IsInRoom)
            {
                ConnectionLabel.Text = "Connected!";

                UsersBox.Visible = true;
                Chat.Visible = true;
                SayLabel.Visible = true;
                SayBox.Visible = true;

                ConnectionLabel.Visible = false;
                Connection.Visible = false;
                NameBox.Visible = false;

            }
            else if (userData.IsConnected)
            {
                ConnectionLabel.Text = "Enter room";
                Connection.Text = "Join room";
                NameBox.Text = "Matchmaking v1.2";
            }
            else
            {
                ConnectionLabel.Text = "Enter name";
            }
        }
    }
}
