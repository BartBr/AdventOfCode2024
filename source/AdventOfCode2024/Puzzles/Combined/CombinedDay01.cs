using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Combined;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public class CombinedDay01 : HappyPuzzleBase<long>
{
	public override long SolvePart1(Input input)
	{
		var inputRows = input.Lines.Length;

		scoped Span<long> firstList = stackalloc long[inputRows];
		scoped Span<long> secondList = stackalloc long[inputRows];

		for (var i = 0; i < inputRows; i++)
		{
			ParseInput(input.Lines[i], out var first, out var second);

			firstList[i] = first;
			secondList[i] = second;
		}

		firstList.Sort();
		secondList.Sort();

		long total = 0;
		for (var i = 0; i < inputRows; i++)
		{
			var diff = long.Abs(firstList[i] - secondList[i]);
			total += diff;
		}
		return total;
	}

	public override long SolvePart2(Input input)
	{
		var inputRows = input.Lines.Length;

		scoped Span<int> firstList = stackalloc int[inputRows];
		scoped Span<int> histogramSecondList = stackalloc int[100000];

		firstList = firstList[..inputRows];

		for (var i = 0; i < inputRows; i++)
		{
			ParseInput(input.Lines[i], out var first, out var second);
			firstList[i] = first;
			histogramSecondList[second]++;
		}

		long total = 0;
		for (var i = 0; i < inputRows; i++)
		{
			var diff = firstList[i] * histogramSecondList[firstList[i]];
			total += diff;
		}
		return total;
	}

	private static void ParseInput(string input, out int left, out int right)
	{
		left = 0;

		var number = 0;
		for (var characterIndex = 0; characterIndex < input.Length; characterIndex++)
		{
			var c = input[characterIndex];
			if (c is >= '0' and <= '9')
			{
				number *= 10;
				number += c - '0';
			}
			else
			{
				left = number;
				number = 0;
				characterIndex += 2;
			}
		}

		right = number;
	}

}