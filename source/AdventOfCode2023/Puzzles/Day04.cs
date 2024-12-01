using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AdventOfCode2023.Common;

namespace AdventOfCode2023.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day04 : HappyPuzzleBase
{
	private const int firstNumberOffset = 10;
	private const int firstNumbers = 10;
	private const int lastNumbers = 25;
	private const int lastNumberOffset = 42;
	// private const int firstNumberOffset = 8;
	// private const int firstNumbers = 5;
	// private const int lastNumbers = 8;
	// private const int lastNumberOffset = 25;
	public override object SolvePart1(Input input)
	{
		int total = 0;
		for (var i = 0; i < input.Lines.Length; i++)
		{
			var winningNumbers = new HashSet<int>();
			var score = 0;
			for (var j = 0; j < firstNumbers; j++)
			{
				winningNumbers.Add(AsNumber(input.Lines[i][firstNumberOffset + j * 3], input.Lines[i][firstNumberOffset + j * 3 + 1]));
			}
			for (var j = 0; j < lastNumbers; j++)
			{
				var number = AsNumber(input.Lines[i][lastNumberOffset + j * 3], input.Lines[i][lastNumberOffset + j * 3 + 1]);
				if (winningNumbers.Contains(number))
				{
					score *= 2;
					if (score == 0) score = 1;
				}
			}
			total += score;
		}
		return total;
	}

	private static int AsNumber(char p0, char p1)
	{
		return p0 == ' '
			? p1 - '0'
			: (p0 - '0') * 10 + (p1 - '0');
	}

	public override object SolvePart2(Input input)
	{
		int total = 0;
		var copies = new int[198];
		for (var i = 0; i < input.Lines.Length; i++)
		{
			var winningNumbers = new HashSet<int>();
			var score = 0;
			for (var j = 0; j < firstNumbers; j++)
			{
				winningNumbers.Add(AsNumber(input.Lines[i][firstNumberOffset + j * 3], input.Lines[i][firstNumberOffset + j * 3 + 1]));
			}

			var correctNumbers = 0;
			for (var j = 0; j < lastNumbers; j++)
			{
				var number = AsNumber(input.Lines[i][lastNumberOffset + j * 3], input.Lines[i][lastNumberOffset + j * 3 + 1]);
				if (winningNumbers.Contains(number))
				{
					correctNumbers++;
					if (i + correctNumbers < input.Lines.Length)
					{
						copies[i + correctNumbers] += copies[i] + 1;
					}
				}
			}
			total += (copies[i] +1);
		}
		return total;
	}
}