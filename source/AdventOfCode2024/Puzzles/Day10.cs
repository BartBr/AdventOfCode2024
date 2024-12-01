using System.Collections;
using System.Data.Common;
using System.Dynamic;
using System.Runtime.CompilerServices;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day10 : HappyPuzzleBase
{
	private const char UP_DOWN = '|'; // is a vertical pipe connecting north and south.
	private const char LEFT_RIGHT = '-'; // is a horizontal pipe connecting east and west.
	private const char UP_RIGHT= 'L'; // is a 90-degree bend connecting north and east.
	private const char UP_LEFT = 'J'; // is a 90-degree bend connecting north and west.
	private const char DOWN_LEFT = '7'; // is a 90-degree bend connecting south and west.
	private const char DOWN_RIGHT = 'F'; // is a 90-degree bend connecting south and east.

	private static readonly char[] PossibleStartingPipes = new[] { DOWN_RIGHT ,UP_DOWN, LEFT_RIGHT, UP_RIGHT, UP_LEFT, DOWN_LEFT, DOWN_RIGHT };
	private static readonly char[] PossiblePipes = new[] {  UP_DOWN, UP_RIGHT, UP_LEFT};

	public override object SolvePart1(Input input)
	{
		var c = input.Lines;

		GetStartingPosition(c, out var startPosColumn, out var startPosRow);

		foreach (var startingPipe in PossibleStartingPipes)
		{
			var calculateRouteLength = CalculateRouteLength(c, startingPipe, startPosRow, startPosColumn);
			if (calculateRouteLength > 0)
			{
				return calculateRouteLength / 2;
			}
		}

		return -1;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static int CalculateRouteLength(string[] c, char startingPipe, int startPosRow, int startPosColumn)
	{
		int rows = c.Length;
		int columns = c[0].Length;

		int length = 0;
		var i = startPosRow;
		var j = startPosColumn;

		var direction = Direction.Unknown;

		var currentChar = startingPipe;
		while (length < 10000000)
		{
			switch (currentChar)
			{
				case UP_DOWN:
					if (i == 0 || i + 1 == rows) return -1;
					if (direction is Direction.GoingLeft or Direction.GoingRight) return -1;
					i = direction== Direction.GoingUp ? i - 1 : i + 1;
					break;
				case LEFT_RIGHT:
					if (j == 0 || j + 1 == columns) return -1;
					j = direction == Direction.GoingRight ? j + 1 : j - 1;
					break;
				case UP_LEFT:
					if (direction is Direction.GoingUp or Direction.GoingLeft) return -1;
					if (j == 0 || i == 0) return -1;

					direction = direction == Direction.GoingRight ? Direction.GoingUp : Direction.GoingLeft;

					if (direction == Direction.GoingUp)
					{
						i--;
					}
					else
					{
						j--;
					}

					break;
				case UP_RIGHT:
					if (direction is Direction.GoingUp or Direction.GoingRight) return -1;
					if (j +1 == columns || i == 0) return -1;

					direction = direction == Direction.GoingLeft ? Direction.GoingUp : Direction.GoingRight;

					if (direction == Direction.GoingUp)
					{
						i--;
					}
					else
					{
						j++;
					}
					break;
				case DOWN_RIGHT:
					if (direction is Direction.GoingDown or Direction.GoingRight) return -1;
					if (j +1 == columns || i +1== rows) return -1;

					direction = direction == Direction.GoingLeft ? Direction.GoingDown : Direction.GoingRight;

					if (direction == Direction.GoingDown)
					{
						i++;
					}
					else
					{
						j++;
					}
					break;
				case DOWN_LEFT:
					if (direction is Direction.GoingDown or Direction.GoingLeft) return -1;
					if (j +1 == 0 || i +1== rows) return -1;

					direction = direction == Direction.GoingRight ? Direction.GoingDown : Direction.GoingLeft;

					if (direction == Direction.GoingDown)
					{
						i++;
					}
					else
					{
						j--;
					}
					break;
			}
			//Console.WriteLine($"new direction: {direction}, new position: {i}:{j} char: {c[i][j]}");

			length++;

			if (i == startPosRow && j == startPosColumn)
			{
				return length;
			}

			currentChar = c[i][j];
		}

		throw new Exception("invalid");
	}

	private enum Direction
	{
		GoingUp = 0,
		GoingDown = 1,
		GoingLeft = 2,
		GoingRight = 3,
		Unknown = 4,
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static void GetStartingPosition(string[] c, out int startPosColumn, out int startPosRow)
	{
		startPosRow = 0;
		startPosColumn = 0;
		for (var i = 0; i < c.Length; i++)
		{
			for (var j = 0; j < c[i].Length; j++)
			{
				if (c[i][j] == 'S')
				{
					startPosColumn = j;
					startPosRow = i;
					return;
				}
			}
		}
	}

	public override object SolvePart2(Input input)
	{
		GetStartingPosition(input.Lines, out var startPosColumn, out var startPosRow);

		var rows = input.Lines.Length;
		var columns = input.Lines[0].Length;

		var spanLength = rows * columns;
		scoped Span<char> field = stackalloc char[spanLength];
		field.Fill('.');

		FillLoopIntoField(input.Lines, startPosRow, startPosColumn, field);

		for (int i = 0; i < rows; i++)
		{
			var amountOfPipesFromTheLeft = 0;
			for (int j = 0; j < columns; j++)
			{
				var index = GetIndex(i, j, columns);
				var ch = field[index];
				if (ch == '.' && amountOfPipesFromTheLeft % 2 == 0)
				{
					field[index] = '0';
				}
				if (PossiblePipes.Contains(ch))
				{
					amountOfPipesFromTheLeft++;
				}
			}
		}

		// for (var i = 0; i < rows; i++)
		// {
		// 	for (var j = 0; j < columns; j++)
		// 	{
		// 		Console.Write(field[i*columns + j]);
		// 	}
		// 	Console.WriteLine();
		// }

		var total = 0;
		for (int i = 0; i < field.Length; i++)
		{
			if (field[i] == '.')
			{
				total++;
			}
		}

		return total;
	}

	private static int GetIndex(int i, int j, int columns)
	{
		return i * columns + j;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static void FillLoopIntoField(string[] inputLines, int startPosRow, int startPosColumn, scoped Span<char> field)
	{
		foreach (var startingPipe in PossibleStartingPipes)
		{
			var loopLength = CalculateRouteLength(inputLines, startingPipe, startPosRow, startPosColumn);
			if (loopLength > 0)
			{
				CreateLoop(inputLines, startingPipe, startPosRow, startPosColumn, field);
				return;
			}
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static void CreateLoop(string[] c, char startingPipe, int startPosRow, int startPosColumn, scoped Span<char> field)
	{
		int rows = c.Length;
		int columns = c[0].Length;

		var i = startPosRow;
		var j = startPosColumn;

		var direction = Direction.Unknown;

		var currentChar = startingPipe;

		var foundStart = false;
		while (!foundStart)
		{
			field[GetIndex(i,j,columns)] = c[i][j];
			if (field[GetIndex(i, j, columns)] == 'S') field[GetIndex(i, j, columns)] = startingPipe;

			switch (currentChar)
			{
				case UP_DOWN:
					i = direction == Direction.GoingUp ? i - 1 : i + 1;
					break;
				case LEFT_RIGHT:
					j = direction == Direction.GoingRight ? j + 1 : j - 1;
					break;
				case UP_LEFT:
					direction = direction == Direction.GoingRight ? Direction.GoingUp : Direction.GoingLeft;
					if (direction == Direction.GoingUp)
					{
						i--;
					}
					else
					{
						j--;
					}
					break;
				case UP_RIGHT:
					direction = direction == Direction.GoingLeft ? Direction.GoingUp : Direction.GoingRight;

					if (direction == Direction.GoingUp)
					{
						i--;
					}
					else
					{
						j++;
					}
					break;
				case DOWN_RIGHT:
					direction = direction == Direction.GoingLeft ? Direction.GoingDown : Direction.GoingRight;

					if (direction == Direction.GoingDown)
					{
						i++;
					}
					else
					{
						j++;
					}
					break;
				case DOWN_LEFT:
					direction = direction == Direction.GoingRight ? Direction.GoingDown : Direction.GoingLeft;

					if (direction == Direction.GoingDown)
					{
						i++;
					}
					else
					{
						j--;
					}
					break;
			}

			foundStart = i == startPosRow && j == startPosColumn;
			currentChar = c[i][j];
		}
		field[GetIndex(startPosRow, startPosColumn, columns)] = startingPipe;
	}
}