using System;
using System.Collections.Generic;

namespace ClientNamespace 
{
	class UserData 
	{
		public UserData(Networking networking) 
		{
			this.networking = networking;
			
			this.networking.OnUnknownMessage += OnUnknownMessage;
			this.networking.OnDeniedMessage += OnDeniedMessage;
			this.networking.OnUserJoinedMessage += OnUserJoinedMessage;
			this.networking.OnUserLeftMessage += OnUserLeftMessage;
			this.networking.OnChallengedMessage += OnChallengedMessage;
			this.networking.OnChallengeRevokedMessage += OnChallengeRevokedMessage;
			this.networking.OnGameStartedMessage += OnGameStartedMessage;
			this.networking.OnGameEndedMessage += OnGameEndedMessage;

            this.networking.OnConnected += OnConnected;
            this.networking.OnJoinedRoom += OnJoinedRoom;
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
