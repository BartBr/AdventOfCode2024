using System.Runtime.CompilerServices;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day13 : HappyPuzzleBase
{
	public override object SolvePart1(Input input)
	{
		int total = 0;
		int pattern = 0;
		var currentPatternRows = 0;
		var currentPatternStartRow = 0;
		for (int i = 0; i < input.Lines.Length; i++)
		{
			if (input.Lines[i].Length == 0)
			{
				//Console.Write($"pattern {(++pattern):0000} :");
				currentPatternRows = i - currentPatternStartRow;
				total += ProcessPattern1(input.Lines, currentPatternStartRow, currentPatternRows);
				currentPatternStartRow = i+1;
				//Console.WriteLine();
			}
		}
		//Console.Write($"pattern {(++pattern):0000} :");
		currentPatternRows = input.Lines.Length - currentPatternStartRow;
		total += ProcessPattern1(input.Lines, currentPatternStartRow, currentPatternRows);

		return total;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private int ProcessPattern1(string[] lines, int rowStartIndex, int rows)
	{
		var patternTotal = 0;

		var columns = lines[rowStartIndex].Length;

		for (var i = 1; i < rows; i++)
		{
			if (AreRowsEqual(lines[rowStartIndex+i-1], lines[rowStartIndex+i]))
			{
				var rowA = i - 2;
				var rowB = i + 1;
				var rowsEqual = true;
				while (rowA >= 0 && rowB < rows && rowsEqual)
				{
					rowsEqual = AreRowsEqual(lines[rowStartIndex + rowA], lines[rowStartIndex + rowB]);
					rowA--;
					rowB++;
				}
				if(rowsEqual)
				{
					return i * 100;
					//Console.Write($"{i} row");
				}
			}
		}

		for (var j = 1; j < columns; j++)
		{
			if (AreColumnsEqual(lines, j-1, j, rows, rowStartIndex))
			{
				var columnA = j - 2;
				var columnB = j + 1;
				var columnsEqual = true;
				while (columnA >= 0 && columnB < columns && columnsEqual)
				{
					columnsEqual = AreColumnsEqual(lines,columnA, columnB, rows, rowStartIndex);
					columnA--;
					columnB++;
				}
				if(columnsEqual)
				{
					return j;
					//Console.Write($"{j} column");
				}
			}
		}
		return patternTotal;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static bool AreRowsEqual(string a, string b)
	{
		return a.Equals(b);
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static bool AreColumnsEqual(string[] lines, int columnIndexA, int columnIndexB, int rows,int rowOffset)
	{
		for (int row = 0; row < rows; row++)
		{
			if (lines[row+rowOffset][columnIndexA] != lines[row+rowOffset][columnIndexB])
			{
				return false;
			}
		}
		return true;
	}

	public override object SolvePart2(Input input)
	{
		int total = 0;
		int pattern = 0;
		var currentPatternRows = 0;
		var currentPatternStartRow = 0;
		for (int i = 0; i < input.Lines.Length; i++)
		{
			if (input.Lines[i].Length == 0)
			{
				//Console.Write($"pattern {(++pattern):0000} :");
				currentPatternRows = i - currentPatternStartRow;
				total += ProcessPattern2(input.Lines, currentPatternStartRow, currentPatternRows);
				currentPatternStartRow = i+1;
				//Console.WriteLine();
			}
		}
		//Console.Write($"pattern {(++pattern):0000} :");
		currentPatternRows = input.Lines.Length - currentPatternStartRow;
		total += ProcessPattern2(input.Lines, currentPatternStartRow, currentPatternRows);

		return total;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private int ProcessPattern2(string[] lines, int rowStartIndex, int rows)
	{
		var patternTotal = 0;

		var columns = lines[rowStartIndex].Length;

		for (var i = 1; i < rows; i++)
		{
			var smudges = 0;
			var (rowsEqual, hasSmudge) = AreRowsEqual2(lines[rowStartIndex + i - 1], lines[rowStartIndex + i]);
			if (hasSmudge) smudges++;

			if (rowsEqual)
			{
				var rowA = i - 2;
				var rowB = i + 1;
				while (rowA >= 0 && rowB < rows && rowsEqual && smudges<=1)
				{
					var (newRowsEqual, newHasSmudge) = AreRowsEqual2(lines[rowStartIndex + rowA], lines[rowStartIndex + rowB]);
					rowsEqual = newRowsEqual;
					if (newHasSmudge) smudges++;
					rowA--;
					rowB++;
				}
				if(rowsEqual && smudges==1)
				{
					return i * 100;
					//Console.Write($"{i} row");
				}
			}
		}

		for (var j = 1; j < columns; j++)
		{
			var smudges = 0;
			var (columnsEqual, hasSmudge) = AreColumnsEqual2(lines, j - 1, j, rows, rowStartIndex);
			if (hasSmudge) smudges++;
			if (columnsEqual)
			{
				var columnA = j - 2;
				var columnB = j + 1;

				while (columnA >= 0 && columnB < columns && columnsEqual && smudges<=1)
				{
					var (newColumnsEqual, newHasSmudge) = AreColumnsEqual2(lines,columnA, columnB, rows, rowStartIndex);
					columnsEqual = newColumnsEqual;
					if (newHasSmudge) smudges++;
					columnA--;
					columnB++;
				}
				if(columnsEqual && smudges==1)
				{
					return j;
					//Console.Write($"{j} column");
				}
			}
		}

		return patternTotal;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static (bool equal, bool hasSmudge) AreRowsEqual2(string a, string b)
	{
		var hasSmudge = false;
		for (int column = 0; column < a.Length; column++)
		{
			if (a[column] != b[column])
			{
				if (!hasSmudge)
				{
					hasSmudge = true;
				}
				else
				{
					return (false,false);
				}
			}
		}
		return (true,hasSmudge);
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static (bool equal, bool hasSmudge) AreColumnsEqual2(string[] lines, int columnIndexA, int columnIndexB, int rows,int rowOffset)
	{
		var hasSmudge = false;
		for (int row = 0; row < rows; row++)
		{
			if (lines[row+rowOffset][columnIndexA] != lines[row+rowOffset][columnIndexB])
			{
				if (!hasSmudge)
				{
					hasSmudge = true;
				}
				else
				{
					return (false,false);
				}
			}
		}
		return (true,hasSmudge);
	}

	// [MethodImpl(MethodImplOptions.AggressiveOptimization)]
	// private static int GetIndex(int i, int j, int columns)
	// {
	// 	return i * columns + j;
	// }
}