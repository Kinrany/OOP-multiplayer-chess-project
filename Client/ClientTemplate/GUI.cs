using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;

namespace ClientNamespace {
	partial class GUI {
		public GUI(UserData userData,GameData gameData)
		{
			this.userData = userData;
			this.gameData = gameData;
			Esc = false;

			userData.OnChallengedMessage += OnChallengedMessage;
			userData.OnConnected += OnConnect;
			userData.OnDeniedMessage += OnDeniedMessage;
			userData.OnJoinedRoom += OnJoined;
			userData.OnSayMessage += OnSayMessage;
			userData.OnUserJoinedMessage += OnUserJoinedMessage;
			userData.OnUserLeftMessage += OnUserLeftMessage;
			userData.OnUnknownMessage += OnUnknownMessage;

			userData.OnChallengeRevokedMessage += OnUnhandledMessage;
			userData.OnGameEndedMessage += OnUnhandledMessage;
			userData.OnGameStartedMessage += OnUnhandledMessage;
			gameData.GUIMove += GUIMove;
			LoadCommands();

			// Таймер для регулярного вызова Loop
			timer = new Timer(new TimerCallback(Loop));
		}

		/// <summary>
		/// Запускает основной цикл.
		/// </summary>
		public void StartLoop() {
			timer.Change(0, Timeout.Infinite);
		}

		/// <summary>
		/// Потокобезопасный вывод в консоль.
		/// </summary>
		public static void SafePrint(object obj) {
			stringsToWrite.Enqueue(obj.ToString());
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
			if (input == "") {
				return;
			}

			Match m = Regex.Match(input, @"^(\S+)\s*(\S.*)?$");
			string command = m.Groups[1].ToString();
			string args = m.Groups[2].ToString();

			if (commands.ContainsKey(command)) {
				commands[command](args);
			}
			else {
				SafePrint("Unknown command.");
			}
		}

		private void OnDeniedMessage(string text) 
		{
			SafePrint("Denied: " + text);
		}
		private void OnUnknownMessage(string type) 
		{
			SafePrint("Unknown message of type \"" + type + "\"");
		}
		private void OnConnect() 
		{
			SafePrint("Connected");
		}
		private void OnJoined() 
		{
			SafePrint("Joined room");
		}
		private void OnChallengedMessage(string nemesis)
		{
			SafePrint("Challenged by " + nemesis);
		}
		private void OnSayMessage(string sender, string message)
		{
			if (userData.Name == sender) {
				SafePrint("You say: \"" + message + "\"");
			}
			else {
				SafePrint(sender + " says: \"" + message + "\"");
			}
		}
		private void OnUserJoinedMessage(string player)
		{
			if (userData.Name == player) {
				SafePrint("You joined the room");
			}
			else { 
				SafePrint(player + " has joined the room");
			}
		}
		private void GUIMove(string player, string f, string t)
		{
			print(player);
			SafePrint(player + " has made a move" + f + "-" + t);
		}
		private void OnUserLeftMessage(string player)
		{
			SafePrint(player + " has left the room");
		}
		private void OnUnhandledMessage()
		{
			SafePrint("Unhandled message");
		}
		private void OnUnhandledMessage(string str)
		{
			SafePrint("Unhandled message: " + str);
		}

		private static Queue<string> stringsToWrite = new Queue<string>();

		private UserData userData;
		private GameData gameData;
		private Timer timer;
	}
}
