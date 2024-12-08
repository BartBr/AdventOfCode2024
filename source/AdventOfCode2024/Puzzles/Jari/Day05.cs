using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jari;

public class Day05 : HappyPuzzleBase<int>
{
	public override int SolvePart1(Input input)
	{
		scoped Span<bool> rules = stackalloc bool[10_000];
		int sum = 0;
		int lineCounter = 0;

		while (input.Lines[lineCounter].Length != 0)
		{
			var index = ((input.Lines[lineCounter][0] - '0') * 1_000) + ((input.Lines[lineCounter][1] - '0') * 100) + ((input.Lines[lineCounter][3] - '0') * 10) + (input.Lines[lineCounter][4] - '0');
			rules[index] = true;

			lineCounter++;
		}

		for (lineCounter += 1; lineCounter < input.Lines.Length; lineCounter++)
		{
			IsCorrectlyOrdered(input.Lines[lineCounter], rules, ref sum);
		}

		return sum;
	}

	private void IsCorrectlyOrdered(string updates, Span<bool> rules, ref int sum)
	{
		for (int i = 0; i < updates.Length; i += 3)
		{
			int x = (((updates[i] - '0') * 10) + (updates[i + 1] - '0'));
			for (int j = i + 3; j < updates.Length; j += 3)
			{
				// if rule exists for opposite key then updates are out of order
				int index = ((((updates[j] - '0') * 10) + (updates[j + 1] - '0')) * 100) + x;
				if (rules[index])
				{
					return;
				}
			}
		}

		int middleIndex = updates.Length / 2 - 1;
		sum += ((updates[middleIndex] - '0') * 10) + (updates[middleIndex + 1] - '0');
	}

	public override int SolvePart2(Input input)
	{
		scoped Span<bool> rules = stackalloc bool[10_000];
		scoped Span<int> workTable = stackalloc int[50];
		int sum = 0;
		int lineCounter = 0;

		while (input.Lines[lineCounter].Length != 0)
		{
			var index = ((input.Lines[lineCounter][0] - '0') * 1_000) + ((input.Lines[lineCounter][1] - '0') * 100) + ((input.Lines[lineCounter][3] - '0') * 10) + (input.Lines[lineCounter][4] - '0');
			rules[index] = true;

			lineCounter++;
		}

		for (lineCounter += 1; lineCounter < input.Lines.Length; lineCounter++)
		{
			IsCorrectlyOrdered_P2(input.Lines[lineCounter], rules, ref sum, ref workTable);
		}

		return sum;
	}

	private void IsCorrectlyOrdered_P2(string updates, Span<bool> rules, ref int sum, ref Span<int> workTable)
	{
		int tmp;
		bool incorrect = false;
		int pagesNumbers = updates.Length / 3 + 1;
		int i, j, m;

		for (m = 0; m < pagesNumbers; m++)
		{
			workTable[m] = (((updates[m * 3] - '0') * 10) + (updates[m * 3 + 1] - '0'));
		}

		for (i = 0; i < pagesNumbers - 1; i++)
		{
			for (j = i + 1; j < pagesNumbers; j++)
			{
				if (rules[workTable[j] * 100 + workTable[i]])
				{
					tmp = workTable[j];
					workTable[j] = workTable[i];
					workTable[i] = tmp;
					incorrect = true;
					j = i;
				}
			}
		}

		if (incorrect)
		{
			sum += workTable[pagesNumbers / 2];
		}
	}
}