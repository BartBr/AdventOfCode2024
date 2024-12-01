using System.Buffers;
using System.Collections;
using System.Data.Common;
using System.Dynamic;
using System.Runtime.CompilerServices;
using AdventOfCode2023.Common;

namespace AdventOfCode2023.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day12 : HappyPuzzleBase
{
	private static long testedPermutations = 0;

	public override object SolvePart1(Input input)
	{
		long total = 0;
		foreach (var line in input.Lines)
		{
			var ways = CountDifferentPossibleWays1(line);

			Console.WriteLine(line + $" --- ways:{ways} permutations:{testedPermutations}");
			testedPermutations = 0;

			total += ways;
		}
		return total;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private long CountDifferentPossibleWays1(string line)
	{
		var span = line.AsSpan();
		var separator = span.IndexOf(' ');

		scoped Span<char> springs = stackalloc char[separator+1];
		span.Slice(0, separator+1).CopyTo(springs);
		springs[separator] = '.';
		//Console.WriteLine(new string(springs));

		scoped Span<int> numbers = stackalloc int[15];

		var amountOfNumbers = 0;
		var number = 0;
		for (var i = separator+1; i < line.Length; i++)
		{
			if (span[i] == ',')
			{
				numbers[amountOfNumbers] = number;
				amountOfNumbers++;
				number = 0;
			}
			else
			{
				number = number * 10 + (span[i] - '0');
			}
		}
		if(number>0)
		{
			numbers[amountOfNumbers] = number;
			amountOfNumbers++;
		}
		numbers = numbers.Slice(0, amountOfNumbers);

		return CountRecursive(springs, numbers, 0, 0, 0);
	}

	private long CountRecursive(scoped Span<char> springs, scoped Span<int> numbers, int springIndex, int inspectGroup, int currentGroupLength)
	{
		if (springIndex == springs.Length)
		{
			if(inspectGroup == numbers.Length)
			{
				//Console.WriteLine($"valid {new string(springs.Slice(0, springIndex).ToArray())}");
				return 1;
			}
			return 0;
		}
		if (springs[springIndex] == '?')
		{
			scoped Span<char> springsA = stackalloc char[springs.Length];
			springs.CopyTo(springsA);

			springs[springIndex] = '.';
			springsA[springIndex] = '#';

			testedPermutations++;


			var count1 = CountRecursive(springs, numbers, springIndex, inspectGroup, currentGroupLength);
			var count2 = CountRecursive(springsA, numbers, springIndex, inspectGroup, currentGroupLength);
			return count1 + count2;
		}
		else if (springs[springIndex] == '.')
		{
			if (currentGroupLength > 0)
			{
				if (inspectGroup >= numbers.Length)
				{
					//Console.WriteLine($"invalid {new string(springs.Slice(0,springIndex).ToArray())} group length");
					return 0;
				}

				if (numbers[inspectGroup] != currentGroupLength)
				{
					//Console.WriteLine($"invalid {new string(springs.Slice(0,springIndex).ToArray())} group invalid");
					return 0;
				}
				inspectGroup++;
			}
			currentGroupLength = 0;
		}
		else if (springs[springIndex] == '#')
		{
			currentGroupLength++;
		}
		return CountRecursive(springs, numbers, springIndex+1, inspectGroup,currentGroupLength);
	}

	private bool ValidatePartial(scoped Span<char> springs, scoped Span<int> numbers, int maxIndex)
	{
		var currentGroup = 0;
		var previousBrokenSpring = false;
		var numberOfBrokenSprings = 0;
		for (int i = 0; i < maxIndex; i++)
		{
			if (previousBrokenSpring && springs[i] != '#')
			{
				if (currentGroup >= numbers.Length) return false;
				if (numbers[currentGroup] != numberOfBrokenSprings)
				{
					//Console.WriteLine($"{springs} Currentgroup {currentGroup} / {numberOfBrokenSprings}");
					return false;
				}

				currentGroup++;
				numberOfBrokenSprings = 0;
				previousBrokenSpring = false;
			}
			else if (springs[i] == '#')
			{
				numberOfBrokenSprings++;
				previousBrokenSpring = true;
			}
		}

		return true; // COULD BE CORRECT
	}

	private int ValidateOption(scoped Span<char> springs, scoped Span<int> numbers)
	{
		var currentGroup = 0;
		var previousBrokenSpring = false;
		var numberOfBrokenSprings = 0;
		for (int i = 0; i < springs.Length; i++)
		{
			if (previousBrokenSpring && springs[i] != '#')
			{
				if (currentGroup < numbers.Length && numbers[currentGroup] != numberOfBrokenSprings)
				{
					//Console.WriteLine($"{springs} Currentgroup {currentGroup} / {numberOfBrokenSprings}");
					return 0;
				}
				currentGroup++;
				numberOfBrokenSprings = 0;
				previousBrokenSpring = false;
			}
			else if (springs[i] == '#')
			{
				numberOfBrokenSprings++;
				previousBrokenSpring = true;
			}
		}

		if (currentGroup != numbers.Length) //CHECK if amount of groups is equal
		{
			//Console.WriteLine($"{springs} unequal group length {currentGroup} / {numbers.Length}");

			return 0;
		}

		//Console.WriteLine($"{springs} CORRECT!! {currentGroup} / {numbers.Length}");
		return 1;
	}

	//[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	//private void FillGalaxyFromInput(string[] input, scoped Span<long> galaxyPositionRows, scoped Span<long> galaxyPositionColumns, long increment, out long rows, out long columns, out int galaxies)

	public override object SolvePart2(Input input)
	{
		return SlowSolvePart2(input);
	}

	private object SlowSolvePart2(Input input)
	{
		long total = 0;
		foreach (var line in input.Lines)
		{
			var ways = CountDifferentPossibleWays2(line);
			Console.WriteLine($" --- ways:{ways} permutations:{testedPermutations}");
			testedPermutations = 0;
			total += ways;
		}

		//Console.WriteLine();
		return total;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private long CountDifferentPossibleWays2(string line)
	{
		var span = line.AsSpan();
		var separator = span.IndexOf(' ');

		scoped Span<char> springs = stackalloc char[(separator+1)*5+2];
		for (int i = 0; i < separator; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				springs[i + (separator+1) * j] = span[i];
			}
		}

		// 0 1 2 3
		// 2 5 8 11

		for (int j = 0; j < 5; j++)
		{
			springs[(separator +1)* (j) + separator] = '?';
		}

		springs[(separator + 1) * 5 + 1] = '.';

		scoped Span<int> numbers = stackalloc int[40];

		var amountOfNumbers = 0;
		var number = 0;
		for (var i = separator+1; i < line.Length; i++)
		{
			if (span[i] == ',')
			{
				numbers[amountOfNumbers] = number;
				amountOfNumbers++;
				number = 0;
			}
			else
			{
				number = number * 10 + (span[i] - '0');
			}
		}
		if(number>0)
		{
			numbers[amountOfNumbers] = number;
			amountOfNumbers++;
		}

		for (int i = 0; i < amountOfNumbers*5; i++)
		{
			numbers[i] = numbers[i % amountOfNumbers];
		}

		amountOfNumbers *= 5;

		numbers = numbers.Slice(0, amountOfNumbers);

		Console.Write(new string(springs) + " " + string.Join(',',numbers.ToArray().ToList()));
		return CountRecursive(springs, numbers, 0, 0, 0);
	}

	// [MethodImpl(MethodImplOptions.AggressiveOptimization)]
	// private static int GetIndex(int i, int j, int columns)
	// {
	// 	return i * columns + j;
	// }
}