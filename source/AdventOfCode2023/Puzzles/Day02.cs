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
public partial class Day02 : HappyPuzzleBase
{
	// public override object SolvePart1_Regex(Input input)
	// {
	// 	var total = 0;
	// 	for (var game = 0; game < input.Lines.Length; game++)
	// 	{
	// 		if (!MyRegex().IsMatch(input.Lines[game]))
	// 		{
	// 			total += (game + 1);
	// 		}
	// 	}
	// 	return total;
	// }

	public override object SolvePart1(Input input)
	{
		var total = 0;
		for (var game = 0; game < input.Lines.Length; game++)
		{
			if (IsValidGame(input.Lines[game]))
			{
				total += (game + 1);
			}
		}
		return total;
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private bool IsValidGame(string game)
	{
		int i = 8; //first character where a number can start

		while (i < game.Length)
		{
			if (game[i] >= '0' && game[i] <= '9')
			{
				int number;
				if (game[i+1] >= '0' && game[i+1] <= '9')
				{
					//Case 2 number
					number = (game[i] - '0') * 10 + (game[i + 1] - '0');
					i += 3;
				}
				else
				{
					//Case 1 number
					number = (game[i] - '0');
					i += 2;
				}

				if (game[i] == 'r' && number > 12) return false;
				else if (game[i] == 'g' && number > 13) return false;
				else if (game[i] == 'b' && number > 14) return false;
			}
			i++;
		}
		return true;
	}

	public override object SolvePart2(Input input)
	{
		var total = 0;
		for (var game = 0; game < input.Lines.Length; game++)
		{
			total += CalculateGamePower(input.Lines[game]);
		}
		return total;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private int CalculateGamePower(string game)
	{
		int maxRed = 0;
		int maxGreen = 0;
		int maxBlue = 0;

		int i = 8; //first character where a number can start

		while (i < game.Length)
		{
			if (game[i] >= '0' && game[i] <= '9')
			{
				int number;
				if (game[i+1] >= '0' && game[i+1] <= '9')
				{
					//Case 2 number
					number = (game[i] - '0') * 10 + (game[i + 1] - '0');
					i += 3;
				}
				else
				{
					//Case 1 number
					number = (game[i] - '0');
					i += 2;
				}

				if (game[i] == 'r' && number > maxRed) maxRed = number;
				else if (game[i] == 'g' && number > maxGreen) maxGreen = number;
				else if (game[i] == 'b' && number > maxBlue) maxBlue = number;
			}
			i++;
		}
		return maxGreen * maxBlue * maxRed;
	}

	//[GeneratedRegex("(20 |1[3,4,5,6,7,8,9] red|1[4,5,6,7,8,9] green|1[5,6,7,8,9] blue)", RegexOptions.Compiled)]
	//private static partial Regex MyRegex();
}