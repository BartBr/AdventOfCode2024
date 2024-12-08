using System.Buffers;
using System.Text.RegularExpressions;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Bart;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public class Day06 : HappyPuzzleBase<int>
{
	private const char Up = '^';
	private const char Down = 'v';
	private const char Left = '<';
	private const char Right = '>';

	public override int SolvePart1(Input input)
	{
		var rows = input.Lines[0].Length;
		var columns = input.Lines.Length;

		scoped Span<bool> discoveredPositions = stackalloc bool[rows * columns];

		scoped Span<char> nextDirection = stackalloc char[120];
		nextDirection[Up] = Right;
		nextDirection[Right] = Down;
		nextDirection[Down] = Left;
		nextDirection[Left] = Up;

		var searchVal = SearchValues.Create("^v><");
		scoped Span<char> inputSpan = stackalloc char[rows * columns];
		for (var i = 0; i < rows; i++)
		{
			for(var j = 0; j< columns; j++)
			{
				inputSpan[i * columns + j] = input.Lines[i][j];
			}
		}
		var index = inputSpan.IndexOfAny(searchVal);

		var posX = index % rows;
		var posY = index / rows;
		var direction = input.Lines[posY][posX];
		discoveredPositions[posX + posY * columns] = true;

		while (posX >= 0 && posX < columns && posY >= 0 && posY < rows)
		{
			switch (direction)
			{
				case Up:
					GoUp(ref posX, ref posY, ref discoveredPositions, input.Lines, ref columns);
					break;
				case Down:
					GoDown(ref posX, ref posY, ref discoveredPositions, input.Lines, ref columns);
					break;
				case Left:
					GoLeft(ref posX, ref posY, ref discoveredPositions, input.Lines, ref columns);
					break;
				case Right:
					GoRight(ref posX, ref posY, ref discoveredPositions, input.Lines, ref columns);
					break;
			}

			direction = nextDirection[direction];
		}

		var sum = 0;
		for (var i = 0; i < discoveredPositions.Length; i++)
		{
			if (discoveredPositions[i])
			{
				sum++;
			}
		}
		return sum;
	}

	private static void GoUp(ref int posX, ref int posY, ref Span<bool> discoveredPositions, string[] inputLines, ref int columns)
	{
		while (posY >= 0)
		{
			discoveredPositions[posX + posY * columns] = true;

			if (posY >= 1 && inputLines[posY - 1][posX] == '#')
			{
				return;
			}

			posY--;
		}
	}

	private static void GoDown(ref int posX, ref int posY, ref Span<bool> discoveredPositions, string[] inputLines, ref int columns)
	{
		while (posY < inputLines.Length)
		{
			discoveredPositions[posX + posY * columns] = true;

			if (posY < inputLines.Length-1 && inputLines[posY + 1][posX] == '#')
			{
				return;
			}

			posY++;
		}
	}

	private static void GoLeft(ref int posX, ref int posY, ref Span<bool> discoveredPositions, string[] inputLines, ref int columns)
	{
		while (posX >= 0)
		{
			discoveredPositions[posX + posY * columns] = true;

			if (posX >= 1 && inputLines[posY][posX-1] == '#')
			{
				return;
			}

			posX--;
		}
	}
	private static void GoRight(ref int posX, ref int posY, ref Span<bool> discoveredPositions, string[] inputLines, ref int columns)
	{
		while (posX < columns)
		{
			discoveredPositions[posX + posY * columns] = true;

			if (posX < columns-1 && inputLines[posY ][posX + 1] == '#')
			{
				return;
			}

			posX++;
		}
	}


	public override int SolvePart2(Input input)
	{
		// same as part 1, to find the path of the security guard.
		// On that path we will put new blocks, and permution over those.
		// except for the guards starting position
		var rows = input.Lines[0].Length;
		var columns = input.Lines.Length;

		scoped Span<bool> discoveredPositions = stackalloc bool[rows * columns];

		scoped Span<char> nextDirection = stackalloc char[120];
		nextDirection[Up] = Right;
		nextDirection[Right] = Down;
		nextDirection[Down] = Left;
		nextDirection[Left] = Up;

		var searchVal = SearchValues.Create("^v><");
		scoped Span<char> inputSpan = stackalloc char[rows * columns];
		for (var i = 0; i < rows; i++)
		{
			for(var j = 0; j< columns; j++)
			{
				inputSpan[i * columns + j] = input.Lines[i][j];
			}
		}
		var index = inputSpan.IndexOfAny(searchVal);

		var posX = index % rows;
		var posY = index / rows;
		var startX = posX;
		var startY = posY;

		var direction = input.Lines[posY][posX];
		discoveredPositions[posX + posY * columns] = true;

		while (posX >= 0 && posX < columns && posY >= 0 && posY < rows)
		{
			switch (direction)
			{
				case Up:
					GoUp(ref posX, ref posY, ref discoveredPositions, input.Lines, ref columns);
					break;
				case Down:
					GoDown(ref posX, ref posY, ref discoveredPositions, input.Lines, ref columns);
					break;
				case Left:
					GoLeft(ref posX, ref posY, ref discoveredPositions, input.Lines, ref columns);
					break;
				case Right:
					GoRight(ref posX, ref posY, ref discoveredPositions, input.Lines, ref columns);
					break;
			}

			direction = nextDirection[direction];
		}
		discoveredPositions[startX + startY * columns] = false;

		scoped Span<bool> visitedUps = stackalloc bool[columns * rows];
		scoped Span<bool> visitedDowns = stackalloc bool[columns * rows];
		scoped Span<bool> visitedLefts = stackalloc bool[columns * rows];
		scoped Span<bool> visitedRights = stackalloc bool[columns * rows];

		var loopedPermutations = 0;
		var startDirection = input.Lines[startY][startX];
		// Permutations on all path
		for (var x =0;x  < columns; x++)
		{
			for(var y=0; y < rows; y++)
			{
				if (discoveredPositions[x + y * columns] && (x != startX || y != startY))
				{
					inputSpan[x + y * columns] = '#';

					for (var i = 0; i < rows * columns; i++)
					{
						visitedUps[i] = false;
						visitedDowns[i] = false;
						visitedLefts[i] = false;
						visitedRights[i] = false;
					}
					loopedPermutations += CheckIfLooped(ref inputSpan, columns, rows, startX, startY, startDirection, ref nextDirection, ref visitedUps, ref visitedDowns, ref visitedLefts, ref visitedRights);


					inputSpan[x + y * columns] = '.';
				}

			}
		}
		return loopedPermutations;
	}

	private static int CheckIfLooped(ref Span<char> inputSpan, int columns, int rows, int startX, int startY, char startDirection, ref Span<char> nextDirection, ref Span<bool> visitedUps, ref Span<bool> visitedDowns, ref Span<bool> visitedLefts, ref Span<bool> visitedRights)
	{
		var posX = startX;
		var posY = startY;
		var direction = startDirection;

		bool inALoop = false;
		while (posX >= 0 && posX < columns && posY >= 0 && posY < rows)
		{

			switch (direction)
			{
				case Up:
					inALoop = GoUp2(ref posX, ref posY, ref inputSpan, ref visitedUps, ref columns, ref rows);
					break;
				case Down:
					inALoop = GoDown2(ref posX, ref posY, ref inputSpan, ref visitedDowns, ref columns, ref rows);
					break;
				case Left:
					inALoop = GoLeft2(ref posX, ref posY, ref inputSpan, ref visitedLefts, ref columns, ref rows);
					break;
				case Right:
					inALoop = GoRight2(ref posX, ref posY, ref inputSpan, ref visitedRights, ref columns, ref rows);
                          					break;
			}

			if(inALoop)
			{
				return 1;
			}

			direction = nextDirection[direction];
		}

		return 0;
	}

	private static bool GoUp2(ref int posX, ref int posY, ref Span<char> inputSpan, ref Span<bool> visitedUps, ref int columns, ref int rows)
	{
		while (posY >= 0)
		{
			if(visitedUps[posX + posY * columns])
			{
				return true;
			}
			visitedUps[posX + posY * columns] = true;

			if (posY >= 1 && inputSpan[(posY - 1) * columns + posX] == '#')
			{
				return false;
			}

			posY--;
		}

		return false;
	}

	private static bool GoDown2(ref int posX, ref int posY, ref Span<char> inputSpan, ref Span<bool> visitedDowns, ref int columns, ref int rows)
	{
		while (posY < rows)
		{
			if(visitedDowns[posX + (posY * columns)])
			{
				return true;
			}
			visitedDowns[posX + (posY * columns)] = true;

			if (posY < rows-1 && inputSpan[(posY + 1) * columns + posX] == '#')
			{
				return false;
			}

			posY++;
		}

		return false;
	}

	private static bool GoLeft2(ref int posX, ref int posY, ref Span<char> inputSpan, ref Span<bool> visitedLefts, ref int columns, ref int rows)
	{
		while (posX >= 0)
		{
			if(visitedLefts[posX + (posY * columns)])
			{
				return true;
			}
			visitedLefts[posX + (posY * columns)] = true;

			if (posX >= 1 && inputSpan[(posY * columns) + posX-1] == '#')
			{
				return false;
			}

			posX--;
		}

		return false;
	}

	private static bool GoRight2(ref int posX, ref int posY, ref Span<char> inputSpan, ref Span<bool> visitedRights, ref int columns, ref int rows)
	{
		while (posX < columns)
		{
			if(visitedRights[posX + (posY * columns)])
			{
				return true;
			}
			visitedRights[posX + posY * columns] = true;

			if (posX < columns-1 && inputSpan[posY * columns + posX + 1] == '#')
			{
				return false;
			}

			posX++;
		}

		return false;
	}
}