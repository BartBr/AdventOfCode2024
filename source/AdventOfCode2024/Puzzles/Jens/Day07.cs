using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jens;

public class Day07 : HappyPuzzleBase<long>
{
	public override long SolvePart1(Input input)
	{
		scoped Span<long> operandsBuffer = stackalloc long[20];

		var sum = 0L;
		foreach (var line in input.Lines)
		{
			long expectedResult = 0;

			var characterIndex = 0;
			for (; characterIndex < line.Length; characterIndex++)
			{
				var c = line[characterIndex];
				if (c == ':')
				{
					break;
				}

				expectedResult = expectedResult * 10 + (c - '0');
			}

			characterIndex += 2;

			var operandIndex = 0;
			ref var operand = ref operandsBuffer[operandIndex];
			operand = 0;
			for (; characterIndex < line.Length; characterIndex++)
			{
				var c = line[characterIndex];
				if (c == ' ')
				{
					operand = ref operandsBuffer[++operandIndex];
					operand = 0;
				}
				else
				{
					operand = operand * 10 + (c - '0');
				}
			}

			if (Part1_PermutateOperatorsAndValidate(operandsBuffer.Slice(1, operandIndex), 0, operandsBuffer[0], expectedResult))
			{
				sum += expectedResult;
			}
		}

		return sum;
	}

	private static bool Part1_PermutateOperatorsAndValidate(Span<long> operandsBuffer, int operandsBufferIndex, long currentResult, long expectedResult)
	{
		if (currentResult > expectedResult)
		{
			return false;
		}

		if (operandsBufferIndex == operandsBuffer.Length)
		{
			if (currentResult == expectedResult)
			{
				return true;
			}

			return false;
		}

		var currentOperand = operandsBuffer[operandsBufferIndex];
		++operandsBufferIndex;

		return Part1_PermutateOperatorsAndValidate(operandsBuffer, operandsBufferIndex, currentResult * currentOperand, expectedResult)
		       || Part1_PermutateOperatorsAndValidate(operandsBuffer, operandsBufferIndex, currentResult + currentOperand, expectedResult);
	}

	public override long SolvePart2(Input input)
	{
		scoped Span<long> operandsBuffer = stackalloc long[20];

		var sum = 0L;
		foreach (var line in input.Lines)
		{
			long expectedResult = 0;

			var characterIndex = 0;
			for (; characterIndex < line.Length; characterIndex++)
			{
				var c = line[characterIndex];
				if (c == ':')
				{
					break;
				}

				expectedResult = expectedResult * 10 + (c - '0');
			}

			characterIndex += 2;

			var operandIndex = 0;
			ref var operand = ref operandsBuffer[operandIndex];
			operand = 0;
			for (; characterIndex < line.Length; characterIndex++)
			{
				var c = line[characterIndex];
				if (c == ' ')
				{
					operand = ref operandsBuffer[++operandIndex];
					operand = 0;
				}
				else
				{
					operand = operand * 10 + (c - '0');
				}
			}

			var slicedOperandsBuffer = operandsBuffer.Slice(1, operandIndex);
			if (Part2_PermutateOperatorsAndValidate(ref slicedOperandsBuffer, 0, operandsBuffer[0], expectedResult))
			{
				sum += expectedResult;
			}
		}

		return sum;
	}

	private static bool Part2_PermutateOperatorsAndValidate(ref Span<long> operandsBuffer, int operandsBufferIndex, long currentResult, long expectedResult)
	{
		if (currentResult > expectedResult)
		{
			return false;
		}

		if (operandsBufferIndex == operandsBuffer.Length)
		{
			if (currentResult == expectedResult)
			{
				return true;
			}

			return false;
		}

		var currentOperand = operandsBuffer[operandsBufferIndex];
		++operandsBufferIndex;

		return Part2_PermutateOperatorsAndValidate(ref operandsBuffer, operandsBufferIndex, currentResult * Part2_CalculateOffsetMultiplier(currentOperand) + currentOperand, expectedResult)
		       ||Part2_PermutateOperatorsAndValidate(ref operandsBuffer, operandsBufferIndex, currentResult * currentOperand, expectedResult)
		       || Part2_PermutateOperatorsAndValidate(ref operandsBuffer, operandsBufferIndex, currentResult + currentOperand, expectedResult);
	}

	private static long Part2_CalculateOffsetMultiplier(long currentOperand)
	{
		return (long) Math.Pow(10, ((int)Math.Log10(Math.Abs(currentOperand)) + 1));
	}
}