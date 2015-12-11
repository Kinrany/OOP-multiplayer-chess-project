using System;
//using System.Runtime.InteropServices;

namespace ClientNamespace {
	class Program
    {
        //here comes the horrible KOSTIL-PROGRAMMING
        /*const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        const int SW_Min = 2;
        const int SW_Max = 3;
        const int SW_Norm = 4;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);*/


        static void Main(string[] args)
        {
            //var handle = GetConsoleWindow();

            //скрыть консоль
            //ShowWindow(handle, SW_HIDE);

            networking = new Networking();
			userData = new UserData(networking);
			gui = new GUI(userData);
            gui.StartLoop();

            //Форма для интерфейса
            showtime = new GUIForm(gui, userData);

            showtime.Activate();
            showtime.ShowDialog();

            while (!gui.Esc);
		}

        static GUIForm showtime;
        static Networking networking;
		static GUI gui;
		static UserData userData;
	}
}
