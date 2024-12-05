using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Bart;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public class Day05 : HappyPuzzleBase<int>
{
	const int MaxAllowedNumbers = 25;

	public override int SolvePart1(Input input)
	{
		scoped Span<int> reportNumbers = stackalloc int[100*MaxAllowedNumbers];
		scoped Span<int> amountOfNumbers = stackalloc int[100];

		var row = 0;
		while (input.Lines[row] != "")
		{
			ReadSortedNumbers(ref reportNumbers, ref amountOfNumbers, input.Lines[row]);
			row++;
		}

		int sum = 0;
		scoped Span<int> line = stackalloc int[30];
		for (var i = row +1; i < input.Lines.Length; i++)
		{
			ReadNumbers(ref line, input.Lines[i], out var count);
			sum += Part1ValidateLine(ref line, ref count, ref reportNumbers, ref amountOfNumbers);
		}

		return sum;
	}

	private static int Part1ValidateLine(ref Span<int> line, ref int count, ref Span<int> reportNumbers, ref Span<int> amountOfNumbers)
	{
		for (var i = 0; i < count-1; i++)
		{
			for(var j = i+1; j < count; j++)
			{
				if (!ValidateOneNumberBeforeAnOther(line[i], line[j], ref reportNumbers, ref amountOfNumbers))
				{
					return 0;
				}
			}
		}
		//return middle number
		return line[count / 2];
	}


	private static void ReadSortedNumbers(ref Span<int> sortedNumberDictionary, ref Span<int> amountOfNumbers, string inputLine)
	{
		var number1 = (inputLine[0] - '0') * 10 + inputLine[1] - '0';
		var number2 = (inputLine[3] - '0') * 10 + inputLine[4] - '0';
		var amount = amountOfNumbers[number1];
		sortedNumberDictionary[number1 * MaxAllowedNumbers + amount] = number2;
		amountOfNumbers[number1]++;
	}

	public override int SolvePart2(Input input)
	{
		scoped Span<int> reportNumbers = stackalloc int[100*MaxAllowedNumbers];
		scoped Span<int> amountOfNumbers = stackalloc int[100];

		var row = 0;
		while (input.Lines[row] != "")
		{
			ReadSortedNumbers(ref reportNumbers, ref amountOfNumbers, input.Lines[row]);
			row++;
		}

		int sum = 0;
		scoped Span<int> line = stackalloc int[30];
		for (var i = row +1; i < input.Lines.Length; i++)
		{
			ReadNumbers(ref line, input.Lines[i], out var count);
			sum += Part2ValidateLine(ref line, ref count, ref reportNumbers, ref amountOfNumbers);
		}

		return sum;
	}

	private static int Part2ValidateLine(ref Span<int> line, ref int count, ref Span<int> reportNumbers, ref Span<int> amountOfNumbers)
	{
		var isCorrected = false;

		var i = 0;
		while(i < count -1)
		{
			var j = i + 1;
			while (j < count)
			{
				if (!ValidateOneNumberBeforeAnOther(line[i], line[j], ref reportNumbers, ref amountOfNumbers))
				{
					isCorrected = true;
					var x = line[i];
					line[i] = line[j];
					line[j] = x;

					i = 0 - 1; //break to begin
					j = count; //break to begin
				}
				j++;
			}
			i++;
		}

		//return middle number if it's been corrected
		return isCorrected
			? line[count / 2]
			: 0;


	}

	private static void ReadNumbers(ref Span<int> numbers, string input, out int count)
	{
		count = 0;
		while (count * 3 + 1 <= input.Length) //think it can be smaller than
		{
			numbers[count] = (input[count * 3] -'0') * 10 + input[count * 3 + 1] - '0';
			count++;
		}

	}

	private static bool ValidateOneNumberBeforeAnOther(int before, int after, ref Span<int> reportNumbers, ref Span<int> amountOfNumbers)
	{
		var amount = amountOfNumbers[after];
		for (var i = 0; i < amount; i++)
		{
			var shouldNotBefore = reportNumbers[after * MaxAllowedNumbers + i];
			if (shouldNotBefore == before)
			{
				return false;
			}
		}
		return true;
	}
}