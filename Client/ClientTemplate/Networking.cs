using System;
using PlayerIOClient;

namespace ClientNamespace {
	class Networking {
		public Networking(string playerName) {
			client = PlayerIO.Connect(
				"test-game-1-sebwxyhi2k6yfeymgnxfa",
				"public",
				playerName,
				null, null);

			connection = null;
		}

		public bool Connected {
			get {
				return (connection != null && connection.Connected);
			}
		}
		public void Connect(string roomType) {
			connection = client.Multiplayer.CreateJoinRoom(
				"my-room-id",
				roomType,
				true,
				null, null);

			connection.OnMessage += OnMessage;
		}

		public delegate void DeniedMessageDelegate(string text);
		public event DeniedMessageDelegate OnDeniedMessage;

		public delegate void UnknownMessageDelegate(string messageType);
		public event UnknownMessageDelegate OnUnknownMessage;

		public delegate void ConnectedDelegate();
		public event ConnectedDelegate OnConnected;

		private Client client;
		private Connection connection;

		private void OnMessage(object sender, Message m) {
			switch (m.Type) {
				case "Denied":
					OnDeniedMessage((string)m[0]);
					break;
				default:
					OnUnknownMessage(m.Type);
					break;
			}
		}
	}
}
