using System;
using System.Collections.Generic;
using System.Threading;

namespace ClientNamespace 
{
	partial class GUI 
	{
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
		
		void joined()
		{
			SafePrint(userData.IsInRoom);
		}
		
		void connect()
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
		
		void connected()
		{
			SafePrint(userData.IsConnected);
		}
		
		void esc()
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
		
		void help()
		{
			SafePrint("Available commands: join, joined, connect, connected, name, challenge, help, esc.");
		}
	}
}