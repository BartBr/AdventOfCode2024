using System.Collections;
using System.Runtime.CompilerServices;
using AdventOfCode2023.Common;

namespace AdventOfCode2023.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day08 : HappyPuzzleBase
{
	private static readonly char[] stopWord1 = new char[]{'Z','Z','Z'};
	private static readonly char[] startWord2 = new char[]{'A','A','A'};

	public override object SolvePart1(Input input)
	{
		const int totalCodes = 260000;

		scoped Span<int> codeToLineNumber = stackalloc int[totalCodes];
		scoped Span<int> leftCodeOnLineNumber = stackalloc int[input.Lines.Length];
		scoped Span<int> rightCodeOnLineNumber = stackalloc int[input.Lines.Length];

		for (int i = 2; i < input.Lines.Length; i++)
		{
			var code = input.Lines[i].AsSpan(0, 3);
			var codeAsNumber = CodeAsNumber1(code);
			codeToLineNumber[codeAsNumber] = i;

			var leftCode = input.Lines[i].AsSpan(7, 3);
			leftCodeOnLineNumber[i] = CodeAsNumber1(leftCode);

			var rightCode = input.Lines[i].AsSpan(12, 3);
			rightCodeOnLineNumber[i] = CodeAsNumber1(rightCode);
		}

		var endCode = CodeAsNumber1(new ReadOnlySpan<char>(stopWord1));
		var directionPerStep = input.Lines[0].AsSpan();
		long amountOfDirections = directionPerStep.Length;
		long step = 0;
		int directionIndex = 0;
		var nextCode = CodeAsNumber1(new ReadOnlySpan<char>(startWord2));
		while (nextCode != endCode)
		{
			if (directionIndex == amountOfDirections) directionIndex = 0;
			bool goLeft = directionPerStep[directionIndex] == 'L';
			var lineNumber = codeToLineNumber[nextCode];
			nextCode = goLeft ? leftCodeOnLineNumber[lineNumber] : rightCodeOnLineNumber[lineNumber];
			step++;
			directionIndex++;
		}
		return step;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int CodeAsNumber1(ReadOnlySpan<char> code)
	{
		var number = code[2] - 'A';
		number +=      (code[1] - 'A') * 100;
		number +=      (code[0] - 'A') * 10000;
		return number;
	}

	public override object SolvePart2(Input input)
	{
		const int totalCodes = 260000;
		const int stopCode = 'Z' - 'A';
		int total = 0;

		scoped Span<int> codeToLineNumber = stackalloc int[totalCodes];
		scoped Span<int> startCodes = stackalloc int[input.Lines.Length];
		scoped Span<long> stepsForStartCode = stackalloc long[input.Lines.Length]; //Same index as in startCodes
		int amountOfCodes = 0;
		scoped Span<int> leftCodeOnLineNumber = stackalloc int[input.Lines.Length];
		scoped Span<int> rightCodeOnLineNumber = stackalloc int[input.Lines.Length];

		for (int i = 2; i < input.Lines.Length; i++)
		{
			var code = input.Lines[i].AsSpan(0, 3);
			var codeAsNumber = CodeAsNumber1(code);
			codeToLineNumber[codeAsNumber] = i;
			if (code[2] == 'A')
			{
				startCodes[amountOfCodes] = codeAsNumber;
				amountOfCodes++;
			}

			var leftCode = input.Lines[i].AsSpan(7, 3);
			leftCodeOnLineNumber[i] = CodeAsNumber1(leftCode);

			var rightCode = input.Lines[i].AsSpan(12, 3);
			rightCodeOnLineNumber[i] = CodeAsNumber1(rightCode);
		}

		for (int i = 0; i < amountOfCodes; i++)
		{
			var directionPerStep = input.Lines[0].AsSpan();
			long amountOfDirections = directionPerStep.Length;
			long step = 0;
			int directionIndex = 0;
			var nextCode = startCodes[i];
			while (nextCode % 100 != stopCode)
			{
				if (directionIndex == amountOfDirections) directionIndex = 0;
				bool goLeft = directionPerStep[directionIndex] == 'L';
				var lineNumber = codeToLineNumber[nextCode];
				nextCode = goLeft ? leftCodeOnLineNumber[lineNumber] : rightCodeOnLineNumber[lineNumber];
				step++;
				directionIndex++;
			}
			stepsForStartCode[i] = step;
		}

		stepsForStartCode = stepsForStartCode.Slice(0, amountOfCodes);
		return LCM(stepsForStartCode);

		/*
		var directionPerStep = input.Lines[0].AsSpan();
		long amountOfDirections = directionPerStep.Length;
		long step = 0;
		int directionIndex = 0;
		bool allCodesEndWithTheStopChar = false;
		while (!allCodesEndWithTheStopChar)
		{
			if (directionIndex == amountOfDirections) directionIndex = 0;
			bool goLeft = directionPerStep[directionIndex] == 'L';

			allCodesEndWithTheStopChar = true;
			for (int i = 0; i < amountOfCodes; i++)
			{
				var currentCode = codes[i];
				var nextLineNumber = codeToLineNumber[currentCode];
				var nextCode = goLeft
					? leftCodeOnLineNumber[nextLineNumber]
					: rightCodeOnLineNumber[nextLineNumber];
				codes[i] = nextCode;

				if (allCodesEndWithTheStopChar && nextCode % 100 != stopCode) allCodesEndWithTheStopChar = false;
			}

			step++;
			directionIndex++;
		}
		*/
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	static long LCM(Span<long> numbers)
	{
		var cummulated = numbers[0];
		for (int i = 1; i < numbers.Length; i++)
		{
			cummulated = lcm(cummulated, numbers[i]);
		}

		return cummulated;
	}
	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	static long LCM(long[] numbers)
	{
		return numbers.Aggregate(lcm);
	}
	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	static long lcm(long a, long b)
	{
		return Math.Abs(a * b) / GCD(a, b);
	}
	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	static long GCD(long a, long b)
	{
		return b == 0 ? a : GCD(b, a % b);
	}
}