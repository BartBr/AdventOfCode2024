using System.Buffers;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Bart;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public class Day07 : HappyPuzzleBase<ulong>
{
	public override ulong SolvePart1(Input input)
	{
		ulong sum = 0;

		scoped Span<ulong> numbers = stackalloc ulong[15];

		for (var i = 0; i < input.Lines.Length; i++)
		{
			var j = 0;
			ulong total = 0;
			while (input.Lines[i][j] != ':')
			{
				var newNumber = (ulong)(input.Lines[i][j] - '0');
				total = (total * (ulong)(10)) + newNumber;
				j++;
			}

			j = j + 2;//skip the ": "


			int amountOfNumbers = 0;
			ulong number = 0;
			for (; j < input.Lines[i].Length; j++)
			{
				if(input.Lines[i][j] == ' ')
				{
					numbers[amountOfNumbers] = number;
					number = 0;
					amountOfNumbers++;
				}
				else
				{
					number = (number * 10) + (ulong)(input.Lines[i][j] - '0');
				}
			}

			numbers[amountOfNumbers] = number;
			amountOfNumbers++;

			var span = numbers[..amountOfNumbers];

			sum += CanFormTotal(span, total);
		}

		return sum;
	}

	private static ulong CanFormTotal(ReadOnlySpan<ulong> numbers, ulong total)
	{
		var permutations = Math.Pow(2, numbers.Length - 1);
		Span<ulong> totals = stackalloc ulong[(int)permutations];

		totals[0] = numbers[0];
		var sumCount = 1;

		for (var i = 1; i < numbers.Length; i++)
		{
			var groupSums = sumCount;
			for (var j = 0; j < groupSums; j++)
			{
				var currentVal = totals[j];

				// Create a new entry for multiplication
				totals[sumCount++] = currentVal * numbers[i];

				// Replace the original entry with addition
				totals[j] = currentVal + numbers[i];
			}
		}

		for (var i = 0; i < sumCount; i++)
		{
			if (totals[i] == total)
			{
				return total;
			}
		}

		return 0;
	}

	public override ulong SolvePart2(Input input)
	{
		ulong sum = 0;

		scoped Span<ulong> numbers = stackalloc ulong[15];

		for (var i = 0; i < input.Lines.Length; i++)
		{
			var j = 0;
			ulong total = 0;
			while (input.Lines[i][j] != ':')
			{
				var newNumber = (ulong)(input.Lines[i][j] - '0');
				total = (total * (ulong)(10)) + newNumber;
				j++;
			}

			j = j + 2;//skip the ": "


			int amountOfNumbers = 0;
			ulong number = 0;
			for (; j < input.Lines[i].Length; j++)
			{
				if(input.Lines[i][j] == ' ')
				{
					numbers[amountOfNumbers] = number;
					number = 0;
					amountOfNumbers++;
				}
				else
				{
					number = (number * 10) + (ulong)(input.Lines[i][j] - '0');
				}
			}

			numbers[amountOfNumbers] = number;
			amountOfNumbers++;

			var span = numbers[..amountOfNumbers];

			sum += CanFormTotalPart2(span, total);
		}

		return sum;
	}

	private static ulong CanFormTotalPart2(ReadOnlySpan<ulong> numbers, ulong total)
	{
		var permutations = Math.Pow(3, numbers.Length - 1);
		Span<ulong> totals = stackalloc ulong[(int)permutations];

		totals[0] = numbers[0];
		var sumCount = 1;

		for (var i = 1; i < numbers.Length; i++)
		{
			var groupSums = sumCount;
			for (var j = 0; j < groupSums; j++)
			{

				var currentVal = totals[j];

				// Create a new entry for multiplication
				totals[sumCount++] = currentVal * numbers[i];

				// join 2 numbers
				totals[sumCount++] = currentVal * GetNumberOfCharacters(numbers[i]) + numbers[i];

				// Replace the original entry with addition
				totals[j] = currentVal + numbers[i];
			}
		}

		for (var i = 0; i < sumCount; i++)
		{
			if (totals[i] == total)
			{
				return total;
			}
		}

		return 0;
	}

	private static ulong GetNumberOfCharacters(ulong number)
	{
		ulong count = 1;
		while (number > 0)
		{
			count *= 10;
			number /= 10;
		}
		return count;
	}
}