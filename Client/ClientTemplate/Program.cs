using System;

namespace ClientNamespace {
	class Program {
		static void Main(string[] args) {
			networking = new Networking();
			gui = new GUI(networking);
			gui.StartLoop();

			while (!networking.IsInRoom);
			while (networking.IsInRoom);
		}

		static Networking networking;
		static GUI gui;
	}
}
