using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jens;

public class Day03 : HappyPuzzleBase<int>
{
	public override int SolvePart1(Input input)
	{
		var readOnlySpan = input.Text.AsSpan();

		var totalScore = 0;
		var firstNumber = 0;
		var secondNumber = 0;

		var i = 0;
		for (; i < readOnlySpan.Length; i++)
		{
			if (readOnlySpan[i] != 'm' || !readOnlySpan[(i + 1)..(i + 4)].Equals("ul(", StringComparison.Ordinal))
			{
				continue;
			}

			i += 4;

			ref var target = ref firstNumber;

			for (; i < readOnlySpan.Length; i++)
			{
				var c = readOnlySpan[i];

				if (c is >= '0' and <= '9')
				{
					target = target * 10 + (c - '0');
				}
				else if (c == ',')
				{
					target = ref secondNumber;
				}
				else if (c == ')')
				{
					totalScore += firstNumber * secondNumber;
					break;
				}
				else
				{
					break;
				}
			}

			firstNumber = 0;
			secondNumber = 0;
		}

		return totalScore;
	}

	public override int SolvePart2(Input input)
	{
		var readOnlySpan = input.Text.AsSpan();

		var shouldDo = true;

		var totalScore = 0;
		var firstNumber = 0;
		var secondNumber = 0;

		var i = 0;
		for (; i < readOnlySpan.Length; i++)
		{
			var c = readOnlySpan[i];
			if (shouldDo)
			{
				if (c == 'm' && readOnlySpan[(i + 1)..(i + 4)].Equals("ul(", StringComparison.Ordinal))
				{
					i += 4;

					ref var target = ref firstNumber;

					for (; i < readOnlySpan.Length; i++)
					{
						c = readOnlySpan[i];

						if (c is >= '0' and <= '9')
						{
							target = target * 10 + (c - '0');
						}
						else if (c == ',')
						{
							target = ref secondNumber;
						}
						else if (c == ')')
						{
							totalScore += firstNumber * secondNumber;
							break;
						}
						else
						{
							break;
						}
					}

					firstNumber = 0;
					secondNumber = 0;
				}
				else if (c == 'd' && readOnlySpan[(i + 1)..(i + 7)].Equals("on't()", StringComparison.Ordinal))
				{
					i += 6;
					shouldDo = false;
				}
			}
			else if (c == 'd' && readOnlySpan[(i + 1)..(i + 4)].Equals("o()", StringComparison.Ordinal))
			{
				i += 3;
				shouldDo = true;
			}
		}

		return totalScore;
	}
}