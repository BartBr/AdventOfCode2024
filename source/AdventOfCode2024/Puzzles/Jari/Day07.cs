using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jari;

public class Day07 : HappyPuzzleBase<long>
{
	public override long SolvePart1(Input input)
	{
		long sum = 0L;
		scoped Span<long> numbers = stackalloc long[20];

		for (int l = 0; l < input.Lines.Length; l++)
		{
			ProcessEquation(input.Lines[l], numbers, ref sum);
		}

		return sum;
	}

	private void ProcessEquation(string line, Span<long> numbers, ref long sum)
	{
		var i = 0;
		int equationSize = 0;
		long number = 0;
		long expectedResult = 0;

		while (line[i] != ':')
		{
			expectedResult = expectedResult * 10 + (line[i] - '0');
			i++;
		}

		i += 2;
		for (; i < line.Length; i++)
		{
			char c = line[i];
			if (c == ' ')
			{
				numbers[equationSize] = number;
				equationSize++;
				number = 0L;
			}
			else
			{
				number = number * 10L + (c - '0');
			}
		}

		numbers[equationSize] = number;

		if (Calc(numbers[0], expectedResult, numbers.Slice(1, equationSize), 0))
		{
			sum += expectedResult;
		}
	}

	private bool Calc(long value, long expectedResult, ReadOnlySpan<long> numbers, int pos)
	{
		if (value > expectedResult)
		{
			return false;
		}

		if (pos == numbers.Length)
		{
			return value == expectedResult;
		}

		return Calc(value * numbers[pos], expectedResult, numbers, pos + 1)
		       || Calc(value + numbers[pos], expectedResult, numbers, pos + 1);
	}


	public override long SolvePart2(Input input)
	{
		throw new NotImplementedException();
	}
}