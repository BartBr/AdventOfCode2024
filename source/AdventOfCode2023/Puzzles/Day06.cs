using System.Runtime.CompilerServices;
using AdventOfCode2023.Common;

namespace AdventOfCode2023.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day06 : HappyPuzzleBase
{
	private const int NumberLength = 4;

	public override object SolvePart1(Input input)
	{
		int total = 0;

		const int offset = 11;
		const int amountOfRaces = 4;

		var timeLine = input.Lines[0].AsSpan();
		var distanceLine = input.Lines[1].AsSpan();

		for (int race = 0; race < amountOfRaces; race++)
		{
			var startNumberIndex = offset + race * (NumberLength + 3);
			var time = AsNumber(timeLine.Slice(startNumberIndex, NumberLength));
			var distance = AsNumber(distanceLine.Slice(startNumberIndex, NumberLength));
			if (total == 0)
			{
				total = CalculateMultipleWaysToWin(time, distance);
			}
			else
			{
				total *= CalculateMultipleWaysToWin(time, distance);
			}
		}

		return total;
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int AsNumber(ReadOnlySpan<char> span)
	{
		var number = 0;
		for (int i = 0; i < NumberLength; i++)
		{
			if (span[i] != ' ')
			{
				var newNumber = (span[i] - '0');
				number = number * 10 + newNumber;
			}
		}
		return number;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int CalculateMultipleWaysToWin(int time, int distanceToBeat)
	{
		double turningPoint = -(-time + Math.Sqrt(time * time - 4 * distanceToBeat)) / 2;
		return time - 2 * ((int) turningPoint) - 1;
	}

	public override object SolvePart2(Input input)
	{
		int total = 0;

		const int offset = 12;

		var timeLine = input.Lines[0].AsSpan();
		var distanceLine = input.Lines[1].AsSpan();

		var startNumberIndex = offset;
		long time = AsSingleNumber(timeLine.Slice(startNumberIndex));
		long distance = AsSingleNumber(distanceLine.Slice(startNumberIndex));
		total = (int)CalculateMultipleWaysToWin2(time, distance);

		return total;
	}
	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static long AsSingleNumber(ReadOnlySpan<char> span)
	{
		long number = 0;
		for (int i = 0; i < span.Length; i++)
		{
			if (span[i] != ' ')
			{
				var newNumber = (span[i] - '0');
				number = number * 10 + newNumber;
			}
		}
		return number;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static long CalculateMultipleWaysToWin2(long time, long distanceToBeat)
	{
		double turningPoint = -(-time + Math.Sqrt(time * time - 4 * distanceToBeat)) / 2;
		return time - 2 * ((int) turningPoint) - 1;
	}
}