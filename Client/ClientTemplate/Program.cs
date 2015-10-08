using System;

namespace ClientNamespace {
	class Program {
		static void Main(string[] args) {
			networking = new Networking("kinrany");
			gui = new GUI(networking);
			gui.StartLoop();

			while (!networking.Connected);
			while (networking.Connected);
		}

		static Networking networking;
		static GUI gui;
	}
}
