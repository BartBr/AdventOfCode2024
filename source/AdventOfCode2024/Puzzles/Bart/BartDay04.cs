using System.Buffers;
using System.Text.RegularExpressions;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Bart;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class BartDay04 : HappyPuzzleBase
{
	public override object SolvePart1(Input input)
	{
		var rows = input.Lines.Length;
		var columns = input.Lines[0].Length;

		long sum = 0;

		sum += CountHorizontal(input, rows, columns);
		sum += CountVertical(input, rows, columns);
		sum += CountDiagonalLeftDown(input, rows, columns);
		sum += CountDiagonalLeftUp(input, rows, columns);
		return sum;
	}

	private static long CountHorizontal(Input input, int rows, int columns)
	{
		long sum = 0;
		for (var row = 0; row < rows; row++)
		{
			var span = input.Lines[row].AsSpan();
			var count = CountValidPhrases(span);

			sum += count;
		}
		return sum;
	}

	private static long CountVertical(Input input, int rows, int columns)
	{
		long sum = 0;
		Span<char> vertical = stackalloc char[rows];
		for (var col = 0; col < columns; col++)
		{
			for (var row = 0; row < rows; row++)
			{
				vertical[row] = input.Lines[row][col];
			}

			var count = CountValidPhrases(vertical);
			sum += count;
		}
		return sum;
	}

	// Left Top to right down
	private static long CountDiagonalLeftDown(Input input, int rows, int cols)
	{
		long sum=0;
		Span<char> diagonal = stackalloc char[rows + cols];

		for (var d = 0; d < rows + cols - 1; d++)
		{
			var diagonalIndex = 0;
			for (var row = 0; row < rows; row++)
			{
				var col = d - row;
				if (col >= 0 && col < cols)
				{
					diagonal[diagonalIndex++] = input.Lines[row][col];
				}
			}
			var span = diagonal[..(diagonalIndex)];
			var count = CountValidPhrases(span);
			sum += count;
		}

		return sum;
	}

	// Left Bottom to right up
	private static long CountDiagonalLeftUp(Input input, int rows, int cols)
	{
		long sum=0;
		Span<char> diagonal = stackalloc char[rows + cols];
		for (var d = 0; d < rows + cols - 1; d++)
		{
			var diagonalIndex = 0;
			for (var row = 0; row < rows; row++)
			{
				var col = d - (rows - 1 - row);
				if (col >= 0 && col < cols)
				{
					diagonal[diagonalIndex++] = input.Lines[row][col];
				}
			}

			var span = diagonal[..(diagonalIndex)];
			var count = CountValidPhrases(span);
			sum += count;
		}

		return sum;
	}


	[GeneratedRegex("XMAS", RegexOptions.None)]
	private static partial Regex XmasGeneratedRegex();

	[GeneratedRegex("SAMX", RegexOptions.None)]
	private static partial Regex SamxGeneratedRegex();

	private static long CountValidPhrases(ReadOnlySpan<char> span)
	{
		return XmasGeneratedRegex().Count(span) + SamxGeneratedRegex().Count(span);
	}

	public override object SolvePart2(Input input)
	{
		var rows = input.Lines.Length;
		var cols = input.Lines[0].Length;

		long sum = 0;

		for (var row = 1; row < rows-1; row++)
		{
			for (var col = 1; col < cols-1; col++)
			{
				if(input.Lines[row][col] == 'A' && IsAnX(input, row, col))
				{
					sum++;
				}
			}
		}

		return sum;
	}

	private static bool IsAnX(Input input, int row, int col)
	{
		var diag1 = input.Lines[row-1][col-1] == 'M' && input.Lines[row+1][col+1] == 'S'
		            || input.Lines[row-1][col-1] == 'S' && input.Lines[row+1][col+1] == 'M';

		var diag2 = input.Lines[row-1][col+1] == 'M' && input.Lines[row+1][col-1] == 'S'
		            || input.Lines[row-1][col+1] == 'S' && input.Lines[row+1][col-1] == 'M';
		return diag2 && diag1;
	}
}