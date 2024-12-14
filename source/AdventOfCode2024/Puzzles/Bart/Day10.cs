using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Bart;

public class Day10 : HappyPuzzleBase<int>
{
	const char Start = '0';

	public override int SolvePart1(Input input)
	{
		var sum = 0;

		for (var y = 0; y < input.Lines.Length; y++)
		{
			for (var x = 0; x < input.Lines[0].Length; x++)
			{
				if (input.Lines[y][x] == Start)
				{
					sum += GetTrailScore1(ref input, x, y);
				}
			}
		}

		return sum;
	}

	private static int GetTrailScore1(ref Input input, int startX, int startY)
	{
		scoped Span<(int, int)> positions = stackalloc (int, int)[200];
		positions[0] = (startX, startY);
		var toProcessAmount = 1;
		var nextNumber = '1';

		scoped Span<(int, int)> nextPositions = stackalloc (int, int)[200];

		while (toProcessAmount > 0 && nextNumber <= '9')
		{
			var nextAmount = 0;

			for (var i = 0; i < toProcessAmount; i++)
			{
				var (x, y) = positions[i];

				//up
				if (y >= 1 && input.Lines[y - 1][x] == nextNumber )
				{
					if (!Contains(ref nextPositions, nextAmount, (x, y - 1)))
					{
						nextPositions[nextAmount++] = (x, y - 1);
					}
				}

				//Down
				if (y < input.Lines.Length - 1 && input.Lines[y + 1][x] == nextNumber)
				{
					if (!Contains(ref nextPositions, nextAmount, (x, y + 1)))
					{
						nextPositions[nextAmount++] = (x, y + 1);
					}
				}

				//Left
				if (x >= 1 && input.Lines[y][x - 1] == nextNumber)
				{
					if (!Contains(ref nextPositions, nextAmount, (x -1, y)))
					{
						nextPositions[nextAmount++] = (x - 1, y);
					}
				}

				//right
				if (x < input.Lines[0].Length - 1 && input.Lines[y][x + 1] == nextNumber)
				{
					if (!Contains(ref nextPositions, nextAmount, (x + 1, y)))
					{
						nextPositions[nextAmount++] = (x + 1, y);
					}
				}
			}

			nextPositions.CopyTo(positions);
			toProcessAmount = nextAmount;

			nextNumber = (char)(nextNumber + 1);
		}

		if (nextNumber - 1 == '9')
		{
			return toProcessAmount;
		}

		return 0;
	}

	private static int GetTrailScore2(ref Input input, int startX, int startY)
	{
		scoped Span<(int, int)> positions = stackalloc (int, int)[200];
		positions[0] = (startX, startY);
		var toProcessAmount = 1;
		var nextNumber = '1';

		scoped Span<(int, int)> nextPositions = stackalloc (int, int)[200];

		while (toProcessAmount > 0 && nextNumber <= '9')
		{
			var nextAmount = 0;

			for (var i = 0; i < toProcessAmount; i++)
			{
				var (x, y) = positions[i];

				//up
				if (y >= 1 && input.Lines[y - 1][x] == nextNumber )
				{
					//if (!Contains(ref nextPositions, nextAmount, (x, y - 1)))
					//{
						nextPositions[nextAmount++] = (x, y - 1);
					//}
				}

				//Down
				if (y < input.Lines.Length - 1 && input.Lines[y + 1][x] == nextNumber)
				{
					//if (!Contains(ref nextPositions, nextAmount, (x, y + 1)))
					//{
						nextPositions[nextAmount++] = (x, y + 1);
					//}
				}

				//Left
				if (x >= 1 && input.Lines[y][x - 1] == nextNumber)
				{
					//if (!Contains(ref nextPositions, nextAmount, (x -1, y)))
					//{
						nextPositions[nextAmount++] = (x - 1, y);
					//}
				}

				//right
				if (x < input.Lines[0].Length - 1 && input.Lines[y][x + 1] == nextNumber)
				{
					//if (!Contains(ref nextPositions, nextAmount, (x + 1, y)))
					//{
						nextPositions[nextAmount++] = (x + 1, y);
					//}
				}
			}

			nextPositions.CopyTo(positions);
			toProcessAmount = nextAmount;

			nextNumber = (char)(nextNumber + 1);
		}

		if (nextNumber - 1 == '9')
		{
			return toProcessAmount;
		}

		return 0;
	}

	private static bool Contains(ref Span<(int,int)> span, int count, (int,int) value)
	{
		for (var i = 0; i < count; i++)
		{
			if (span[i].Equals(value))
			{
				return true;
			}
		}
		return false;
	}

	public override int SolvePart2(Input input)
	{
		var sum = 0;


		for (int y = 0; y < input.Lines.Length; y++)
		{
			for (int x = 0; x < input.Lines[0].Length; x++)
			{
				if (input.Lines[y][x] == Start)
				{
					sum += GetTrailScore2(ref input, x, y);
				}
			}
		}

		return sum;
	}
}