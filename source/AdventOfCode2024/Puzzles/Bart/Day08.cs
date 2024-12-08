using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Bart;

public class Day08 : HappyPuzzleBase<int>
{
	private const int MaxCharacters = 4;
	private static readonly char[] AllCharacters = ['0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'];

	public override int SolvePart1(Input input)
	{
		var size = input.Lines.Length;
		scoped Span<int> amount = stackalloc int[size * size];
		scoped Span<(int x, int y)> coordinates = stackalloc (int x, int y)[size * size * MaxCharacters];
		scoped Span<bool> board = stackalloc bool[size * size];

		//read all coordinates, and map in
		ReadCoordinates(input, coordinates, amount,size);

		foreach (var t in AllCharacters)
		{
			var index = IndexForChar(t);
			var amountForChar = amount[index];

			for (var j = 0; j < amountForChar-1; j++)
			{
				var (x1, y1) = coordinates[index * MaxCharacters + j];

				for (var k = j+1; k < amountForChar; k++)
				{
					var (x2, y2) = coordinates[index * MaxCharacters + k];

					var xDiff = x2 - x1;
					var yDiff = y2 - y1;

					// Create new coordinates extending away in opposite directions
					var x3 = x1 - xDiff;  // opposite direction from point1
					var y3 = y1 - yDiff;
					if(!IsPointNotOnBoard(x3, y3, size))
					{
						FillAntiNode(x3, y3,ref board, size);
					}


					var x4 = x2 + xDiff;  // same direction from point2
					var y4 = y2 + yDiff;
					if(!IsPointNotOnBoard(x4, y4, size))
					{
						FillAntiNode(x4, y4,ref board, size);
					}
				}
			}
		}

		var sum = 0;
		for (var i = 0; i < size*size; i++)
		{
			if (board[i])
			{
				sum++;
			}
		}

		return sum;
	}

	private static void FillAntiNode(int antiX1, int antiY1, ref Span<bool> board, int size)
	{
		var index = antiY1 * size + antiX1;
		board[index] = true;
	}

	private static bool IsPointNotOnBoard(int antiX1, int antiY1, int size)
	{
		return antiX1 < 0 || antiX1 >= size || antiY1 < 0 || antiY1 >= size;
	}

	private static void ReadCoordinates(Input input, Span<(int x, int y)> coordinates, Span<int> amount, int size)
	{
		for (var y = 0; y < size; y++)
		{
			for (var x = 0; x < size; x++)
			{
				if (input.Lines[y][x] != '.')
				{
					var index = IndexForChar(input.Lines[y][x]);

					coordinates[index * MaxCharacters + amount[index]] = (x, y);
					amount[index]++;
				}
			}
		}
	}

	public override int SolvePart2(Input input)
	{
		var size = input.Lines.Length;
		scoped Span<int> amount = stackalloc int[size * size];
		scoped Span<(int x, int y)> coordinates = stackalloc (int x, int y)[size * size * MaxCharacters];
		scoped Span<bool> board = stackalloc bool[size * size];

		//read all coordinates, and map in
		ReadCoordinates(input, coordinates, amount,size);

		foreach (var c in AllCharacters)
		{
			var index = IndexForChar(c);
			var amountForChar = amount[index];

			for (var j = 0; j < amountForChar-1; j++)
			{
				var (x1, y1) = coordinates[index * MaxCharacters + j];

				for (var k = j+1; k < amountForChar; k++)
				{
					var (x2, y2) = coordinates[index * MaxCharacters + k];

					var xDiff = x2 - x1;
					var yDiff = y2 - y1;

					// Create new coordinates extending away in opposite directions
					var x3 = x1 - xDiff;  // opposite direction from point1
					var y3 = y1 - yDiff;
					while (!IsPointNotOnBoard(x3, y3, size))
					{
						FillAntiNode(x3, y3,ref board, size);
						x3 -= xDiff;  // opposite direction from point1
						y3 -= yDiff;
					}



					var x4 = x2 + xDiff;  // same direction from point2
					var y4 = y2 + yDiff;
					while (!IsPointNotOnBoard(x4, y4, size))
					{
						FillAntiNode(x4, y4,ref board, size);
						x4 += xDiff;  // same direction from point2
						y4 += yDiff;
					}

					FillAntiNode(x2, y2,ref board, size);
					FillAntiNode(x1, y1,ref board, size);
				}
			}
		}

		var sum = 0;
		for (var i = 0; i < size*size; i++)
		{
			if (board[i])
			{
				sum++;
			}
		}

		return sum;
	}

	private static int IndexForChar(char c)
	{
		return c switch
		{
			>= '0' and <= '9' => (c - '0'),
			>= 'A' and <= 'Z' => (10 + c - 'A'),
			>= 'a' and <= 'z' => (10 + 26 + c - 'a'),
			_ => 0
		};
	}
}