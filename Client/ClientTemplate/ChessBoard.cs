using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientNamespace
{
	class ChessBoard
	{

		public void MoveFigure(ChessFigurePosition position1, ChessFigurePosition position2)
		{
			this[position2] = this[position1];
			this[position1] = ChessFigure.None;
		}

		public void CreateFigure(ChessFigurePosition position, ChessFigure figure)
		{
			if (this[position] == ChessFigure.None)
			{
				this[position] = figure;
			}
			else {
				throw new InvalidOperationException("There's a chess figure at this position.");
			}
		}

		public void DeleteFigure(ChessFigurePosition position)
		{
			if (this[position] != ChessFigure.None)
			{
				this[position] = ChessFigure.None;
			}
			else {
				throw new InvalidOperationException("There are no chess figures at this position.");
			}
		}

		public ChessFigure this[ChessFigurePosition position]
		{
			get
			{
				return Array[position.Column - ChessFigurePosition.MIN_COLUMN, position.Row - ChessFigurePosition.MIN_COLUMN];
			}
			set
			{
				Array[position.Column - ChessFigurePosition.MIN_COLUMN, position.Row - ChessFigurePosition.MIN_COLUMN] = value;
			}
		}

		public override string ToString()
		{
			string res_board = "";
			for (int collum = 0; collum < Columns; collum++)
			{
				for (int row = 0; row < Rows; row++)
					res_board += Array[row, collum] + " ";
				res_board += "\n";
			}
			return res_board;
		}

		public int Columns
		{
			get
			{
				return Array.GetLength(0);
			}
		}
		public int Rows
		{
			get
			{
				return Array.GetLength(1);
			}
		}

		public const string DefaultBoard = @"   r n b q k b n r
												p p p p p p p p
												. . . . . . . .
												. . . . . . . .
												. . . . . . . .
												. . . . . . . .
												P P P P P P P P
												R N B Q K B N R";

		public ChessFigure[,] Array = new ChessFigure[
			ChessFigurePosition.MAX_COLUMN - ChessFigurePosition.MIN_COLUMN + 1,
			ChessFigurePosition.MAX_ROW - ChessFigurePosition.MIN_ROW + 1];
	}

	struct ChessFigurePosition
	{

		/// <summary>
		/// Создаёт новые координаты на шахматном поле по букве и номеру строки.
		/// </summary>
		/// <param name="column">Буква от MIN_COLUMN до MAX_COLUMN, соответствующая столбцу.</param>
		/// <param name="row">Число от MIN_ROW до MAX_ROW, соответствующее строке.</param>
		public ChessFigurePosition(char column, int row)
		{
			column = Char.ToUpper(column);
			if (!isValid(column))
			{
				throw new ArgumentException(invalidColumn());
			}

			if (!isValid(row))
			{
				throw new ArgumentException(invalidRow());
			}

			this.column = column;
			this.row = row;
		}

		/// <summary>
		/// Создаёт новые координаты на шахматном поле по строке формата "A1".
		/// </summary>
		/// <param name="str"></param>
		public ChessFigurePosition(string str) : this(str[0], (int)(str[1] - '0')) { }

		public char Column
		{
			get
			{
				return column;
			}
			set
			{
				char upperCase = Char.ToUpper(value);
				if (isValid(upperCase))
				{
					column = upperCase;
				}
				else {
					throw new InvalidOperationException(invalidColumn());
				}
			}
		}
		public int Row
		{
			get
			{
				return row;
			}
			set
			{
				if (isValid(value))
				{
					row = value;
				}
				else {
					throw new InvalidOperationException(invalidRow());
				}
			}

		}

		public const char MIN_COLUMN = 'A';
		public const char MAX_COLUMN = 'H';
		public const int MIN_ROW = 1;
		public const int MAX_ROW = 8;

		private static bool isValid(char column)
		{
			return (MIN_COLUMN <= column && column <= MAX_COLUMN);
		}
		private static bool isValid(int row)
		{
			return (MIN_ROW <= row && row <= MAX_ROW);
		}

		private static string invalidColumn()
		{
			return String.Format("Column should be a character in range ['{0}', '{1}'].", MIN_COLUMN, MAX_COLUMN);
		}
		private static string invalidRow()
		{
			return String.Format("Row should be a number in range [{0}, {1}].", MIN_ROW, MAX_ROW);
		}

		private char column;
		private int row;
	}
}
