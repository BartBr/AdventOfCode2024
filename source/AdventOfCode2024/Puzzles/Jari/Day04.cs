using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jari;

public class Day04 : HappyPuzzleBase<int>
{
	// XMAS
	public override int SolvePart1(Input input)
	{
		int found = 0;
		int height = input.Lines.Length;
		int width = input.Lines[0].Length;

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				if (x + 3 < width && input.Lines[y][x] == 'X' && input.Lines[y][x + 1] == 'M' && input.Lines[y][x + 2] == 'A' && input.Lines[y][x + 3] == 'S')
				{
					found++;
				}

				if (x - 3 >= 0 && input.Lines[y][x] == 'X' && input.Lines[y][x - 1] == 'M' && input.Lines[y][x - 2] == 'A' && input.Lines[y][x - 3] == 'S')
				{
					found++;
				}

				if (y + 3 < height && input.Lines[y][x] == 'X' && input.Lines[y + 1][x] == 'M' && input.Lines[y + 2][x] == 'A' && input.Lines[y + 3][x] == 'S')
				{
					found++;
				}

				if (y - 3 >= 0 && input.Lines[y][x] == 'X' && input.Lines[y - 1][x] == 'M' && input.Lines[y - 2][x] == 'A' && input.Lines[y - 3][x] == 'S')
				{
					found++;
				}

				// ==== DIAGONAL ====
				if (x + 3 < width && y + 3 < height && input.Lines[y][x] == 'X' && input.Lines[y + 1][x + 1] == 'M' && input.Lines[y + 2][x + 2] == 'A' && input.Lines[y + 3][x + 3] == 'S')
				{
					found++;
				}

				if (x - 3 >= 0 && y - 3 >= 0 && input.Lines[y][x] == 'X' && input.Lines[y - 1][x - 1] == 'M' && input.Lines[y - 2][x - 2] == 'A' && input.Lines[y - 3][x - 3] == 'S')
				{
					found++;
				}

				if (x - 3 >= 0 && y + 3 < height && input.Lines[y][x] == 'X' && input.Lines[y + 1][x - 1] == 'M' && input.Lines[y + 2][x - 2] == 'A' && input.Lines[y + 3][x - 3] == 'S')
				{
					found++;
				}

				if (x + 3 < width && y - 3 >= 0 && input.Lines[y][x] == 'X' && input.Lines[y - 1][x + 1] == 'M' && input.Lines[y - 2][x + 2] == 'A' && input.Lines[y - 3][x + 3] == 'S')
				{
					found++;
				}
			}
		}


		return found;
	}

	public override int SolvePart2(Input input)
	{
		return -1;
	}
}