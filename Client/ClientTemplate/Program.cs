using System;

namespace ClientNamespace {
	class Program {
		static void Main(string[] args) {
			networking = new Networking();
			userData = new UserData(networking);
			gui = new GUI(userData);
			gui.StartLoop();

			while (!gui.Esc);
		}

		static Networking networking;
		static GUI gui;
		static UserData userData;
	}
}
