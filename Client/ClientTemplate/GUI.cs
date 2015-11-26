using System;
using System.Collections.Generic;
using System.Threading;

namespace ClientNamespace {
	class GUI {
		public GUI(UserData userData) {
			//this.userData. = userData.;
			this.userData = userData;
			Esc = false;

			userData.OnDeniedMessage += OnDeniedMessage;
			userData.OnUnknownMessage += OnUnknownMessage;
			userData.OnJoinedRoom += OnJoined;
			userData.OnConnected += OnConnect;
			

			// Таймер для регулярного вызова Loop
			timer = new Timer(new TimerCallback(Loop));
		}

		/// <summary>
		/// Запускает основной цикл.
		/// </summary>
		public void StartLoop() {
			timer.Change(0, Timeout.Infinite);
		}

		public bool Esc {
			get;
			private set;
		}

		private void Loop(object o) {
			// Выводит содержимое stringsToWrite в консоль
			while (stringsToWrite.Count > 0) {
				Console.WriteLine(stringsToWrite.Dequeue());
			}

			// Считывает и выполняет новую команду
			Console.Write(">>");
			ProcessCommand(Console.ReadLine());

			// Запускает отсчёт до следующего вызова Loop
			timer.Change(16, Timeout.Infinite);
		}

		private void ProcessCommand(string input) {
			string[] split = input.Split(' ');
			string command = split[0];

			switch (command) {
				case "":
					break;
				case "join":
					string roomType = "Trivial v1.1";
					if (split.Length >= 2) {
						roomType = split[1];
					}

					try {
						userData.JoinRoom(roomType);
					}
					catch (Exception e) {
						AddString(e.Message);
					}

					break;
				case "joined":
					AddString(userData.IsInRoom.ToString());
					break;
				case "connect":
					string playerName = userData.Name;

					try {
						userData.Connect(playerName);
					}
					catch (Exception e) {
						AddString(e.Message);
					}
					
					break;
				case "connected":
					AddString(userData.IsConnected.ToString());
					break;
				case "esc":
					Esc = true;
					break;
				case "name":
					if(userData.IsConnected)
					{
						AddString("You're already connected,you fool!");
						break;
					}
					else
					{
						if (split.Length >= 2) 
						{
							userData.Name = split[1];
                            AddString(userData.Name);
						}
						else
						{
							AddString("You came up short");
						}
					}
					break;
				case "help":
					AddString("Available commands: join, joined, connect, connected, help, esc.");
					break;
				default:
					AddString("Unknown command");
					break;
			}
		}

		private void OnDeniedMessage(string text) {
			AddString("Denied: " + text);
		}
		private void OnUnknownMessage(string type) {
			AddString("Unknown message of type \"" + type + "\"");
		}
		private void OnConnect() {
			AddString("Connected");
		}
		private void OnJoined() {
			AddString("Joined room");
		}

		private void AddString(string str) {
			stringsToWrite.Enqueue(str);
		}

		private Queue<string> stringsToWrite = new Queue<string>();
		private UserData userData;
		private Timer timer;
	}
}
