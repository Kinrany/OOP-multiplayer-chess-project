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

			figures = new ChessFigures();
			board = figures.LoadBoard(ChessBoard.DefaultBoard);
		}

		public delegate void GameEndDelegate();
		public event GameEndDelegate OnGameEnded;

		public void CreateFigure(string position, string figure) {
			board.CreateFigure(
				new ChessFigurePosition(position), 
				figures.LoadFigure(figure));
		}
		public void MoveFigure(string pos1, string pos2) {
			board.MoveFigure(
				new ChessFigurePosition(pos1),
				new ChessFigurePosition(pos2));
		}
		public void DeleteFigure(string position) {
			board.DeleteFigure(new ChessFigurePosition(position));
		}
		public void ReplaceFigure(string position, string figure) {
			board.ReplaceFigure(
				new ChessFigurePosition(position),
				figures.LoadFigure(figure));
		}

		private Player player1, player2;

		private ChessBoard board;
		private ChessFigures figures;
	}
}
