using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientNamespace
{
	class ChessFigures
	{
		public ChessFigures()
		{
			LoadDefaultFigures();
		}

		public ChessFigure LoadFigure(string str)
		{
			ChessFigure figure;
			if (figures.TryGetValue(str, out figure))
			{
				return figure;
			}
			else {
				return ChessFigure._;
			}
		}

		public ChessBoard LoadBoard(string savedBoard)
		{
			ChessBoard board = new ChessBoard();

			string[] rows = savedBoard.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
			for (int row = 0; row <= board.Rows - 1; row++)
			{
				string[] columns = rows[row].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
				columns[columns.Length - 1] = columns[0];
				for (int col = 0; col < board.Columns && col < rows[row].Length; ++col)
				{
					board.Array[col, row] = LoadFigure(columns[col]);
				}
				for (int col = rows[row].Length; col < board.Columns; ++col)
				{
					board.Array[col, row] = ChessFigure._;
				}
			}

			return board;
		}


		private Dictionary<string, ChessFigure> figures = new Dictionary<string, ChessFigure>();

		private void LoadDefaultFigures()
		{
			figures["K"] = ChessFigure.K;
			figures["Q"] = ChessFigure.Q;
			figures["R"] = ChessFigure.R;
			figures["B"] = ChessFigure.B;
			figures["N"] = ChessFigure.N;
			figures["P"] = ChessFigure.P;
			figures["k"] = ChessFigure.k;
			figures["q"] = ChessFigure.q;
			figures["r"] = ChessFigure.r;
			figures["b"] = ChessFigure.b;
			figures["n"] = ChessFigure.n;
			figures["p"] = ChessFigure.p;
		}
	}

	enum ChessFigure
	{
		k = -6, 
		q,      
		r,
		b,
		n,
		p,
		_,
		K,
		Q,
		R,
		B,
		N,
		P
	}
}
