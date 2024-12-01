using System.Buffers;
using System.Collections;
using System.Data.Common;
using System.Dynamic;
using System.Runtime.CompilerServices;
using AdventOfCode2023.Common;

namespace AdventOfCode2023.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day11 : HappyPuzzleBase
{
	private const char GALAXY = '#';
	public override object SolvePart1(Input input)
	{
		var inputRows = input.Lines.Length;
		var inputColumns = input.Lines[0].Length;

		scoped Span<long> galaxyPositionRows = stackalloc long[500];
		scoped Span<long> galaxyPositionColumns = stackalloc long[500];

		FillGalaxyFromInput(input.Lines, galaxyPositionRows, galaxyPositionColumns, 1, out var rows, out var columns, out var galaxies);

		galaxyPositionRows = galaxyPositionRows.Slice(0, galaxies);
		galaxyPositionColumns = galaxyPositionColumns.Slice(0, galaxies);

		long totalDistance = 0;
		for (int a = 0; a < galaxies - 1; a++)
		{
			for (int b = a + 1; b < galaxies; b++)
			{
				var distance = Math.Abs(galaxyPositionColumns[b] - galaxyPositionColumns[a]) + Math.Abs(galaxyPositionRows[b] - galaxyPositionRows[a]);
				totalDistance += distance;
				//Console.WriteLine($"Compare {a:00} with {b:00}: distance: {distance}");
			}
		}

		return totalDistance;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private void FillGalaxyFromInput(string[] input, scoped Span<long> galaxyPositionRows, scoped Span<long> galaxyPositionColumns, long increment, out long rows, out long columns, out int galaxies)
	{
		int voidRows = 0;
		int voidColumns = 0;

		// read line per line. void increase the rows
		galaxies = 0;
		rows = 0;
		foreach (var row in input)
		{
			var isVoid = true;
			for (int j = 0; j < row.Length; j++)
			{
				if (row[j] == GALAXY)
				{
					isVoid = false;
					galaxyPositionRows[galaxies] = rows;
					galaxyPositionColumns[galaxies] = j;
					galaxies++;
				}
			}

			if (isVoid)
			{
				rows+=increment;
				voidRows++;
			}
			rows++;
		}

		// now increase the rows for each galaxy with a column greater than a void
		columns = 0;
		for (int j = 0; j < input[0].Length; j++)
		{
			var isVoid = true;
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i][j] == GALAXY) isVoid = false;
			}

			if (isVoid)
			{
				for (int g = 0; g < galaxies; g++)
				{
					if (galaxyPositionColumns[g] > columns)
					{
						galaxyPositionColumns[g]+=increment;
					}
				}
				columns+=increment;
				voidColumns++;
			}
			columns++;
		}

		//Console.WriteLine($"void rows: {voidRows}, rows:{rows}");
		//Console.WriteLine($"void columns: {voidColumns}, columns: {columns}");
	}

	public override object SolvePart2(Input input)
	{
		var inputRows = input.Lines.Length;
		var inputColumns = input.Lines[0].Length;

		scoped Span<long> galaxyPositionRows = stackalloc long[500]; //they can have max: inputRows * inputColumns, but decreased for faster exc
		scoped Span<long> galaxyPositionColumns = stackalloc long[500];

		FillGalaxyFromInput(input.Lines, galaxyPositionRows, galaxyPositionColumns, 1000000-1, out var rows, out var columns, out var galaxies);

		galaxyPositionRows = galaxyPositionRows.Slice(0, galaxies);
		galaxyPositionColumns = galaxyPositionColumns.Slice(0, galaxies);

		long totalDistance = 0;
		for (int a = 0; a < galaxies - 1; a++)
		{
			for (int b = a + 1; b < galaxies; b++)
			{
				var distance = Math.Abs(galaxyPositionColumns[b] - galaxyPositionColumns[a]) + Math.Abs(galaxyPositionRows[b] - galaxyPositionRows[a]);
				totalDistance += distance;
				//Console.WriteLine($"Compare {a:00} with {b:00}: distance: {distance}");
			}
		}
		return totalDistance;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static int GetIndex(int i, int j, int columns)
	{
		return i * columns + j;
	}
}