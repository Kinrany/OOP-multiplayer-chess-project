using System;
using System.Collections.Generic;

namespace ClientNamespace 
{
	class UserData 
	{
		public UserData(Networking networking) 
		{
			this.networking = networking;
			
			this.networking.OnUnknownMessage += delegate(string s) { OnUnknownMessage(s); };
			this.networking.OnDeniedMessage += delegate(string s) { OnDeniedMessage(s); };
			this.networking.OnUserJoinedMessage += delegate(string s) { OnUserJoinedMessage(s); };
			this.networking.OnUserLeftMessage += delegate(string s) { OnUserLeftMessage(s); };
			this.networking.OnChallengedMessage += delegate(string s) { OnChallengedMessage(s); };
			this.networking.OnChallengeRevokedMessage += delegate(string s) { OnChallengeRevokedMessage(s); };
			this.networking.OnGameStartedMessage += delegate() { OnGameStartedMessage(); };
			this.networking.OnGameEndedMessage += delegate() { OnGameEndedMessage(); };

            this.networking.OnConnected += delegate() { OnConnected(); };
            this.networking.OnJoinedRoom += delegate() { OnJoinedRoom(); };
		}
		
		public string Name = "Forgettable Frank";
		
		public bool IsConnected
		{
			get 
			{
				return networking.IsConnected;
			}
		}
		public bool IsInRoom
		{
			get 
			{
				return networking.IsInRoom;
			}
		}
		public string Opponent
		{
			get 
			{
				return opponent;
			}
			set
			{
				opponent = value;
				ChallengePlayer(opponent);
			}
		}
		public List<string> ChallengeThrow
		{
			get 
			{
				return challenges;
			}
		}
		
        public void Connect(string playerName) 
        {
            networking.Connect(playerName);
		}
        public void JoinRoom(string roomType)
        {
            networking.JoinRoom(roomType);
        }
		public void ChallengePlayer(string name)
		{
			networking.ChallengePlayer(name);
		}

		public bool IsPlaying = false;
		
		// "= delegate { }" спасает от исключения, если нет подписчиков
		public event Networking.UnknownMessageDelegate OnUnknownMessage = delegate { };
        public event Networking.DeniedMessageDelegate OnDeniedMessage = delegate { };
        public event Networking.UserJoinedMessageDelegate OnUserJoinedMessage = delegate { };
        public event Networking.UserLeftMessageDelegate OnUserLeftMessage = delegate { };
        public event Networking.ChallengedMessageDelegate OnChallengedMessage = delegate { };
        public event Networking.ChallengeRevokedMessageDelegate OnChallengeRevokedMessage = delegate { };
        public event Networking.GameStartedMessageDelegate OnGameStartedMessage = delegate { };
        public event Networking.GameEndedMessageDelegate OnGameEndedMessage = delegate { };

        public event Networking.ConnectedDelegate OnConnected = delegate { };
        public event Networking.JoinedRoomDelegate OnJoinedRoom = delegate { };
		
		Networking networking;
		private string opponent = null;
		private List<string> challenges = new List<string>();
	}
}
