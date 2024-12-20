using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jens;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public class Day01 : HappyPuzzleBase<int>
{
	private const int BUFFER_SIZE = 1000;

	public override int SolvePart1(Input input)
	{
		scoped Span<int> left = stackalloc int[BUFFER_SIZE];
		scoped Span<int> right = stackalloc int[BUFFER_SIZE];

		ParseInput(input.Lines, ref left, ref right);

		left.Sort();
		right.Sort();

		var sumDistance = 0;
		for (var i = 0; i < left.Length; i++)
		{
			sumDistance += Math.Abs(right[i] - left[i]);
		}

		return sumDistance;
	}

	public override int SolvePart2(Input input)
	{
		scoped Span<int> left = stackalloc int[BUFFER_SIZE];
		scoped Span<int> right = stackalloc int[BUFFER_SIZE];

		ParseInput(input.Lines, ref left, ref right);

		left.Sort();
		right.Sort();

		var referenceNumber = -1;
		var referenceScore = 0;
		var totalScore = 0;
		var leftIndex = 0;
		var rightIndex = 0;

		do
		{
			var leftNumber = left[leftIndex];
			if (leftNumber != referenceNumber)
			{
				referenceNumber = leftNumber;
				referenceScore = 0;

				do
				{
					var rightNumber = right[rightIndex];
					if (rightNumber == referenceNumber)
					{
						referenceScore++;
					}
					else if (rightNumber > referenceNumber)
					{
						break;
					}

					++rightIndex;
				} while (rightIndex < right.Length);

				referenceScore *= referenceNumber;
			}

			totalScore += referenceScore;

			++leftIndex;
		} while (leftIndex < left.Length);

		return totalScore;
	}

	private static void ParseInput(string[] input, ref Span<int> left, ref Span<int> right)
	{
		for (var lineIndex = 0; lineIndex < input.Length; lineIndex++)
		{
			var line = input[lineIndex];
			var target = left;
			var number = 0;
			for (var characterIndex = 0; characterIndex < line.Length; characterIndex++)
			{
				var c = line[characterIndex];
				if (c is >= '0' and <= '9')
				{
					number *= 10;
					number += c - '0';
				}
				else
				{
					target[lineIndex] = number;
					number = 0;
					characterIndex += 2;
					target = right;
				}
			}

			target[lineIndex] = number;
		}
	}
}