using System;
using System.Collections.Generic;
using System.Threading;

namespace ClientNamespace {
	partial class GUI {
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

		private void ProcessCommand(string input) 
		{
			string[] split = input.Split(' ');
			string command = split[0];

			switch (command) 
			{
				case "":
					break;
				case "join":
					join(split);
					break;
				case "joined":
					joined();
					break;
				case "connect":
					connect();
					break;
				case "connected":
					connected();
					break;
				case "esc":
					esc();
					break;
				case "name":
					change_name(split);
					break;
				case "challenge":
					challenge(split);
					break;
				case "help":
					help();
					break;
				default:
					AddString("Unknown command");
					break;
			}
		}

		private void OnDeniedMessage(string text) 
		{
			AddString("Denied: " + text);
		}
		private void OnUnknownMessage(string type) 
		{
			AddString("Unknown message of type \"" + type + "\"");
		}
		private void OnConnect() 
		{
			AddString("Connected");
		}
		private void OnJoined() 
		{
			AddString("Joined room");
		}
		private void OnChallengedMessage(string nemesis)
		{
			AddString("Challenged by " + nemesis);
		}

		private void AddString(object obj) 
		{
			stringsToWrite.Enqueue(obj.ToString());
		}

		private Queue<string> stringsToWrite = new Queue<string>();
		private UserData userData;
		private Timer timer;
	}
}
