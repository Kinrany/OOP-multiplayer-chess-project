using System;

namespace ClientNamespace {
	class Program {
		static void Main(string[] args) {
			networking = new Networking();
			userData = new UserData(networking);
			gameData = new GameData(networking);
			gui = new GUI(userData,gameData);
			gui.StartLoop();

			while (!gui.Esc);
		}

		static Networking networking;
		static GUI gui;
		static UserData userData;
		static GameData gameData;
	}
}
