using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientNamespace {
	class GameData {
		public GameData(Networking networking) {
			this.networking = networking;
			this.networking.OnMoveFigureMessage += OnMoveFigureMessage;
			this.networking.OnMoveFigureMessage += delegate (string p, string f, string t) { GUIMove(p, f, t); };
			networking.OnGameStartedMessage += GameStartedHandler;
			networking.OnGameEndedMessage += GameEndedHandler;

			figures = new ChessFigures();

			NewGame();
		}

		public ChessBoard Board {
			get { return board; }
		}

		public event Networking.MoveFigureMessageDelegate GUIMove = delegate { };

		public void NewGame() {
			board = figures.LoadBoard(ChessBoard.DefaultBoard);
		}

		public void MoveFigure(ChessFigurePosition position1, ChessFigurePosition position2,string a,string b) {
			networking.MoveFigure(a, b);
			board.MoveFigure(position1, position2);
		}
		
		
		private Networking networking;
		private ChessFigures figures;

		private ChessBoard board;

		private void OnMoveFigureMessage(string playername, string from, string to) {
			ChessFigurePosition tmp1 = new ChessFigurePosition(from);
			ChessFigurePosition tmp2 = new ChessFigurePosition(to);
			board.MoveFigure(tmp1, tmp2);
		}

		private void GameStartedHandler() {
			NewGame();
		}

		private void GameEndedHandler() { }
	}
}
