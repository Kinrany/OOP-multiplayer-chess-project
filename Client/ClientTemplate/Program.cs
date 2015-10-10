using System;

namespace ClientNamespace {
	class Program {
		static void Main(string[] args) {
			networking = new Networking();
			gui = new GUI(networking);
			gui.StartLoop();

			while (!gui.Esc);
		}

		static Networking networking;
		static GUI gui;
	}
}
