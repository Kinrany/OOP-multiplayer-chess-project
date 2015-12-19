using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientNamespace
{
	class GameData
	{
		public GameData(Networking networking) {
			this.networking = networking;
			networking.OnGameStartedMessage += GameStartedHandler;
			networking.OnGameEndedMessage += GameEndedHandler;

			figures = new ChessFigures();

			NewGame();
		}

		public void NewGame()
		{
			board = figures.LoadBoard(ChessBoard.DefaultBoard);
		}

		public ChessBoard Board
		{
			get { return board; }
		}

		private Networking networking;
		private ChessFigures figures;

		private ChessBoard board;

		private void GameStartedHandler()
		{
			NewGame();
		}

		public void MoveFigure(ChessFigurePosition position1, ChessFigurePosition position2,string a,string b)
		{
			networking.MoveFigure(a, b);
			board.MoveFigure(position1, position2);
		}

		private void GameEndedHandler() { }
	}
}
