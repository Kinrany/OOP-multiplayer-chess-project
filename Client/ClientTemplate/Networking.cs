using System;
using PlayerIOClient;

namespace ClientNamespace {
	partial class Networking {
		public Networking() {
			client = null;
			connection = null;
		}

		/// <summary>
		/// Установлено ли соединение с сервером.
		/// </summary>
		public bool IsConnected {
			get {
				return (client != null);
			}
		}
		/// <summary>
		/// Заходит на сервер под заданным именем.
		/// </summary>
		/// <param name="playerName">Имя игрока.</param>
		public void Connect(string playerName) {
			client = PlayerIO.Connect(
				"test-game-1-sebwxyhi2k6yfeymgnxfa",
				"public",
				playerName,
				null, null);

			OnConnected();
		}

		/// <summary>
		/// Установлено ли соединение с комнатой.
		/// </summary>
		public bool IsInRoom {
			get {
				return (connection != null && connection.Connected);
			}
		}
		/// <summary>
		/// Соединяет с комнатой заданного типа.
		/// </summary>
		/// <param name="roomType">Тип комнаты.</param>
		public void JoinRoom(string roomType) {
			if (!IsConnected) {
				throw new InvalidOperationException("Нет соединения с сервером");
			}

			//client.Multiplayer.DevelopmentServer = new ServerEndpoint("127.0.0.1", 8184);

			connection = client.Multiplayer.CreateJoinRoom(
				"my-room-id",
				roomType,
				true,
				null, null);

			connection.OnMessage += OnMessage;

			OnJoinedRoom();
		}


		// Методы для отправки сообщений
		public void ChallengePlayer(string name) {
			connection.Send("Challenge player", name);
		}
		public void Say(string text) {
			connection.Send("Say", text);
		}
		public void CreateFigure(string position, string figure) {
			connection.Send("Create figure", position, figure);
		}
		public void MoveFigure(string position1, string position2) {
			connection.Send("Move figure", position1, position2);
		}
		public void DeleteFigure(string position) {
			connection.Send("Delete figure", position);
		}

		// События для обработки входящих сообщений
		public event UnknownMessageDelegate OnUnknownMessage = delegate { };
		public event DeniedMessageDelegate OnDeniedMessage = delegate { };
		public event UserJoinedMessageDelegate OnUserJoinedMessage = delegate { };
		public event UserLeftMessageDelegate OnUserLeftMessage = delegate { };
		public event ChallengedMessageDelegate OnChallengedMessage = delegate { };
		public event ChallengeRevokedMessageDelegate OnChallengeRevokedMessage = delegate { };
		public event GameStartedMessageDelegate OnGameStartedMessage = delegate { };
		public event GameEndedMessageDelegate OnGameEndedMessage = delegate { };
		public event CreateFigureMessageDelegate OnCreateFigureMessage = delegate { };
		public event MoveFigureMessageDelegate OnMoveFigureMessage = delegate { };
		public event DeleteFigureMessageDelegate OnDeleteFigureMessage = delegate { };
		public event SayMessageDelegate OnSayMessage = delegate { };
		// Делегаты для обработки входящих сообщений
		public delegate void UnknownMessageDelegate(string messageType);
		public delegate void DeniedMessageDelegate(string text);
		public delegate void UserJoinedMessageDelegate(string username);
		public delegate void UserLeftMessageDelegate(string username);
		public delegate void ChallengedMessageDelegate(string username);
		public delegate void ChallengeRevokedMessageDelegate(string username);
		public delegate void GameStartedMessageDelegate();
		public delegate void GameEndedMessageDelegate();
		public delegate void SayMessageDelegate(string playername, string text);
		public delegate void CreateFigureMessageDelegate(string playername, string position, string figure);
		public delegate void MoveFigureMessageDelegate(string playername, string position1, string position2);
		public delegate void DeleteFigureMessageDelegate(string playername, string position);


		/// <summary>
		/// Вызывается при успешном подключении к серверу.
		/// </summary>
		public event ConnectedDelegate OnConnected = delegate { };
		public delegate void ConnectedDelegate();

		/// <summary>
		/// Вызывается при успешном подключении к комнате.
		/// </summary>
		public event JoinedRoomDelegate OnJoinedRoom = delegate { };
		public delegate void JoinedRoomDelegate();

		private Client client;
		private Connection connection;

		private void OnMessage(object sender, Message m) {
			if (messages.ContainsKey(m.Type)) {
				messages[m.Type](m);
			}
			else {
				OnUnknownMessage(m.Type);
			}
		}
	}
}
