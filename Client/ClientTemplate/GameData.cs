using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientNamespace
{
	class GameData
	{
		public GameData(Networking networking)
		{
			this.networking = networking;
			this.networking.OnMoveFigureMessage += OnMoveFigureMessage;
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

		private void GameStartedHandler()
		{
			NewGame();
		}

		public void MoveFigure(ChessFigurePosition position1, ChessFigurePosition position2,string a,string b)
		{
			networking.MoveFigure(position1, position2);
			board.MoveFigure(position1, position2);
		}

		private void OnMoveFigureMessage(string playername, ChessFigurePosition from, ChessFigurePosition to)
		{
			board.MoveFigure(from, to);
		}

		//public event Networking.MoveFigureMessageDelegate OnMoveFigureMessage = delegate { };

		private void GameEndedHandler() { }

		private Networking networking;
		private ChessFigures figures;

		private ChessBoard board;
	}
}
