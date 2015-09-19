using System;
using PlayerIO.GameLibrary;

namespace BouncePlus {
	[RoomType("Trivial v1.0")]
	public class Game : Game<Player> {

		public override bool AllowUserJoin(Player player) {
			// Не позволяет пользователю присоединиться, если имя уже занято
			string requestedId = player.ConnectUserId;
			foreach (var p in Players) {
				if (requestedId == p.ConnectUserId) {
					p.Send("Denied", "Name " + requestedId + " is already in use.");
					return false;
				}
			}
			return true;
		}

		public override void UserJoined(Player player) {
			player.Initialize(this);
			player.SendPlayerList();

			Broadcast("User joined", player.ConnectUserId);
		}

		public override void UserLeft(Player player) {
			Broadcast("User left", player.ConnectUserId);
		}

		public override void GotMessage(Player player, Message message) {
			switch (message.Type) {
				case "ChallengePlayer":
					try {
						if (player.IsPlaying) {
							player.Send("Denied", "You are already playing.");
						}
						Player target = PlayerByName(message.GetString(0));
						if (target.IsPlaying) {
							player.Send("Denied", "Target is already playing.");
						}
						player.ChallengePlayer(target);
					}
					catch {
						player.Send("Denied", "Incorrect message format/player not found.");
					}
					break;
				default:
					player.Send("Denied", "Unknown message type.");
					break;
			}
		}

		private Player PlayerByName(string name) {
			foreach (var player in Players) {
				if (player.ConnectUserId == name) {
					return player;
				}
			}
			return null;
		}
	}

	public class Player : BasePlayer {

		public void Initialize(Game game) {
			this.game = game;
		}

		public void SendPlayerList() {
			foreach (Player player in game.Players) {
				if (player != this) {
					this.Send("User joined", player.ConnectUserId);
				}
			}
		}

		public void ChallengePlayer(Player other) {
			if (challenged != null) {
				challenged.Send("ChallengeRevokedBy", this.ConnectUserId);
			}
			challenged = other;
			other.Send("ChallengedBy", this.ConnectUserId);

			if (other.Challenged == this) {
				GameModel.Create(this, other);
				game.ScheduleCallback(delegate() {
					
				}, 10);
			}
		}

		public bool IsPlaying {
			get {
				return (model != null);
			}
		}
		public Player Challenged {
			get {
				return challenged;
			}
		}

		public void GameStarted(GameModel model) {
			this.model = model;
			this.Send("GameStarted");
		}
		public void GameEnded() {
			model = null;
			this.Send("Game Ended");
		}

		private Game game;
		private Player challenged = null;
		private GameModel model = null;
	}

	public class GameModel {

		private GameModel(Player player1, Player player2) {
			this.player1 = player1;
			this.player2 = player2;
		}

		public void Stop() {
			player1.GameEnded();
			player2.GameEnded();
		}

		public static void Create(Player player1, Player player2) {
			GameModel model = new GameModel(player1, player2);
			player1.GameStarted(model);
			player2.GameStarted(model);
		}

		Player player1, player2;
	}
}