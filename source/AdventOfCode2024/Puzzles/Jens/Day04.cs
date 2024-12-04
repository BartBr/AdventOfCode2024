using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jens;

public class Day04 : HappyPuzzleBase<int>
{
	public override int SolvePart1(Input input)
	{
		var gridWidth = input.Lines[0].Length + 1;
		var gridHeight = input.Lines.Length;

		var inputSpan = input.Text.AsSpan();

		var xmasCount = 0;

		for (var xSpanIndex = 0; xSpanIndex < inputSpan.Length; xSpanIndex++)
		{
			if (inputSpan[xSpanIndex] != 'X')
			{
				continue;
			}

			var x = xSpanIndex % gridWidth;
			var y = xSpanIndex / gridWidth;

			// Check left-up
			if (x >= 3 && y >= 3)
			{
				Part1_Validate(
					ref inputSpan,
					xSpanIndex - gridWidth - 1,
					xSpanIndex - gridWidth - gridWidth - 2,
					xSpanIndex - gridWidth - gridWidth - gridWidth - 3,
					ref xmasCount);
			}

			// Check center-up
			if (y >= 3)
			{
				Part1_Validate(
					ref inputSpan,
					xSpanIndex - gridWidth,
					xSpanIndex - gridWidth - gridWidth,
					xSpanIndex - gridWidth - gridWidth - gridWidth,
					ref xmasCount);
			}

			// Check right-up
			// -4 to account for \n at the end of each virtual grid row
			if (x < gridWidth - 4 && y >= 3)
			{
				Part1_Validate(
					ref inputSpan,
					xSpanIndex - gridWidth + 1,
					xSpanIndex - gridWidth - gridWidth + 2,
					xSpanIndex - gridWidth - gridWidth - gridWidth + 3,
					ref xmasCount);
			}

			// Check left
			if (x >= 3 && inputSpan.Slice(xSpanIndex - 3, 3).Equals("SAM", StringComparison.Ordinal))
			{
				xmasCount++;
			}

			// Check right
			// -4 to account for \n at the end of each virtual grid row
			if (x < gridWidth - 4 && inputSpan.Slice(xSpanIndex + 1, 3).Equals("MAS", StringComparison.Ordinal))
			{
				xmasCount++;
			}

			// Check left-down
			if (x >= 3 && y < gridHeight - 3)
			{
				Part1_Validate(
					ref inputSpan,
					xSpanIndex + gridWidth - 1,
					xSpanIndex + gridWidth + gridWidth - 2,
					xSpanIndex + gridWidth + gridWidth + gridWidth - 3,
					ref xmasCount);
			}

			// Check center-down
			if (y < gridHeight - 3)
			{
				Part1_Validate(
					ref inputSpan,
					xSpanIndex + gridWidth,
					xSpanIndex + gridWidth + gridWidth,
					xSpanIndex + gridWidth + gridWidth + gridWidth,
					ref xmasCount);
			}

			// Check right-down
			// -4 to account for \n at the end of each virtual grid row
			if (x < gridWidth - 4 && y < gridHeight - 3)
			{
				Part1_Validate(
					ref inputSpan,
					xSpanIndex + gridWidth + 1,
					xSpanIndex + gridWidth + gridWidth + 2,
					xSpanIndex + gridWidth + gridWidth + gridWidth + 3,
					ref xmasCount);
			}
		}

		return xmasCount;
	}

	private static void Part1_Validate(ref ReadOnlySpan<char> inputSpan, int mIndex, int aIndex, int sIndex, ref int xmasCount)
	{
		if (inputSpan[mIndex] == 'M' && inputSpan[aIndex] == 'A' && inputSpan[sIndex] == 'S')
		{
			xmasCount++;
		}
	}

	public override int SolvePart2(Input input)
	{
		var gridWidth = input.Lines[0].Length + 1;

		var inputSpan = input.Text.AsSpan();

		var xmasCount = 0;

		var xSkipThreshold = gridWidth - 3;

		for (var xSpanIndex = gridWidth + 1; xSpanIndex < inputSpan.Length - gridWidth - 2; xSpanIndex++)
		{
			var x = xSpanIndex % gridWidth;
			if (x > xSkipThreshold)
			{
				xSpanIndex += 2;
				continue;
			}

			if (inputSpan[xSpanIndex] != 'A')
			{
				continue;
			}

			Part2_Validate(
				inputSpan[xSpanIndex - gridWidth - 1],
				inputSpan[xSpanIndex - gridWidth + 1],
				inputSpan[xSpanIndex + gridWidth - 1],
				inputSpan[xSpanIndex + gridWidth + 1],
				ref xmasCount);
		}

		return xmasCount;
	}

	private static void Part2_Validate(char topLeft, char topRight, char bottomLeft, char bottomRight, ref int xmasCount)
	{
		if ((topLeft == 'M' && bottomRight == 'S' || topLeft == 'S' && bottomRight == 'M') &&
		    (topRight == 'M' && bottomLeft == 'S' || topRight == 'S' && bottomLeft == 'M'))
		{
			xmasCount++;
		}
	}
}