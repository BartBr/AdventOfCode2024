using System.Collections;
using System.Runtime.CompilerServices;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day09 : HappyPuzzleBase
{
	public override object SolvePart1(Input input)
	{
		int total = 0;

		for (int i = 0; i < input.Lines.Length; i++)
		{
			Span<int> numbers = stackalloc int[25];
			var line = input.Lines[i].AsSpan();
			var currentNumber = 0;
			var amountOfNumbers = 0;
			bool isNegativeNumber = false;

			for (int c = 0; c < line.Length; c++)
			{
				var ch = line[c];
				if (ch == ' ')
				{
					numbers[amountOfNumbers++] = isNegativeNumber? -1 * currentNumber : currentNumber;
					currentNumber = 0;
					isNegativeNumber = false;
				}
				else if (ch >= '0' && ch <= '9')
				{
					currentNumber = currentNumber * 10 + (ch - '0');
				}
				else if (ch == '-')
				{
					isNegativeNumber = true;
				}
			}
			numbers[amountOfNumbers] = isNegativeNumber? -1 * currentNumber : currentNumber;
			amountOfNumbers++;

			numbers = numbers.Slice(0, amountOfNumbers);

			//PrintLine(numbers.ToArray()); //TODO remove

			//Calculate second, third, ... line
			var allNumbersAreZero = false;
			var level = 1;
			Span<int> lastNumberOfEachLevel = stackalloc int[amountOfNumbers]; //can't be deeper than the length
			lastNumberOfEachLevel[0] = numbers[amountOfNumbers - 1];
			var levelAbove = numbers;
			while (!allNumbersAreZero)
			{
				allNumbersAreZero = true;
				var amountOnLowerLevel = amountOfNumbers - level;
				Span<int> lowerRow = stackalloc int[amountOnLowerLevel]; //lower number
				for (int j = 0; j < amountOnLowerLevel; j++)
				{
					lowerRow[j] = levelAbove[j + 1] - levelAbove[j];
					if (allNumbersAreZero && lowerRow[j] != 0) allNumbersAreZero = false;
				}

				//PrintLine(lowerRow.ToArray()); //TODO remove

				lastNumberOfEachLevel[level] = lowerRow[amountOnLowerLevel - 1];//last element
				//make ready for next while loop
				levelAbove = lowerRow;
				level++;
			}

			int calculatedExtraNumberOfLevel=0;
			for (int j = level-2; j >= 0; j--)
			{
				var lastNumberOfLevelAbove = lastNumberOfEachLevel[j];
				calculatedExtraNumberOfLevel += lastNumberOfLevelAbove;
			}
			total += calculatedExtraNumberOfLevel;
		}
		return total;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static void PrintLine(int[] row)
	{
		foreach (var t in row)
		{
			Console.Write(t + " ");
		}
		Console.WriteLine();
	}

	public override object SolvePart2(Input input)
	{
		int total = 0;

		for (int i = 0; i < input.Lines.Length; i++)
		{
			Span<int> numbers = stackalloc int[25];
			var line = input.Lines[i].AsSpan();
			var currentNumber = 0;
			var amountOfNumbers = 0;
			bool isNegativeNumber = false;

			for (int c = 0; c < line.Length; c++)
			{
				var ch = line[c];
				if (ch == ' ')
				{
					numbers[amountOfNumbers++] = isNegativeNumber? -1 * currentNumber : currentNumber;
					currentNumber = 0;
					isNegativeNumber = false;
				}
				else if (ch >= '0' && ch <= '9')
				{
					currentNumber = currentNumber * 10 + (ch - '0');
				}
				else if (ch == '-')
				{
					isNegativeNumber = true;
				}
			}
			numbers[amountOfNumbers] = isNegativeNumber? -1 * currentNumber : currentNumber;
			amountOfNumbers++;

			numbers = numbers.Slice(0, amountOfNumbers);

			//PrintLine(numbers.ToArray()); //TODO remove

			//Calculate second, third, ... line
			var allNumbersAreZero = false;
			var level = 1;
			Span<int> firstNumberOfEachLevel = stackalloc int[amountOfNumbers]; //can't be deeper than the length
			firstNumberOfEachLevel[0] = numbers[0];
			var levelAbove = numbers;
			while (!allNumbersAreZero)
			{
				allNumbersAreZero = true;
				var amountOnLowerLevel = amountOfNumbers - level;
				Span<int> lowerRow = stackalloc int[amountOnLowerLevel]; //lower number
				for (int j = 0; j < amountOnLowerLevel; j++)
				{
					lowerRow[j] = levelAbove[j + 1] - levelAbove[j];
					if (allNumbersAreZero && lowerRow[j] != 0) allNumbersAreZero = false;
				}

				//PrintLine(lowerRow.ToArray()); //TODO remove

				firstNumberOfEachLevel[level] = lowerRow[0];//last element
				//make ready for next while loop
				levelAbove = lowerRow;
				level++;
			}

			//Console.WriteLine();

			int calculatedExtraNumberOfLevel=0;
			for (int j = level-2; j >= 0; j--)
			{
				var lastNumberOfLevelAbove = firstNumberOfEachLevel[j];
				calculatedExtraNumberOfLevel = -calculatedExtraNumberOfLevel + lastNumberOfLevelAbove;
			}

			// Console.WriteLine(calculatedExtraNumberOfLevel);
			// Console.WriteLine();

			total += calculatedExtraNumberOfLevel;
		}
		return total;
	}
}