using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matchmaking {
	class ChessFigures {
		public ChessFigures() {
			LoadDefaultFigures();
		}

		public ChessFigure LoadFigure(string str) {
			ChessFigure figure;
			if (figures.TryGetValue(str, out figure)) {
				return figure;
			}
			else {
				return ChessFigure.None;
			}
		}

		public ChessBoard LoadBoard(string savedBoard) {
			ChessBoard board = new ChessBoard();

			string[] rows = savedBoard.Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
			for (int row = board.Rows - 1; row >= 0; --row) {
				string[] columns = rows[row].Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
				for (int col = 0; col < board.Columns && col < rows[row].Length; ++col) {
					board.Array[col, row] = LoadFigure(columns[col]);
                }
				for (int col = rows[row].Length; col < board.Columns; ++col) {
					board.Array[col, row] = ChessFigure.None;
				}
			}

			return board;
		}


		private Dictionary<string, ChessFigure> figures = new Dictionary<string, ChessFigure>();

		private void LoadDefaultFigures() {
			figures["K"] = ChessFigure.WhiteKing;
			figures["Q"] = ChessFigure.WhiteQueen;
			figures["R"] = ChessFigure.WhiteRook;
			figures["B"] = ChessFigure.WhiteBishop;
			figures["N"] = ChessFigure.WhiteKnight;
			figures["P"] = ChessFigure.WhitePawn;
			figures["k"] = ChessFigure.BlackKing;
			figures["q"] = ChessFigure.BlackQueen;
			figures["r"] = ChessFigure.BlackRook;
			figures["b"] = ChessFigure.BlackBishop;
			figures["n"] = ChessFigure.BlackKnight;
			figures["p"] = ChessFigure.BlackPawn;
		}
	}

	enum ChessFigure {
		None,
		WhiteKing,
		WhiteQueen,
		WhiteRook,
		WhiteBishop,
		WhiteKnight,
		WhitePawn,
		BlackKing,
		BlackQueen,
		BlackRook,
		BlackBishop,
		BlackKnight,
		BlackPawn
	}
}
