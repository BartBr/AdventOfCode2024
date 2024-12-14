using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Bart;

public class Day11 : HappyPuzzleBase<ulong>
{
	public override ulong SolvePart1(Input input)
	{
		//return (ulong) 1;
		ulong sum = 0;

		var readingNmbr = 0;
		for (var i = 0; i < input.Lines[0].Length; i++)
		{
			if(input.Lines[0][i] == ' ')
			{
				sum += CountNmbrsAfterBlinks((ulong)readingNmbr, 25);
				readingNmbr = 0;
			}
			else
			{
				readingNmbr = readingNmbr * 10 + (input.Lines[0][i] - '0');
			}
		}
		if (input.Lines[0][input.Lines[0].Length - 1] != ' ')
		{
			sum += CountNmbrsAfterBlinks((ulong)readingNmbr, 25);
		}

		return sum;
	}
	private static ulong CountNmbrsAfterBlinks(ulong startNmbr, int blinks)
	{
		Span<ulong> currentRow = stackalloc ulong[1];
		currentRow[0] = (ulong)startNmbr;

		for (var i = 0; i < blinks; i++)
		{
			Span<ulong> nextRow = stackalloc ulong[currentRow.Length * 2];
			var nextRowCount = 0;

			for (var j = 0; j < currentRow.Length; j++)
			{
				if (currentRow[j] == 0)
				{
					nextRow[nextRowCount++] = 1;
				}
				else
				{
					var digits = GetDigitCount(currentRow[j]);
					if (digits % 2 == 0)
					{
						SplitNumberInTwo(currentRow[j], digits, out var nmbr1, out var nmbr2);
						nextRow[nextRowCount++] = nmbr1;
						nextRow[nextRowCount++] = nmbr2;
					}
					else
					{
						nextRow[nextRowCount++] = currentRow[j] * 2024;
					}
				}
			}

			currentRow = nextRow[..nextRowCount];
		}
		return (ulong)currentRow.Length;
	}
	private static void SplitNumberInTwo(ulong number, int digits, out ulong nmbr1, out ulong nmbr2)
	{
		var half = digits / 2;
		ulong power = 1;

		// Compute 10^(half) using a loop instead of Math.Pow
		for (var i = 0; i < half; i++)
		{
			power *= 10;
		}

		nmbr1 = number / power;
		nmbr2 = number % power;
	}
	private static int GetDigitCount(ulong number)
	{
		var count = 0;
		while (number > 0)
		{
			number /= 10;
			count++;
		}

		return count;
	}
	public override ulong SolvePart2(Input input)
	{
		throw new NotImplementedException();
		ulong sum = 0;

		var readingNmbr = 0;
		for (var i = 0; i < input.Lines[0].Length; i++)
		{
			if(input.Lines[0][i] == ' ')
			{
				sum += CountNmbrsAfterBlinks((ulong)readingNmbr, 75);
				readingNmbr = 0;
			}
			else
			{
				readingNmbr = readingNmbr * 10 + (input.Lines[0][i] - '0');
			}
		}
		if (input.Lines[0][input.Lines[0].Length - 1] != ' ')
		{
			sum += CountNmbrsAfterBlinks((ulong)readingNmbr, 75);
		}

		return sum;
	}
}