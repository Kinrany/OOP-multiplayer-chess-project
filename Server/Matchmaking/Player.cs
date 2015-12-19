using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerIO.GameLibrary;

namespace Matchmaking {
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

		public GameModel GameModel {
			get {
				return model;
			}
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
}
