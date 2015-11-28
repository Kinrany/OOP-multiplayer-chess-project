using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerIO.GameLibrary;

namespace Matchmaking {
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
