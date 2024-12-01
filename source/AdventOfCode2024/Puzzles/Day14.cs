using System.Runtime.CompilerServices;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day14 : HappyPuzzleBase
{
	public override object SolvePart1(Input input)
	{
		int rows = input.Lines.Length;
		int columns = input.Lines[0].Length;

		scoped Span<char> board = stackalloc char[rows * columns];
		InputToBoard(input, rows, columns, board);
		RollStonesNorth(board, rows, columns);

		PrintBoard(rows, board, columns); //TODO comment

		var total = CalculateBoard(rows, columns, board);
		return total;
	}

	private static void PrintBoard(int rows, Span<char> board, int columns)
	{
		for (int i = 0; i < rows; i++)
		{
			var line = board.Slice(columns * i, columns);
			Console.WriteLine(new string(line));
		}
		Console.WriteLine();
	}

	private static int CalculateBoard(int rows, int columns, Span<char> board)
	{
		var total = 0;
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				if (board[GetIndex(i, j, columns)] == 'O')
					total += (rows - i);
			}
		}

		return total;
	}

	private void RollStonesNorth(scoped Span<char> board, int rows, int columns)
	{
		for (int j = 0; j < columns; j++)
		{
			var freeRow = 0;
			for (int i = 0; i < rows; i++)
			{
				switch (board[GetIndex(i,j,columns)])
				{
					case 'O':
						//Roll to max north position and put . over there
						board[GetIndex(i, j, columns)] = '.';
						board[GetIndex(freeRow, j, columns)] = 'O';
						freeRow++;
						break;
					case '#':
						freeRow = i + 1; // next row
						break;
				}
			}
		}
	}

	private void RollStonesSouth(scoped Span<char> board, int rows, int columns)
	{
		for (int j = 0; j < columns; j++)
		{
			var freeRow = 0;
			for (int i = 0; i < rows; i++)
			{
				switch (board[GetIndex(rows-i - 1,columns-j - 1,columns)])
				{
					case 'O':
						//Roll to max north position and put . over there
						board[GetIndex(rows-i - 1, columns-j - 1, columns)] = '.';
						board[GetIndex(rows - 1 -freeRow, columns-j - 1, columns)] = 'O';
						freeRow++;
						break;
					case '#':
						freeRow = i + 1; // next row
						break;
				}
			}
		}
	}

	private void RollStonesLeft(scoped Span<char> board, int rows, int columns)
	{
		for (int i = 0; i < rows; i++)
		{
			var freeColumn = 0;
			for (int j = 0; j < columns; j++)
			{
				switch (board[GetIndex(i,j,columns)])
				{
					case 'O':
						//Roll to max north position and put . over there
						board[GetIndex(i, j, columns)] = '.';
						board[GetIndex(i, freeColumn, columns)] = 'O';
						freeColumn++;
						break;
					case '#':
						freeColumn = j + 1; // next row
						break;
				}
			}
		}
	}

	private void RollStonesRight(scoped Span<char> board, int rows, int columns)
	{
		for (int i = 0; i < rows; i++)
		{
			var freeColumns = 0;
			for (int j = 0; j < columns; j++)
			{
				switch (board[GetIndex(rows-i - 1,columns-j - 1,columns)])
				{
					case 'O':
						if(freeColumns < columns)
						{
							//Roll to max north position and put . over there
							board[GetIndex(rows - i - 1, columns - j - 1, columns)] = '.';
							board[GetIndex(rows - i - 1, columns - freeColumns -1, columns)] = 'O';
							freeColumns++;
						}
						break;
					case '#':
						freeColumns = j + 1; // next row
						break;
				}
			}
		}
	}

	private static void InputToBoard(Input input, int rows, int columns, Span<char> board)
	{
		for (var i = 0; i < rows; i++)
		{
			for (var j = 0; j < columns; j++)
			{
				board[GetIndex(i, j, columns)] = input.Lines[i][j];
			}
		}
	}


	//[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	public override object SolvePart2(Input input)
	{
		int rows = input.Lines.Length;
		int columns = input.Lines[0].Length;

		scoped Span<char> board = stackalloc char[rows * columns];
		InputToBoard(input, rows, columns, board);

		var dict = new Dictionary<long, long>();

		var currentCycleScore = 0;
		var cycle = 0;
		//while (!(dict.ContainsKey(currentCycleScore) && dict[currentCycleScore] > 100))
		while (cycle < 1000000000) //
		{
			RollStonesNorth(board, rows, columns);
			RollStonesLeft(board, rows, columns);
			RollStonesSouth(board, rows, columns);
			RollStonesRight(board, rows, columns);
			//PrintBoard(rows, board, columns); //TODO comment

			// Console.WriteLine(" -------------- ");
			// Console.WriteLine(" Board after cycle");
			// Console.WriteLine("");
			//PrintBoard(rows, board, columns); //TODO comment
			currentCycleScore = CalculateBoard(rows, columns, board);

			cycle++;

			if (!dict.ContainsKey(currentCycleScore))
			{
				dict.Add(currentCycleScore, cycle);
			}
			else
			{
				var startOcc = dict[currentCycleScore];

				if(cycle - startOcc > 2)
				{
					var x = (1000000000.00 - startOcc) / (cycle - startOcc);
					var x100 = (long) (x * 100);
					var rest = x100 % 100;
					if (rest == 0)
					{
						return currentCycleScore;
					}
				}
			}
		}

		return -1;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static int GetIndex(int i, int j, int columns)
	{
		return i * columns + j;
	}
}