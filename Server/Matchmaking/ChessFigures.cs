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

		public string SaveFigure(ChessFigure figure) {
			return figureCodes[figure];
		}

		public ChessBoard LoadBoard(string savedBoard) {
			ChessBoard board = new ChessBoard();

			string[] rows = savedBoard.Split(new char[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
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

		public string SaveBoard(ChessBoard board) {
			StringBuilder savedBoard = new StringBuilder("");

			for (int row = board.Rows - 1; row >= 0; --row) {
				for (int col = 0; col < board.Columns; ++col) {
					savedBoard.Append(SaveFigure(board.Array[col, row]));
					savedBoard.Append(' ');
				}
				savedBoard.Append('\n');
			}

			return savedBoard.ToString();
		}


		private Dictionary<string, ChessFigure> figures = new Dictionary<string, ChessFigure>();
		private Dictionary<ChessFigure, string> figureCodes = new Dictionary<ChessFigure, string>();

		private void LoadDefaultFigures() {
			AddFigure("K", ChessFigure.WhiteKing);
			AddFigure("Q", ChessFigure.WhiteQueen);
			AddFigure("R", ChessFigure.WhiteRook);
			AddFigure("B", ChessFigure.WhiteBishop);
			AddFigure("N", ChessFigure.WhiteKnight);
			AddFigure("P", ChessFigure.WhitePawn);
			AddFigure("k", ChessFigure.BlackKing);
			AddFigure("q", ChessFigure.BlackQueen);
			AddFigure("r", ChessFigure.BlackRook);
			AddFigure("b", ChessFigure.BlackBishop);
			AddFigure("n", ChessFigure.BlackKnight);
			AddFigure("p", ChessFigure.BlackPawn);
		}

		private void AddFigure(string code, ChessFigure figure) {
			figures[code] = figure;
			figureCodes[figure] = code;
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
