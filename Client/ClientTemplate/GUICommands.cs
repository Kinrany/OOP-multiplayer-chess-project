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
			commands.Add("join", join);
			commands.Add("joined", joined);
			commands.Add("connect", connect);
			commands.Add("connected", connected);
			commands.Add("esc", esc);
			commands.Add("name", change_name);
			commands.Add("say", say);
			commands.Add("challenge", challenge);
			commands.Add("help", help);
			commands.Add("print", print);
			commands.Add("move", move);
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

		void print(string args)
		{
			string res_board = "";
			for (int col = 0; col < gameData.Board.Columns; col++)
			{
				res_board += (8 - col) + "  ";
				for (int row = 0; row < gameData.Board.Rows; row++)
					res_board += gameData.Board.Array[row, col] + " ";
				res_board += "\n";
			}
			res_board += "\n   A B C D E F G H\n";
			SafePrint(res_board);
		}

		void move(string args)
		{
			string[] positions = args.Split(new char[] { ' ', '\t', '-' }, StringSplitOptions.RemoveEmptyEntries);
			if (positions.Length != 2)
				SafePrint("U WOT M8??!1!!");
			else
			{
				ChessFigurePosition tmp1 = new ChessFigurePosition(positions[0]);
				ChessFigurePosition tmp2 = new ChessFigurePosition(positions[1]);
				if (gameData.Board[tmp1] == ChessFigure._)
				{
					SafePrint("Cant move the non-existent figure");
					return;
				}
				if (positions[0] == positions[1])
				{
					SafePrint("You cant move yourself");
					return;
				}
				
				gameData.MoveFigure(tmp1,tmp2, positions[0], positions[1]);
				print(args);
			}
		}
	}
}
