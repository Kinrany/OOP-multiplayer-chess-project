using System;
using PlayerIOClient;

namespace ClientNamespace {
	class Networking {
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

		// События для обработки входящих сообщений
		// "= delegate { }" спасает от исключения, если нет подписчиков
		public event UnknownMessageDelegate OnUnknownMessage = delegate { };
		public event DeniedMessageDelegate OnDeniedMessage = delegate { };
		public event UserJoinedMessageDelegate OnUserJoinedMessage = delegate { };
		public event UserLeftMessageDelegate OnUserLeftMessage = delegate { };
		public event ChallengedMessageDelegate OnChallengedMessage = delegate { };
		public event ChallengeRevokedMessageDelegate OnChallengeRevokedMessage = delegate { };
		public event GameStartedMessageDelegate OnGameStartedMessage = delegate { };
		public event GameEndedMessageDelegate OnGameEndedMessage = delegate { };
		// Делегаты для обработки входящих сообщений
		public delegate void UnknownMessageDelegate(string messageType);
		public delegate void DeniedMessageDelegate(string text);
		public delegate void UserJoinedMessageDelegate(string username);
		public delegate void UserLeftMessageDelegate(string username);
		public delegate void ChallengedMessageDelegate(string username);
		public delegate void ChallengeRevokedMessageDelegate(string username);
		public delegate void GameStartedMessageDelegate();
		public delegate void GameEndedMessageDelegate();

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
			switch (m.Type) {
				case "Denied":
					OnDeniedMessage((string)m[0]);
					break;
				case "User joined":
					OnUserJoinedMessage((string)m[0]);
					break;
				case "User left":
					OnUserLeftMessage((string)m[0]);
					break;
				case "Challenged":
					OnChallengedMessage((string)m[0]);
					break;
				case "Challenge revoked":
					OnChallengeRevokedMessage((string)m[0]);
					break;
				case "Game started":
					OnGameStartedMessage();
					break;
				case "Game ended":
					OnGameEndedMessage();
					break;
				default:
					OnUnknownMessage(m.Type);
					break;
			}
		}
	}
}
