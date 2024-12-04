using System.Buffers;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Bart;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public class Day03 : HappyPuzzleBase<long>
{
	public override long SolvePart1(Input input)
	{
		var inputSpan = input.Text.AsSpan();
		return SumValidProducts(inputSpan);
	}

	private static long ReadNumbers(ReadOnlySpan<char> input, int fromCharacter = 4)
	{
		long number1 = 0;
		var number = 0;
		for (var characterIndex = fromCharacter; characterIndex < input.Length-1; characterIndex++)
		{
			var c = input[characterIndex];
			if (c is >= '0' and <= '9')
			{
				number *= 10;
				number += c - '0';
			}
			else if(c == ',' && number1 == 0)
			{
				number1 = number;
				number = 0;
			}
			else if (c == ')' && number1 != 0 && number != 0)
			{
				return number1 * number;
			}
			else
			{
				return 0;
			}
		}
		return 0;
	}


	private static long SumValidProducts(ReadOnlySpan<char> text)
	{
		long sum = 0;
		while (text.Length != 0)
		{
			var indexOfDontFunc = text.IndexOfAny(MilFuncSearchValues);
			if (indexOfDontFunc > 0)
			{
				text = text[(indexOfDontFunc + 4)..];
				sum += ReadNumbers(text,0);
			}
			else
			{
				return sum;
			}
		}
		return sum;
	}

	private static readonly SearchValues<string> MilFuncSearchValues = SearchValues.Create(["mul("], StringComparison.Ordinal);
	private static readonly SearchValues<string> DontFuncSearchValues = SearchValues.Create(["don't()"], StringComparison.Ordinal);
	private static readonly SearchValues<string> DoFuncSearchValues = SearchValues.Create(["do()"], StringComparison.Ordinal);

	public override long SolvePart2(Input input)
	{
		return input.Lines.Sum(CalculateSumForLine);
	}

	private static long CalculateSumForLine(string input)
	{
		var inputSpan = input.AsSpan();

		long sum = 0;

		//In the while:
		// 1: Process everything to first don't
		// 2: Skip pos to next Do()
		while (inputSpan.Length > 0)
		{
			var indexOfDontFunc = inputSpan.IndexOfAny(DontFuncSearchValues);
			if (indexOfDontFunc > 0)
			{
				var validSubPart = inputSpan[..indexOfDontFunc];
				sum += SumValidProducts(validSubPart);
				inputSpan = inputSpan[(indexOfDontFunc + 7)..];
			}
			else
			{
				sum += SumValidProducts(inputSpan);
				return sum;
			}

			var indexOfDoFunc = inputSpan.IndexOfAny(DoFuncSearchValues);
			if (indexOfDoFunc > 0)
			{
				inputSpan = inputSpan[(indexOfDoFunc + 4)..];
			}
			else
			{
				return sum;
			}
		}

		return sum;
	}
}