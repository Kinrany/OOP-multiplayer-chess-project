using System;
using System.Linq;
using System.Collections.Generic;
using PlayerIO.GameLibrary;

namespace BouncePlus {
	[RoomType("Trivial v1.1")]
	public class Game : Game<Player> {

		public override bool AllowUserJoin(Player player) {
			Log("Player " + player.ConnectUserId + " tried to join the room.");

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
			Log("Player " + player.ConnectUserId + " joined.");

			player.Initialize(this);
			player.SendPlayerList();

			Broadcast("User joined", player.ConnectUserId);
		}

		public override void UserLeft(Player player) {
			Log("Player " + player.ConnectUserId + " left.");

			Broadcast("User left", player.ConnectUserId);
		}

		public override void GotMessage(Player player, Message message) {
			Log("Player " + player.ConnectUserId + "sent a \"" + message.Type + "\" message. Contents: \n" + message.ToString());

			switch (message.Type) {
				case "Challenge player":
					try {
						if (player.IsPlaying) {
							player.Send("Denied", "You are already playing.");
						}
						Player target = FindPlayerByName(message.GetString(0));
						if (target.IsPlaying) {
							player.Send("Denied", "Target is already playing.");
						}
						player.ChallengePlayer(target);
					}
					catch (Exception e) {
						Log("Challenge player message processing failed.", e);
						player.Send("Denied", "Incorrect message format/player not found.");
					}
					break;
				default:
					player.Send("Denied", "Unknown message type.");
					break;
			}
		}

		private Player FindPlayerByName(string name) {
			return Players.SingleOrDefault<Player>(
				(p) => (p.ConnectUserId == name)
			);
		}

		public void Log(string error) {
			PlayerIO.ErrorLog.WriteError(error);
		}
		public void Log(string error, Exception exception) {
			PlayerIO.ErrorLog.WriteError(error, exception);
		}
		public void Log(string error, string details, string stacktrace, Dictionary<string, string> extraData) {
			PlayerIO.ErrorLog.WriteError(error, details, stacktrace, extraData);
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
			game.Log("Sending player list to " + this.ConnectUserId + ".");
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
			game.Log("Player " + this.ConnectUserId + " challenged player " + other.ConnectUserId + ".");

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
				this.GameCreated(model);
				other.GameCreated(model);

				game.ScheduleCallback(delegate() {
					this.model.Stop();
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
			game.Log("Player " + this.ConnectUserId + " started playing.");

			this.model = model;
			model.OnGameEnded += GameEnded;
			this.Send("Game started");
		}


		private Game game = null;
		private Player challenged = null;
		private GameModel model = null;

		private void GameEnded() {
			game.Log("Player " + this.ConnectUserId + " stopped playing.");

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
		}

		public delegate void GameEndDelegate();
		public event GameEndDelegate OnGameEnded;

		Player player1, player2;
	}
}