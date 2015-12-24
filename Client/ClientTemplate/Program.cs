using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ClientNamespace
{
    

    class Program
    {
        public static LogInForm LogForm;
        public static ChatRoom ChatForm;
        public static ChessForm ChessForm;
        
		/// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
			networking = new Networking();
			userData = new UserData(networking);
			gameData = new GameData(networking);

            LogForm = new LogInForm(userData);
            ChatForm = new ChatRoom(userData);
			ChessForm = new ChessForm(userData, gameData);
			//ChessForm.Hide();
			ChessForm.Visible = true;
			ChatForm.Visible = false;
            

            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(LogForm);
        }

		private static Networking networking;
		private static UserData userData;
		private static GameData gameData;
    }
}
