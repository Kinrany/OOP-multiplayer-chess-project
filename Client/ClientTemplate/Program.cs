using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ClientNamespace
{
    

    class Program
    {
        static LogInForm logform;
        static ChatRoom chatform;
        static ChessForm chessform;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            logform = new LogInForm();
            chatform = new ChatRoom();
            chatform.Visible = false;
            chessform = new ChessForm();
            chessform.Visible = false;

            logform.setchat(chatform);
            chatform.setchessform(chessform);
            chatform.setlogform(logform);
            chessform.setchat(chatform);

            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(logform);
        }
    }
}
