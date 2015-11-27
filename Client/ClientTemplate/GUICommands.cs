using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace ClientNamespace 
{
	partial class GUI 
	{
		private Dictionary<string, GUICommandDelegate> commands = new Dictionary<string, GUICommandDelegate>();

		private delegate void GUICommandDelegate(string[] split);

		private void LoadCommands() {
			commands.Add("join", join);
			commands.Add("joined", joined);
			commands.Add("connect", connect);
			commands.Add("connected", connected);
			commands.Add("esc", esc);
			commands.Add("name", change_name);
			commands.Add("challenge", challenge);
			commands.Add("help", help);
		}

		void join(string[] split)
		{
			string roomType = "Trivial v1.1";
			if (split.Length >= 2) 
			{
				roomType = split[1];
			}

			try 
			{
				userData.JoinRoom(roomType);
			}
			catch (Exception e) 
			{
				SafePrint(e);
			}
		}
		
		void joined(string[] split)
		{
			SafePrint(userData.IsInRoom);
		}
		
		void connect(string[] split)
		{
			string playerName = userData.Name;

			try 
			{
				userData.Connect(playerName);
			}
			catch (Exception e) 
			{
				SafePrint(e);
			}
		}
		
		void connected(string[] split)
		{
			SafePrint(userData.IsConnected);
		}
		
		void esc(string[] split)
		{
			Esc = true;
		}
		
		void change_name(string[] split)
		{
			if(userData.IsConnected)
			{
				SafePrint("You're already connected,you fool!");
			}
			else
			{
				if (split.Length >= 2) 
				{
					userData.Name = split[1];
                    SafePrint(userData.Name);
				}
				else
				{
					SafePrint("You came up short");
				}
			}
		}
		
		void challenge(string[] split)
		{
			if(!userData.IsConnected)
			{
				SafePrint("You're not connected,you fool!");
			}
			else if(!userData.IsInRoom)
			{
				SafePrint("You're not in the room,you fool!");
			}
			else
			{
				if (split.Length >= 2) 
				{
					userData.Opponent = split[1];
				}
				else
				{
					SafePrint("Enter name next time");
				}
			}
		}
		
		void help(string[] split) {
			string text = "Available commands: ";
			if (commands.Keys.Count == 0) {
				text += "none o_o";
			}
			else {
				IEnumerator<string> keys = commands.Keys.GetEnumerator();
				keys.MoveNext();
				text += keys.Current;
				while (keys.MoveNext()) {
					text += ", " + keys.Current;
				}
				text += ".";
			}
			SafePrint(text);
		}
	}
}