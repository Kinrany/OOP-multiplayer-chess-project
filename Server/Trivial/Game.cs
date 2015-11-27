using System;
using System.Linq;
using PlayerIO.GameLibrary;

namespace BouncePlus {
	[RoomType("Trivial v1.20")]
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
				case "Challenge player":
					if (player.IsPlaying) {
						player.Send("Denied", "You are already playing.");
						break;
					}

					Player target = FindPlayerByName(message.GetString(0));

					if (target == null) {
						player.Send("Denied", "Target not found.");
						break;
					}
					if (target.IsPlaying) {
						player.Send("Denied", "Target is already playing.");
						break;
					}

					player.ChallengePlayer(target);
					break;

				default:
					player.Send("Denied", "Unknown message type.");
					break;
			}
		}

		private Player FindPlayerByName(string name) {
			return Players.FirstOrDefault(
				(p) => (p.ConnectUserId == name)
			);

			//// На случай, если ^ сломается
			//
			//foreach (var player in Players) {
			//    if (player.ConnectUserId == name) {
			//        return player;
			//    }
			//}
			//return null;
		}
	}

	public class Player : BasePlayer {

		public void Initialize(Game game) {
			if (this.game == null) {
				this.game = game;
			}
			else {
				throw new InvalidOperationException("Player already initialized");
			}
		}

		/// <summary>
		/// Посылает игроку список со всеми остальными игроками.
		/// </summary>
		public void SendPlayerList() {
			foreach (Player player in game.Players) {
				if (player != this) {
					this.Send("User joined", player.ConnectUserId);
				}
			}
		}

		/// <summary>
		/// Вызывает другого игрока на игру.
		/// </summary>
		/// <param name="other">Вызванный игрок.</param>
		public void ChallengePlayer(Player other) {
			if (other == this) {
				this.Send("Denied", "You can't challenge yourself.");
				return;
			}
			if (challenged != null) {
				challenged.Send("Challenge revoked", this.ConnectUserId);
			}
			challenged = other;
			other.Send("Challenged", this.ConnectUserId);

			if (other.Challenged == this) {
				GameModel model = new GameModel(this, other);
				game.ScheduleCallback(delegate () {
					model.Stop();
				}, 1000);
			}
		}

		/// <summary>
		/// Играет ли игрок в данный момент.
		/// </summary>
		public bool IsPlaying {
			get {
				return (model != null);
			}
		}
		/// <summary>
		/// Игрок, вызванный на игру.
		/// </summary>
		public Player Challenged {
			get {
				return challenged;
			}
		}

		public void GameCreated(GameModel model) {
			this.model = model;
			model.OnGameEnded += GameEnded;
			this.Send("Game started");
		}


		private Game game = null;
		private Player challenged = null;
		private GameModel model = null;

		private void GameEnded() {
			model = null;
			this.Send("Game ended");
		}
	}

	public class GameModel {

		/// <summary>
		/// Останавливает игру.
		/// </summary>
		public void Stop() {
			OnGameEnded();
		}

		/// <summary>
		/// Создаёт новую игру для двух игроков.
		/// </summary>
		/// <param name="player1">Первый игрок.</param>
		/// <param name="player2">Второй игрок.</param>
		public GameModel(Player player1, Player player2) {
			this.player1 = player1;
			this.player2 = player2;
			player1.GameCreated(this);
			player2.GameCreated(this);
		}

		public delegate void GameEndDelegate();
		public event GameEndDelegate OnGameEnded;

		Player player1, player2;
	}
}