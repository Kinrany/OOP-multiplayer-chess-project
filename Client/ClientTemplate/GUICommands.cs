using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace ClientNamespace 
{
	partial class GUI 
	{
		private Dictionary<string, GUICommandDelegate> commands = new Dictionary<string, GUICommandDelegate>();

		private delegate void GUICommandDelegate(string args);

		// Загружает команды в словарь commands
		private void LoadCommands() {
			commands.Add("", delegate(string args) { });
			commands.Add("join", join);
			commands.Add("joined", joined);
			commands.Add("connect", connect);
			commands.Add("connected", connected);
			commands.Add("esc", esc);
			commands.Add("name", change_name);
			commands.Add("say", say);
			commands.Add("challenge", challenge);
			commands.Add("help", help);
		}

		// Соединяет с комнатой
		void join(string args)
		{
			string roomType = "Matchmaking v1.2";
			if (args.Length > 0) 
			{
				roomType = args;
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
		
		// Показывает, присоединился ли клиент к комнате
		void joined(string args)
		{
			SafePrint(userData.IsInRoom);
		}
		
		// Соединяет клиент с YGN
		void connect(string args)
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
		
		// Показывает, присоединился ли клиент к основному серверу YGN
		void connected(string args)
		{
			SafePrint(userData.IsConnected);
		}
		
		// Закрывает игру
		void esc(string args)
		{
			Esc = true;
		}
		
		// Меняет имя игрока
		void change_name(string args)
		{
			if(userData.IsConnected)
			{
				SafePrint("You're already connected,you fool!");
			}
			else
			{
				if (args.Length > 0) 
				{
					userData.Name = args;
					SafePrint(userData.Name);
				}
				else
				{
					SafePrint("You came up short");
				}
			}
		}
		
		// Команда для посылки сообщений
		void say(string args)
		{
			if(!userData.IsInRoom)
			{
				SafePrint("Nobody hears you");
			}
			if (args.Length > 0) 
			{
				userData.SayMessage(args);
			}
		}
		
		// Предлагает сыграть другому игроку
		void challenge(string args)
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
				if (args.Length > 0) 
				{
					userData.Opponent = args;
				}
				else
				{
					SafePrint("Enter name next time");
				}
			}
		}
		
		// Выводит список доступных команд
		void help(string args) {
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