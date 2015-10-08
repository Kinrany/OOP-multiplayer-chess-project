using System;
using System.Collections.Generic;
using System.Threading;

namespace ClientNamespace {
	class GUI {
		public GUI(Networking networking) {
			this.networking = networking;
			timer = new Timer(new TimerCallback(Loop));
			networking.OnDeniedMessage += OnDeniedMessage;
			networking.OnUnknownMessage += OnUnknownMessage;
		}

		public void StartLoop() {
			timer.Change(0, Timeout.Infinite);
		}

		private void Loop(object o) {
			while (stringsToWrite.Count > 0) {
				Console.WriteLine(stringsToWrite.Dequeue());
			}

			ProcessCommand(Console.ReadLine());

			timer.Change(16, Timeout.Infinite);
		}

		private void ProcessCommand(string command) {
			switch (command) {
				case "":
					break;
				case "connect":
					networking.Connect("Trivial v1.0");
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

		private void AddString(string str) {
			stringsToWrite.Enqueue(str);
		}

		private Queue<string> stringsToWrite = new Queue<string>();
		private Networking networking;
		private Timer timer;
	}
}
