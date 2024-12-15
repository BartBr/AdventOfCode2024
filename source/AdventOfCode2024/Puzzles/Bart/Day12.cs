using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Bart;

public class Day12 : HappyPuzzleBase<int>
{
	public override int SolvePart1(Input input)
	{
		var rows = input.Lines.Length;
		var columns = input.Lines[0].Length;
		scoped Span<bool> visited = stackalloc bool[rows * columns];
		var sum = 0;

		for (var y = 0; y < rows; y++)
		{
			for (var x = 0; x < columns; x++)
			{
				if(visited[y * columns + x])
				{
					continue;
				}
				var cellScore = VisitCoordinate_Part1(ref input, x, y, ref visited);
				sum += cellScore;
			}
		}

		return sum;
	}

	private static int VisitCoordinate_Part1(ref Input input, int startX, int startY, ref Span<bool> visited)
	{
		var fences = 0;
		var tiles = 0;
		var current = input.Lines[startY][startX];

		scoped Span<(int x, int y)> toVisit = stackalloc (int x, int y)[input.Lines.Length * input.Lines[0].Length * 2];
		toVisit[0] = (startX, startY);
		var toVisitLength = 1;
		var toVisitIndex = 0;

		while (toVisitIndex <= toVisitLength)
		{
			var x = toVisit[toVisitIndex].x;
			var y = toVisit[toVisitIndex].y;
			toVisitIndex++;

			if(visited[y * input.Lines[0].Length + x])
			{
				continue;
			}
			visited[y * input.Lines[0].Length + x] = true;

			tiles++;

			//Up
			if (y <= 0)
			{
				fences++;
			}
			else if (input.Lines[y - 1][x] == current)
			{
				toVisit[toVisitLength++] = (x, y - 1);
			}
			else
			{
				fences++;
			}



			//Down
			if (y >= input.Lines.Length - 1)
			{
				fences++;
			}
			else if(input.Lines[y + 1][x] == current)
			{
				toVisit[toVisitLength++] = (x, y + 1);
			}
			else
			{
				fences++;
			}

			//Left
			if (x <= 0)
			{
				fences++;
			}
			else if (input.Lines[y][x - 1] == current)
			{
				toVisit[toVisitLength++] = (x - 1, y);
			}
			else
			{
				fences++;
			}

			//Right
			if (x >= input.Lines[0].Length - 1)
			{
				fences++;
			}
			else if (input.Lines[y][x + 1] == current)
			{
				if(visited[y * input.Lines[0].Length + x + 1])
				{
					continue;
				}
				toVisit[toVisitLength++] = (x + 1, y);
			}
			else
			{
				fences++;
			}
		}

		return fences * tiles;
	}

	public override int SolvePart2(Input input)
	{
		var rows = input.Lines.Length;
		var columns = input.Lines[0].Length;
		scoped Span<bool> visited = stackalloc bool[rows * columns];
		var sum = 0;

		for (var y = 0; y < rows; y++)
		{
			for (var x = 0; x < columns; x++)
			{
				if(visited[y * columns + x])
				{
					continue;
				}
				var cellScore = VisitCoordinate_Part2(ref input, x, y, ref visited);
				sum += cellScore;
			}
		}

		return sum;
	}

	[Flags]
	private enum Fences
	{
		ABOVE = 1,
		LEFT = 2,
		RIGHT = 4,
		BELOW = 8
	}

	private static int VisitCoordinate_Part2(ref Input input, int startX, int startY, ref Span<bool> visited)
	{
		var cols = input.Lines[0].Length;
		var tiles = 0;
		scoped Span<Fences> fences = stackalloc Fences[input.Lines.Length * cols];

		var current = input.Lines[startY][startX];

		scoped Span<(int x, int y)> toVisit = stackalloc (int x, int y)[input.Lines.Length * cols * 2];
		toVisit[0] = (startX, startY);
		var toVisitLength = 1;
		var toVisitIndex = 0;

		while (toVisitIndex <= toVisitLength)
		{
			var x = toVisit[toVisitIndex].x;
			var y = toVisit[toVisitIndex].y;
			toVisitIndex++;

			if(visited[y * cols + x])
			{
				continue;
			}
			visited[y * cols + x] = true;

			tiles++;

			//Up
			if (y <= 0)
			{
				fences[y * cols + x] |= Fences.ABOVE;
			}
			else if (input.Lines[y - 1][x] == current)
			{
				toVisit[toVisitLength++] = (x, y - 1);
			}
			else
			{
				fences[y * cols + x] |= Fences.ABOVE;
			}

			//Down
			if (y >= input.Lines.Length - 1)
			{
				fences[y * cols + x] |= Fences.BELOW;
			}
			else if(input.Lines[y + 1][x] == current)
			{
				toVisit[toVisitLength++] = (x, y + 1);
			}
			else
			{
				fences[y * cols + x] |= Fences.BELOW;
			}

			//Left
			if (x <= 0)
			{
				fences[y * cols + x] |= Fences.LEFT;
			}
			else if (input.Lines[y][x - 1] == current)
			{
				toVisit[toVisitLength++] = (x - 1, y);
			}
			else
			{
				fences[y * cols + x] |= Fences.LEFT;
			}

			//Right
			if (x >= cols - 1)
			{
				fences[y * cols + x] |= Fences.RIGHT;
			}
			else if (input.Lines[y][x + 1] == current)
			{
				if(visited[y * cols + x + 1])
				{
					continue;
				}
				toVisit[toVisitLength++] = (x + 1, y);
			}
			else
			{
				fences[y * cols + x] |= Fences.RIGHT;
			}
		}

		var uniqueFences = CountUniqueFences(ref fences, cols, rows: input.Lines.Length);
		return uniqueFences * tiles;
	}

	private static int CountUniqueFences(ref Span<Fences> fences, int cols, int rows)
	{
		var uniqueFences = 0;

		//Horizontals
		for (var y = 0; y < rows; y++)
		{
			var previousAbove = false;
			var previousBelow = false;
			for (var x = 0; x < cols; x++)
			{
				if(fences[y * cols + x].HasFlag(Fences.ABOVE))
				{
					if(!previousAbove)
					{
						uniqueFences++;
						previousAbove = true;
					}
				}
				else
				{
					previousAbove = false;
				}

				if(fences[y * cols + x].HasFlag(Fences.BELOW))
				{
					if(!previousBelow)
					{
						uniqueFences++;
						previousBelow = true;
					}
				}
				else
				{
					previousBelow = false;
				}
			}
		}

		//Verticals
		for (var x = 0; x < cols; x++)
		{
			var previousLeft = false;
			var previousRight = false;
			for (var y = 0; y < rows; y++)
			{
				if(fences[y * cols + x].HasFlag(Fences.LEFT))
				{
					if(!previousLeft)
					{
						uniqueFences++;
						previousLeft = true;
					}
				}
				else
				{
					previousLeft = false;
				}

				if(fences[y * cols + x].HasFlag(Fences.RIGHT))
				{
					if(!previousRight)
					{
						uniqueFences++;
						previousRight = true;
					}
				}
				else
				{
					previousRight = false;
				}
			}
		}
		return uniqueFences;
	}
}