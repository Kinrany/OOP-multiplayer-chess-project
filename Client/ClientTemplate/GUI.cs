using System;
using System.Collections.Generic;
using System.Threading;

namespace ClientNamespace {
	class GUI {
		public GUI(Networking networking) {
			this.networking = networking;

			networking.OnDeniedMessage += OnDeniedMessage;
			networking.OnUnknownMessage += OnUnknownMessage;
			networking.OnJoinedRoom += OnJoined;
			networking.OnConnected += OnConnect;

			// Таймер для регулярного вызова Loop
			timer = new Timer(new TimerCallback(Loop));
		}

		/// <summary>
		/// Запускает основной цикл.
		/// </summary>
		public void StartLoop() {
			timer.Change(0, Timeout.Infinite);
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

		private void ProcessCommand(string command) {
			string[] split = command.Split(' ');
			command = split[0];

			switch (command) {
				case "":
					break;
				case "join":
					string roomType = "Trivial v1.1";
					if (split.Length >= 2) {
						roomType = split[1];
					}
					networking.JoinRoom(roomType);
					break;
				case "joined":
					AddString(networking.IsInRoom.ToString());
					break;
				case "connect":
					string playerName = "John Doe";
					if (split.Length >= 2) {
						playerName = split[1];
					}
					networking.Connect(playerName);
					break;
				case "connected":
					AddString(networking.IsConnected.ToString());
					break;
				case "challenge":
					networking.ChallengePlayer(split[1]);
					AddString("Challenged player " + split[1]);
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
		private Networking networking;
		private Timer timer;
	}
}
