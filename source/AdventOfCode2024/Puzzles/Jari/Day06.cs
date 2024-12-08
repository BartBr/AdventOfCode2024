using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jari;

public class Day06 : HappyPuzzleBase<int>
{
	public override int SolvePart1(Input input)
	{
		int x = 0, y = 0;
		int xAdd = 0, yAdd = -1;
		int totalDistinctPositions = 0, tmp;
		int flip = -1;
		int height = input.Lines.Length;
		int width = input.Lines[0].Length;
		scoped Span<bool> path = stackalloc bool[height * width];

		FindStartingPosition(input.Lines, ref x, ref y);

		while (x < width && x >= 0 && y < height && y >= 0)
		{
			if (y + yAdd >= height || y + yAdd < 0 || x + xAdd >= width || x + xAdd < 0)
			{
				break;
			}

			if (!path[y + x * width])
			{
				totalDistinctPositions++;
				path[y + x * width] = true;
			}

			if (input.Lines[y + yAdd][x + xAdd] == '#')
			{
				tmp = yAdd;
				yAdd = xAdd * flip;
				xAdd = tmp * flip;
				flip *= -1;
			}

			x += xAdd;
			y += yAdd;
		}

		return ++totalDistinctPositions;
	}

	/*
	 * right = (xAdd = +1, yAdd = 0)
	 * down  = (xAdd = 0,  yAdd = +1)
	 * left  = (xAdd = -1, yAdd = 0)
	 * up    = (xAdd = 0,  yAdd = -1)
	 */

	public void FindStartingPosition(string[] lines, ref int x, ref int y)
	{
		int height = lines.Length;
		int width = lines[0].Length;
		for (y = 0; y < height; y++)
		{
			for (x = 0; x < width; x++)
			{
				if (lines[y][x] == '^')
				{
					return;
				}
			}
		}
	}

	public override int SolvePart2(Input input)
	{
		throw new NotImplementedException();
	}
}