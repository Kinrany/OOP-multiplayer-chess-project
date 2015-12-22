using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace ClientNamespace {
	partial class GUI {
		private Dictionary<string, GUICommandDelegate> commands = new Dictionary<string, GUICommandDelegate>();

		private delegate void GUICommandDelegate(string args);

		// Загружает команды в словарь commands
		private void LoadCommands() {
			commands.Add("join", join);
			commands.Add("connect", connect);
			commands.Add("esc", esc);
			commands.Add("name", change_name);
			commands.Add("say", say);
			commands.Add("challenge", challenge);
			commands.Add("help", help);
			commands.Add("print", print);
			commands.Add("move", move);
			commands.Add("stop", stop);
			commands.Add("state", state);
			commands.Add("room", room);
			commands.Add("start", start);
		}


		// Соединяет с комнатой. Можно указать тип комнаты.
		void join(string args) {
			if (!userData.IsConnected) {
				SafePrint("You have to connect to the main server first.");
				return;
			}

			if (args != "") {
				userData.RoomType = args;
			}

			try {
				userData.JoinRoom();
			}
			catch (Exception e) {
				SafePrint(e);
			}
		}

		// Соединяет клиент с YGN
		void connect(string args) {
			try {
				userData.Connect();
			}
			catch (Exception e) {
				SafePrint(e);
			}
		}

		// Закрывает игру
		void esc(string args) {
			Esc = true;
		}

		// Меняет имя игрока
		void change_name(string args) {
			if (userData.IsConnected) {
				SafePrint("You're already connected.");
			}
			else {
				if (args != "") {
					userData.Name = args;
					SafePrint("You changed your name to " + userData.Name + ".");
				}
				else {
					SafePrint("Enter the name you want to use.");
				}
			}
		}

		// Команда для посылки сообщений
		void say(string args) {
			if (!userData.IsInRoom) {
				SafePrint("You have to be in a room to talk.");
			}

			if (args != "") {
				userData.SayMessage(args);
			}
			else {
				SafePrint("You haven't said anything.");
			}
		}

		// Предлагает сыграть другому игроку
		void challenge(string args) {
			if (!userData.IsConnected) {
				SafePrint("You're not connected.");
			}
			else if (!userData.IsInRoom) {
				SafePrint("You're not in a room.");
			}
			else {
				if (args == "") {
					userData.Opponent = args;
				}
				else {
					SafePrint("Enter player's name to challenge them.");
				}
			}
		}

		// Выводит список доступных команд
		void help(string args) {
			StringBuilder builder = new StringBuilder();
			builder.Append("Available commands: ");
			if (commands.Keys.Count == 0) {
				builder.Append("none o_o");
			}
			else {
				IEnumerator<string> keys = commands.Keys.GetEnumerator();
				keys.MoveNext();
				builder.Append(keys.Current);
				while (keys.MoveNext()) {
					builder.Append(", " + keys.Current);
				}
				builder.Append(".");
			}
			SafePrint(builder);
		}

		// Выводит доску
		void print(string args) {
			StringBuilder builder = new StringBuilder();
			for (int row = gameData.Board.Columns - 1; row >= 0; row--) {
				builder.Append(row + 1);
				builder.Append("  ");
				for (int col = 0; col < gameData.Board.Rows; col++) {
					builder.Append(gameData.Board.Array[col, row]);
					builder.Append(" ");
				}
				builder.Append("\n");
			}
			builder.Append("\n   A B C D E F G H\n");
			SafePrint(builder);
		}

		// Передвигает фигуру с одной клетки на другую
		void move(string args) {
			string[] positions = args.Split(new char[] { ' ', '\t', '-' }, StringSplitOptions.RemoveEmptyEntries);
			if (positions.Length != 2)
				SafePrint(@"Incorrect format. Try something like ""e2-e4"".");
			else {
				try {
					gameData.MoveFigure(positions[0], positions[1]);
					print(args);
				}
				catch (InvalidOperationException e) {
					SafePrint(e.Message);
				}
			}
		}

		// Останавливает игру
		void stop(string args) {
			userData.StopGame();
		}

		// Выводит состояние игры
		void state(string args) {
			SafePrint("Name: " + userData.Name);
			SafePrint("Room type: " + userData.RoomType);
			SafePrint("Server connection: " + (userData.IsConnected ? "yes" : "no"));
			SafePrint("Room connection: " + (userData.IsInRoom ? "yes" : "no"));
		}

		// Меняет тип комнаты
		void room(string args) {
			if (!userData.IsInRoom) {
				if (args.Length > 0) {
					userData.RoomType = args;
				}
				else {
					SafePrint("Enter the name of the room.");
				}
			}
			else {
				SafePrint("You have to leave the room first.");
			}
		}

		// Быстрый старт. Задаёт имя и присоединяется к серверу и комнате.
		void start(string args) {
			if (args.Length > 0) {
				userData.Name = args;
			}
			if (!userData.IsConnected) {
				userData.Connect();
			}
			if (!userData.IsInRoom) {
				userData.JoinRoom();
			}
		}
	}
}
