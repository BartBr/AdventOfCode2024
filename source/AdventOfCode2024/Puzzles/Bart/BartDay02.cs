using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Bart;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public class BartDay02 : HappyPuzzleBase<int>
{
	public override int SolvePart1(Input input)
	{
		scoped Span<int> reportNumbers = stackalloc int[10];

		var rows = input.Lines.Length;

		var safeReports = 0;
		for (var i = 0; i < rows; i++)
		{
			ReadNumbers(ref reportNumbers, input.Lines[i], out var columns);
			if (IsReportSafe1(ref reportNumbers, ref columns))
			{
				safeReports++;
			}
		}
		return safeReports;
	}

	private bool IsReportSafe1(ref Span<int> reportNumbers, ref int columns)
	{
		bool goingUp = reportNumbers[0] < reportNumbers[1]; //assumption always 2 columns
		var diff = reportNumbers[1] - reportNumbers[0];
		if (diff * diff < 1 || diff * diff > 9)
		{
			return false;
		}

		var index = 2;
		while (index < columns)
		{
			if (goingUp && reportNumbers[index - 1] >= reportNumbers[index])
			{
				return false;
			}
			if (!goingUp && reportNumbers[index - 1] <= reportNumbers[index])
			{
				return false;
			}

			diff = reportNumbers[index] - reportNumbers[index-1];
			if (diff * diff < 1 || diff * diff > 9)
			{
				return false;
			}

			index++;
		}
		return true;
	}

	public override int SolvePart2(Input input)
	{
		scoped Span<int> reportNumbers = stackalloc int[10];
		scoped Span<int> reportNumbersA = stackalloc int[10];
		scoped Span<int> reportNumbersB = stackalloc int[10];

		var rows = input.Lines.Length;

		var safeReports = 0;
		for (var i = 0; i < rows; i++)
		{
			ReadNumbers(ref reportNumbers, input.Lines[i], out var columns);
			if (IsReportSafe2(ref reportNumbers, ref reportNumbersA, ref reportNumbersB, columns))
			{
				safeReports++;
			}
		}
		return safeReports;
	}

	private static bool IsReportSafe2(ref Span<int> reportNumbers,ref Span<int> reportNumbersA,ref Span<int> reportNumbersB, int columns)
	{
		return IsReportSafeGoingUp2(ref reportNumbers, ref reportNumbersA, ref reportNumbersB, columns)
		       || IsReportSafeGoingDown2(ref reportNumbers, ref reportNumbersA, ref reportNumbersB, columns);
	}

	private static bool IsReportSafeGoingUp2(ref Span<int> reportNumbers,ref Span<int> reportNumbersA, ref Span<int> reportNumbersB, int columns, bool allowedToSkip = true)
	{
		var prevIndex = 0;
		var nextIndex = 1;

		while (nextIndex < columns)
		{
			var diff = reportNumbers[nextIndex] - reportNumbers[prevIndex];
			if (diff < 1 || diff > 3)
			{
				if(allowedToSkip)
				{
					reportNumbers.CopyTo(reportNumbersA);
					reportNumbers.CopyTo(reportNumbersB);

					RemoveNumberAtIndex(ref reportNumbersA, prevIndex);
					RemoveNumberAtIndex(ref reportNumbersB, nextIndex);
					columns--;
					return IsReportSafeGoingUp2(ref reportNumbersA, ref reportNumbersA, ref reportNumbersA, columns, false) ||
					       IsReportSafeGoingUp2(ref reportNumbersB, ref reportNumbersB, ref reportNumbersB, columns, false);
				}
				return false;
			}

			prevIndex++;
			nextIndex++;
		}
		return true;
	}

	private static void RemoveNumberAtIndex(ref Span<int> reportNumbers, int indexToRemove)
	{
		for (var i = indexToRemove; i < reportNumbers.Length - 1; i++)
		{
			reportNumbers[i] = reportNumbers[i + 1];
		}
	}

	private static bool IsReportSafeGoingDown2(ref Span<int> reportNumbers,ref Span<int> reportNumbersA, ref Span<int> reportNumbersB, int columns, bool allowedToSkip = true)
	{
		var prevIndex = 0;
		var nextIndex = 1;

		while (nextIndex < columns)
		{
			var diff = reportNumbers[prevIndex] - reportNumbers[nextIndex];
			if (diff < 1 || diff > 3)
			{
				if(allowedToSkip)
				{
					reportNumbers.CopyTo(reportNumbersA);
					reportNumbers.CopyTo(reportNumbersB);

					RemoveNumberAtIndex(ref reportNumbersA, prevIndex);
					RemoveNumberAtIndex(ref reportNumbersB, nextIndex);
					columns--;
					return IsReportSafeGoingDown2(ref reportNumbersA, ref reportNumbersA, ref reportNumbersA, columns, false) ||
					       IsReportSafeGoingDown2(ref reportNumbersB, ref reportNumbersB, ref reportNumbersB, columns, false);
				}
				return false;
			}

			prevIndex++;
			nextIndex++;
		}
		return true;
	}

	private static void ReadNumbers(ref Span<int> levels, string input, out int columns)
	{
		columns = 0;
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
				levels[columns] = number;
				number = 0;
				columns++;
			}
		}
		levels[columns] = number;
		columns++;
	}
}