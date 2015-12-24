using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClientNamespace
{
    class Command
    {
        // подключается к комнате иначе ексепшон
        static public void joinroom(string room) {
            if (!userData.IsConnected)
            {
                //SafePrint("You have to connect to the main server first.");
                return;
            }

            if (room != "")
            {
                userData.RoomType = room;
            }

            try
            {
                userData.JoinRoom();
            }
            catch (Exception e)
            {
                //SafePrint(e);
            }
        }
        // выходим из комнаты
        static public void exitroom() { }
        // возвращает тру если подключены к комнате, иначе фолс
        static public bool joined() { return true; }
        // подключается к яху
        static public void connect(string login) { }
        // отключаемся от яху, если подключены к комнате то тоже выходим
        static public void disconnect() { }
        // как joined только про сервер яху
        static public bool connected() { return true; }
        // не знаю на кой надо, но пусть будет
        static public void changelogin(string login) { }
        // чатимся
        static public void say(string s) { }
        // возвращает список юзеров в комнате
        static public List<string> getuserlist() { return new List<string>(); }
        // кидаем вызовы
        static public void challendge(string login) { }
    }
}
