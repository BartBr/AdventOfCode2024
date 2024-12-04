using System.Runtime.CompilerServices;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jens;

public class Day02 : HappyPuzzleBase<uint>
{
	public override uint SolvePart1(Input input)
	{
		uint safeCount = 0;

		foreach (var inputLine in input.Lines)
		{
			var inputLineSpan = inputLine.AsSpan();
			Part1_ParseAndCalculateDiff(ref inputLineSpan, ref safeCount);
		}

		return safeCount;
	}

	private static void Part1_ParseAndCalculateDiff(ref ReadOnlySpan<char> inputLine, ref uint safeCount)
	{
		var previousNumber = -1;
		var previousSign = 0;

		foreach (var range in inputLine.Split(' '))
		{
			var currentNumber = ParseNumber(inputLine[range]);

			// Do thingy
			if (previousNumber != -1)
			{
				var result = previousNumber - currentNumber;
				var resultSign = Math.Sign(result);

				if (previousSign != 0 && previousSign != resultSign)
				{
					return;
				}

				if (Math.Abs(result) is > 3 or < 1)
				{
					return;
				}

				previousSign = resultSign;
			}

			previousNumber = currentNumber;
		}

		safeCount++;
	}

	public override uint SolvePart2(Input input)
	{
		uint safeCount = 0;

		Span<int> spanBuffer = stackalloc int[8];
		foreach (var inputLine in input.Lines)
		{
			var inputLineSpan = inputLine.AsSpan();
			Part2_Parse(inputLineSpan, ref spanBuffer, out var spanBufferCount);
			Part2_PermutateAndValidate(spanBuffer.Slice(0, spanBufferCount), ref safeCount);
		}

		return safeCount;
	}

	private static void Part2_Parse(ReadOnlySpan<char> inputLine, ref Span<int> lineBuffer, out int lineBufferCount)
	{
		var bufferIndex = 0;
		foreach (var range in inputLine.Split(' '))
		{
			lineBuffer[bufferIndex++] = ParseNumber(inputLine[range]);
		}

		lineBufferCount = bufferIndex;
	}

	private void Part2_PermutateAndValidate(Span<int> slice, ref uint safeCount)
	{
		if (Part2_Validate(slice.Slice(1)))
		{
			safeCount++;
			return;
		}

		if (Part2_Validate(slice.Slice(0, slice.Length - 1)))
		{
			safeCount++;
			return;
		}

		Span<int> tempSlice = stackalloc int[slice.Length - 1];
		for (var i = 1; i < slice.Length - 1; i++)
		{
			slice.Slice(0, i).CopyTo(tempSlice);
			slice.Slice(i + 1).CopyTo(tempSlice.Slice(i));

			if (Part2_Validate(tempSlice))
			{
				safeCount++;
				return;
			}
		}
	}

	private static bool Part2_Validate(Span<int> slice)
	{
		var previousNumber = slice[0];
		var previousSign = 0;

		for (var i = 1; i < slice.Length; i++)
		{
			var number = slice[i];

			var result = previousNumber - number;
			var resultSign = Math.Sign(result);

			if (previousSign != 0 && previousSign != resultSign)
			{
				return false;
			}

			if (Math.Abs(result) is > 3 or < 1)
			{
				return false;
			}

			previousSign = resultSign;
			previousNumber = number;
		}

		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int ParseNumber(ReadOnlySpan<char> inputLine)
	{
		var number = 0;
		for (var characterIndex = 0; characterIndex < inputLine.Length; characterIndex++)
		{
			var c = inputLine[characterIndex];
			number *= 10;
			number += c - '0';
		}

		return number;
	}
}