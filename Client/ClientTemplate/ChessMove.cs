using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientNamespace
{
	class ChessMoves
	{
		public bool MovementValidationPawn(ChessBoard board, ChessFigurePosition position, ChessFigurePosition destination)
		{
			if (board[destination] == ChessFigure._)
			{
				//Console.WriteLine(position.Row);
				if (position.Row == 2 && position.Row + 2 == destination.Row)
				{
					return true;
				}
				if (position.Row == 7 && position.Row - 2 == destination.Row)
				{
					return true;
				}

				if (destination.Row <= 8 && position.Row + 1 == destination.Row)
				{
					return true;
				}
				if (destination.Row <= 8 && position.Row - 1 == destination.Row)
				{
					return true;
				}

				return false;
			}
			else
			{
				if (position.Row + 1 == destination.Row && 
					position.Column + 1 == destination.Column &&
					(int)board[destination] * (int)board[position] < 0)
				{
					return true;
				}
				else if (position.Row + 1 == destination.Row &&
					position.Column - 1 == destination.Column &&
					(int)board[destination] * (int)board[position] < 0)
				{
					return true;
				}
				return false;
			}
		}
		public bool MovementValidationBishop(ChessBoard board, ChessFigurePosition position, ChessFigurePosition destination)
		{
			ChessFigurePosition tmp = position;

			while (true)
			{
				if (position.Column > destination.Column && position.Row < destination.Row) // I четверть
				{
					position.Column--;
					position.Row++;
				}
				else if (position.Column > destination.Column && position.Row > destination.Row) // II четверть
				{
					position.Column--;
					position.Row--;
				}
				else if (position.Column < destination.Column && position.Row > destination.Row) // III четверть
				{
					position.Column++;
					position.Row--;
				}
				else if (position.Column < destination.Column && position.Row < destination.Row) // IV четверть
				{
					position.Column++;
					position.Row++;
				}

				if (position.Column == destination.Column && position.Row == destination.Row)
				{
					if ((int)board[destination] * (int)board[tmp] <= 0)
						return true;
					else
						return false;
				}

				if (board[position] != ChessFigure._) //препятсвие
					return false;
				if ((position.Column == destination.Column && position.Row != destination.Row)
														   ||
					(position.Column != destination.Column && position.Row == destination.Row))
					return false;
			}
		}
		public bool MovementValidationKnight(ChessBoard board, ChessFigurePosition position, ChessFigurePosition destination)
		{
			if (position.Column - 2 == destination.Column && position.Row - 1 == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else if (position.Column - 2 == destination.Column && position.Row + 1 == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else if (position.Column + 2 == destination.Column && position.Row - 1 == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else if (position.Column + 2 == destination.Column && position.Row + 1 == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else if (position.Column - 1 == destination.Column && position.Row - 2 == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else if (position.Column - 1 == destination.Column && position.Row + 2 == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else if (position.Column + 1 == destination.Column && position.Row - 2 == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else if (position.Column + 1 == destination.Column && position.Row + 2 == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else
				return false;
		}
		public bool MovementValidationRook(ChessBoard board, ChessFigurePosition position, ChessFigurePosition destination)
		{
			ChessFigurePosition tmp = position;

			while (true)
			{
				if (position.Column > destination.Column && position.Row == destination.Row) // ниже
				{
					position.Column--;
				}
				else if (position.Column < destination.Column && position.Row == destination.Row) // выше
				{
					position.Column++;
				}
				else if (position.Column == destination.Column && position.Row > destination.Row) // правее
				{
					position.Row--;
				}
				else if (position.Column == destination.Column && position.Row < destination.Row) // левее
				{
					position.Row++;
				}
				else
				{
					return false;
				}

				if (position.Column == destination.Column && position.Row == destination.Row)
				{
					if ((int)board[destination] * (int)board[tmp] <= 0)
						return true;
					else
						return false;
				}

				if (board[position] != ChessFigure._) //препятсвие
					return false;
			}
		}
		public bool MovementValidationQueen(ChessBoard board, ChessFigurePosition position, ChessFigurePosition destination)
		{
			if ((position.Column == destination.Column && position.Row != destination.Row)
													   ||
				(position.Column != destination.Column && position.Row == destination.Row))
			{
				if (MovementValidationRook(board, position, destination))
					return true;
				else
					return false;
			}
			else if (position.Column != destination.Column && position.Row != destination.Row)
			{
				if (MovementValidationBishop(board, position, destination))
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}
		}

		public bool MovementValidationKing(ChessBoard board, ChessFigurePosition position, ChessFigurePosition destination)
		{
			if (position.Column - 1 == destination.Column && position.Row + 1  == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else if (position.Column == destination.Column && position.Row + 1  == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else if (position.Column + 1 == destination.Column && position.Row + 1  == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else if (position.Column + 1 == destination.Column && position.Row  == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else if (position.Column + 1 == destination.Column && position.Row - 1  == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else if (position.Column == destination.Column && position.Row - 1  == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else if (position.Column - 1 == destination.Column && position.Row - 1  == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else if (position.Column - 1 == destination.Column && position.Row  == destination.Row)
			{
				if ((int)board[destination] * (int)board[position] <= 0)
					return true;
				else
					return false;
			}
			else
				return false;
		}
	}
}
