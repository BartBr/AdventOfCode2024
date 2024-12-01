using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AdventOfCode2024.Benchmarks.Standalone.Puzzles;

[MemoryDiagnoser(true)]
[CategoriesColumn, AllStatisticsColumn, BaselineColumn, MinColumn, Q1Column, MeanColumn, Q3Column, MaxColumn, MedianColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class Day01Benchmark
{
	private readonly Input _input;

	public Day01Benchmark()
	{
		// Input files are symlinked from the Assets folder in the AdventOfCode2024 project
		_input = Helpers.GetInput("Day01.txt");
	}

	[Benchmark]
	[BenchmarkCategory(Constants.PART1)]
	public int Part1_Bart()
	{
		var total = 0;
		for (int i = 0; i < _input.Lines.Length; i++)
		{
			int j = 0;
			while (_input.Lines[i][j] < '0' || _input.Lines[i][j] > '9')
			{
				j++;
			}

			int k = _input.Lines[i].Length - 1;
			while (_input.Lines[i][k] < '0' || _input.Lines[i][k] > '9')
			{
				k--;
			}

			int lineNumber = (_input.Lines[i][j] - '0') * 10 + (_input.Lines[i][k] - '0');
			total += lineNumber;
		}
		return total;
	}

	[Benchmark]
	[BenchmarkCategory(Constants.PART2)]
	public int Part2_Bart()
	{
		var total = 0;
		for (int i = 0; i < _input.Lines.Length; i++)
		{
			if (_input.Lines[i].Length < 1)
			{
				continue;
			}

			int j = 0;
			int? firstNumber = null;
			while (firstNumber==null)
			{
				firstNumber = GetNumber(_input.Lines[i], j);
				j++;
			}

			j = _input.Lines[i].Length-1;
			int? secondNumber = null;
			while (secondNumber==null)
			{
				secondNumber = GetReverseNumber(_input.Lines[i], j);
				j--;
			}

			int lineNumber = firstNumber.Value * 10 + secondNumber.Value;
			total += lineNumber;
		}

		return total;
	}

	static int? GetNumber(string s, int j)
	{
		if (s[j] >= '0' && s[j] <= '9')
		{
			return s[j] - '0';
		}

		if (s.Length - j >= 3 && s[j] == 'o' && s[j + 1] == 'n' && s[j + 2] == 'e')
		{
			return 1;
		}

		if (s.Length - j >= 3 && s[j] == 't' && s[j + 1] == 'w' && s[j + 2] == 'o')
		{
			return 2;
		}

		//three
		if (s.Length - j >= 5 && s[j] == 't' && s[j + 1] == 'h' && s[j + 2] == 'r' && s[j + 3] == 'e' && s[j + 4] == 'e')
		{
			return 3;
		}

		//four
		if (s.Length - j >= 4 && s[j] == 'f' && s[j + 1] == 'o' && s[j + 2] == 'u' && s[j + 3] == 'r')
		{
			return 4;
		}

		//five
		if (s.Length - j >= 4 && s[j] == 'f' && s[j + 1] == 'i' && s[j + 2] == 'v' && s[j + 3] == 'e')
		{
			return 5;
		}

		if (s.Length - j >= 3 && s[j] == 's' && s[j + 1] == 'i' && s[j + 2] == 'x')
		{
			return 6;
		}

		//seven
		if (s.Length - j > 5 && s[j] == 's' && s[j + 1] == 'e' && s[j + 2] == 'v' && s[j + 3] == 'e' && s[j + 4] == 'n')
		{
			return 7;
		}

		if (s.Length - j >= 5 && s[j] == 'e' && s[j + 1] == 'i' && s[j + 2] == 'g' && s[j + 3] == 'h' && s[j + 4] == 't')
		{
			return 8;
		}

		if (s.Length - j >= 4 && s[j] == 'n' && s[j + 1] == 'i' && s[j + 2] == 'n' && s[j + 3] == 'e')
		{
			return 9;
		}

		return null;
	}

	static int? GetReverseNumber(string s, int j)
	{
		if (s[j] >= '0' && s[j] <= '9')
		{
			return s[j] - '0';
		}

		if (j >= 2 && s[j - 2] == 'o' && s[j - 1] == 'n' && s[j] == 'e')
		{
			return 1;
		}

		if (j >= 2 && s[j - 2] == 't' && s[j - 1] == 'w' && s[j] == 'o')
		{
			return 2;
		}

		//three
		if (j >= 4 && s[j - 4] == 't' && s[j - 3] == 'h' && s[j - 2] == 'r' && s[j - 1] == 'e' && s[j] == 'e')
		{
			return 3;
		}

		//four
		if (j >= 3 && s[j - 3] == 'f' && s[j - 2] == 'o' && s[j - 1] == 'u' && s[j] == 'r')
		{
			return 4;
		}

		//five
		if (j >= 3 && s[j - 3] == 'f' && s[j - 2] == 'i' && s[j - 1] == 'v' && s[j] == 'e')
		{
			return 5;
		}

		if (j >= 2 && s[j - 2] == 's' && s[j - 1] == 'i' && s[j] == 'x')
		{
			return 6;
		}

		//seven
		if (j >= 4 && s[j - 4] == 's' && s[j - 3] == 'e' && s[j - 2] == 'v' && s[j - 1] == 'e' && s[j] == 'n')
		{
			return 7;
		}

		if (j >= 4 && s[j - 4] == 'e' && s[j - 3] == 'i' && s[j - 2] == 'g' && s[j - 1] == 'h' && s[j] == 't')
		{
			return 8;
		}

		if (j >= 3 && s[j - 3] == 'n' && s[j - 2] == 'i' && s[j - 1] == 'n' && s[j] == 'e')
		{
			return 9;
		}

		return null;
	}

	[Benchmark]
	[BenchmarkCategory(Constants.PART1)]
	public int Part1_Jens()
	{
		var intCharSearchValues = SearchValues.Create("123456789");

		var total = 0;
		foreach (var line in _input.Lines)
		{
			var lineSpan = line.AsSpan();

			var firstCharIndex = lineSpan.IndexOfAny(intCharSearchValues);
			var lastCharIndex = lineSpan.LastIndexOfAny(intCharSearchValues);

			var calibrationNumber = (lineSpan[firstCharIndex] - '0') * 10 + (lineSpan[lastCharIndex] - '0');

			total += calibrationNumber;
		}

		return total;
	}


	[Benchmark]
	[BenchmarkCategory(Constants.PART2)]
	public int Part2_Jens()
	{
		var intCharSearchValues = SearchValues.Create("1234567890");
		string[] wordForms = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };


		var total = 0;
		foreach (var line in _input.Lines)
		{
			var lineSpan = line.AsSpan();

			var firstIntCharIndex = lineSpan.IndexOfAny(intCharSearchValues);
			var lastIntCharIndex = lineSpan.LastIndexOfAny(intCharSearchValues);

			var firstNumber = lineSpan[firstIntCharIndex] - '0';
			var lastNumber = lineSpan[lastIntCharIndex] - '0';

			/*Optimize further
			Number word forms are 3 characters or longer at least so cutting off the edges to reduce lookup times if needed or fully early returning
			if (firstIntCharIndex <= 2)
			{
			    firstNumber = lineSpan[firstIntCharIndex] - '0';
			    lineSpan = lineSpan.Slice(firstIntCharIndex + 1);

			    lastIntCharIndex -= firstIntCharIndex + 1;
			    firstIntCharIndex = 0;
			}

			if (lastIntCharIndex >= lineSpan.Length - 2)
			{
			    lastNumber = lineSpan[lastIntCharIndex] - '0';
			    lineSpan = lineSpan.Slice(0, lastIntCharIndex - 2);
			}

			// Early calculate if already possible
			if (firstNumber != -1 && lastNumber != -1)
			{
			    total += firstNumber * 10 + lastNumber;
			    continue;
			}*/

			// We can't be sure just yet so we should actually lookup up the number word forms as well.

			var didFind = FindFirstWordForm(ref lineSpan, out var firstWordFormIndex, out var firstWordFormValue, out var firstWordFormLength);

			// No words found, early return based on previous integers chars
			if (!didFind)
			{
				total += firstNumber * 10 + lastNumber;
				continue;
			}

			var offset = 0;
			if (firstWordFormIndex < firstIntCharIndex)
			{
				firstNumber = firstWordFormValue!.Value;

				offset = firstWordFormIndex + firstWordFormLength!.Value;
				lineSpan = lineSpan[offset..];
			}

			if (FindLastWordForm(ref lineSpan, out var lastWordFormIndex, out var lastWordFormValue))
			{
				if (offset + lastWordFormIndex > lastIntCharIndex)
				{
					lastNumber = lastWordFormValue!.Value;
				}
			}
			else if (firstWordFormIndex > firstIntCharIndex)
			{
				lastNumber = firstWordFormValue!.Value;
			}

			total += firstNumber * 10 + lastNumber;
		}

		return total;


		bool FindFirstWordForm(ref ReadOnlySpan<char> span, out int startingIndex, [NotNullWhen(true)] out int? value, [NotNullWhen(true)] out int? wordFormLength)
		{
			startingIndex = int.MaxValue;
			value = null;
			wordFormLength = null;
			for (var i = 0; i < wordForms.Length; i++)
			{
				var wordForm = wordForms[i];
				var wordFormIndex = span.IndexOf(wordForm, StringComparison.Ordinal);
				if (wordFormIndex > -1 && (startingIndex > wordFormIndex))
				{
					startingIndex = wordFormIndex;
					value = i;
					wordFormLength = wordForm.Length;
				}
			}

			return startingIndex != int.MaxValue;
		}

		bool FindLastWordForm(ref ReadOnlySpan<char> span, out int startingIndex, [NotNullWhen(true)] out int? value)
		{
			startingIndex = int.MinValue;
			value = null;
			for (var i = 0; i < wordForms.Length; i++)
			{
				var wordForm = wordForms[i];
				var wordFormIndex = span.LastIndexOf(wordForm, StringComparison.Ordinal);
				if (wordFormIndex > -1 && (startingIndex < wordFormIndex))
				{
					startingIndex = wordFormIndex;
					value = i;
				}
			}

			return startingIndex != int.MinValue;
		}
	}
}