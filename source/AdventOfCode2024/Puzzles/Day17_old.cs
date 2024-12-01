using System.Runtime.CompilerServices;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class __Day17 : HappyPuzzleBase
{
	enum Direction
	{
		Unknown = 0,
		Up = 1,
		Down = 2,
		Left = 3,
		Right = 4,
	}

	private readonly record struct Tile(int Row, int Column, Direction IncomingDirection, int SameDirCount, int StepsTaken);

	public override object SolvePart1(Input input)
	{
		var inputLines = input.Lines;
		var columns = inputLines[0].Length;
		var rows = inputLines.Length;

		scoped Span<int> cityBoard = stackalloc int[rows * columns];
		for (var i = 0; i < rows; i++)
		{
			for (var j = 0; j < columns; j++)
			{
				cityBoard[GetIndex(i, j, columns)] = inputLines[i][j] - '0';
			}
		}
		//PrintBoard(cityBoard, rows, columns);

		var hashes = new HashSet<ulong>();
		var priorityQueue = new PriorityQueue<Tile,int>(1474836);
		//priorityQueue.Enqueue(new Tile(0,0, Direction.Unknown, 0,0), 0);
		priorityQueue.Enqueue(new Tile(0,1, Direction.Right, 1,1), cityBoard[GetIndex(0,1,columns)]);
		priorityQueue.Enqueue(new Tile(1,0, Direction.Down, 1,1), cityBoard[GetIndex(1,0,columns)]);

		while (priorityQueue.TryDequeue(out var tile, out var heat))
		{
			// 184467440737 0955 1615
			// 3 digits columns, 3 digits rows, 1 digit
			var hashedTile =
				(ulong) tile.Row
				+ (ulong) tile.Column * 10000
				+ (ulong) tile.IncomingDirection * 100000000
				+ (ulong) tile.SameDirCount * 1000000000
				+ (ulong) heat * 10000000000;
			if (hashes.Contains(hashedTile))
			{
				continue;
			}
			hashes.Add(hashedTile);

			if (tile.Row == rows - 1 && tile.Column == columns - 1)
			{
				Console.WriteLine($"Found heat, hash size: {hashes.Count}");
				Console.WriteLine($"Found heat, priority queue size: {priorityQueue.UnorderedItems.Count}");
				return heat;
			}

			// if(tile.StepsTaken > (140 + 140) *2 )
			// {
			// 	continue;
			// }

			//GO down
			if (tile.IncomingDirection != Direction.Up && (tile.IncomingDirection != Direction.Down || tile.SameDirCount < 3) && tile.Row < rows -1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Down ? tile.SameDirCount + 1 : 1;
				var nextRow = tile.Row + 1;
				var nextCityHeat = cityBoard[GetIndex(nextRow, tile.Column,columns)];
				priorityQueue.Enqueue(new Tile(nextRow, tile.Column, Direction.Down, sameDirCount, tile.StepsTaken +1), heat + nextCityHeat);
			}

			//GO up
			if (tile.IncomingDirection != Direction.Down && (tile.IncomingDirection != Direction.Up || tile.SameDirCount < 3) && tile.Row >= 1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Up ? tile.SameDirCount + 1 : 1;
				var nextRow = tile.Row - 1;
				var nextCityHeat = cityBoard[GetIndex(nextRow, tile.Column,columns)];
				priorityQueue.Enqueue(new Tile(nextRow, tile.Column, Direction.Up, sameDirCount, tile.StepsTaken +1), heat + nextCityHeat);
			}

			// GO Right
			if (tile.IncomingDirection != Direction.Left && (tile.IncomingDirection != Direction.Right || tile.SameDirCount < 3) && tile.Column < columns -1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Right ? tile.SameDirCount + 1 : 1;
				var nextColumn = tile.Column + 1;
				var nextCityHeat = cityBoard[GetIndex(tile.Row, nextColumn,columns)];
				priorityQueue.Enqueue(new Tile(tile.Row, nextColumn, Direction.Right, sameDirCount, tile.StepsTaken +1), heat + nextCityHeat);
			}

			//GO left
			if (tile.IncomingDirection != Direction.Right && (tile.IncomingDirection != Direction.Left || tile.SameDirCount < 3) && tile.Column >= 1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Left ? tile.SameDirCount + 1 : 1;
				var nextColumn = tile.Column - 1;
				var nextCityHeat = cityBoard[GetIndex(tile.Row, nextColumn,columns)];
				priorityQueue.Enqueue(new Tile(tile.Row, nextColumn, Direction.Left, sameDirCount, tile.StepsTaken +1), heat + nextCityHeat);
			}
		}

		return -1;
	}

	private static void PrintBoard(ReadOnlySpan<int> cityBoard, int rows, int columns)
	{
		Console.WriteLine();
		for (var i = 0; i < rows; i++)
		{
			for (var j = 0; j < columns; j++)
			{
				Console.Write($"{cityBoard[GetIndex(i,j,columns)]}-");
			}
			Console.WriteLine();
		}
		Console.WriteLine();
	}

	public override object SolvePart2(Input input)
	{
		var inputLines = input.Lines;
		var columns = inputLines[0].Length;
		var rows = inputLines.Length;

		scoped Span<int> cityBoard = stackalloc int[rows * columns];
		for (var i = 0; i < rows; i++)
		{
			for (var j = 0; j < columns; j++)
			{
				cityBoard[GetIndex(i, j, columns)] = inputLines[i][j] - '0';
			}
		}
		//PrintBoard(cityBoard, rows, columns);

		var hashes = new HashSet<ulong>();
		var priorityQueue = new PriorityQueue<Tile,int>(1474836);
		priorityQueue.Enqueue(new Tile(0,1, Direction.Right, 1,1), cityBoard[GetIndex(0,1,columns)]);
		priorityQueue.Enqueue(new Tile(1,0, Direction.Down, 1,1), cityBoard[GetIndex(1,0,columns)]);

		while (priorityQueue.TryDequeue(out var tile, out var heat))
		{
			// 184467440737 0955 1615
			// 3 digits columns, 3 digits rows, 1 digit
			var hashedTile =
				(ulong) tile.Row
				+ (ulong) tile.Column * 10000
				+ (ulong) tile.IncomingDirection * 100000000
				+ (ulong) tile.SameDirCount * 1000000000
				+ (ulong) heat * 100000000000;
			if (hashes.Contains(hashedTile))
			{
				continue;
			}
			hashes.Add(hashedTile);

			if (tile.Row == rows - 1 && tile.Column == columns - 1)
			{
				Console.WriteLine($"Found heat, hash size: {hashes.Count}");
				Console.WriteLine($"Found heat, priority queue size: {priorityQueue.UnorderedItems.Count}");
				return heat;
			}

			if(tile.StepsTaken > (140 + 140) *2 )
			{
				continue;
			}

			//GO down
			if ((tile.IncomingDirection == Direction.Down || tile.SameDirCount >= 4) && tile.IncomingDirection != Direction.Up && (tile.IncomingDirection != Direction.Down || tile.SameDirCount < 10) && tile.Row < rows -1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Down ? tile.SameDirCount + 1 : 1;
				var nextRow = tile.Row + 1;
				var nextCityHeat = cityBoard[GetIndex(nextRow, tile.Column,columns)];
				priorityQueue.Enqueue(new Tile(nextRow, tile.Column, Direction.Down, sameDirCount, tile.StepsTaken +1), heat + nextCityHeat);
			}

			//GO up
			if ((tile.IncomingDirection == Direction.Up || tile.SameDirCount >= 4) && tile.IncomingDirection != Direction.Down && (tile.IncomingDirection != Direction.Up || tile.SameDirCount < 10) && tile.Row >= 1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Up ? tile.SameDirCount + 1 : 1;
				var nextRow = tile.Row - 1;
				var nextCityHeat = cityBoard[GetIndex(nextRow, tile.Column,columns)];
				priorityQueue.Enqueue(new Tile(nextRow, tile.Column, Direction.Up, sameDirCount, tile.StepsTaken +1), heat + nextCityHeat);
			}

			// GO Right
			if ((tile.IncomingDirection == Direction.Right || tile.SameDirCount >= 4) && tile.IncomingDirection != Direction.Left && (tile.IncomingDirection != Direction.Right || tile.SameDirCount < 10) && tile.Column < columns -1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Right ? tile.SameDirCount + 1 : 1;
				var nextColumn = tile.Column + 1;
				var nextCityHeat = cityBoard[GetIndex(tile.Row, nextColumn,columns)];
				priorityQueue.Enqueue(new Tile(tile.Row, nextColumn, Direction.Right, sameDirCount, tile.StepsTaken +1), heat + nextCityHeat);
			}

			//GO left
			if ((tile.IncomingDirection == Direction.Left || tile.SameDirCount >= 4) && tile.IncomingDirection != Direction.Right && (tile.IncomingDirection != Direction.Left || tile.SameDirCount < 10) && tile.Column >= 1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Left ? tile.SameDirCount + 1 : 1;
				var nextColumn = tile.Column - 1;
				var nextCityHeat = cityBoard[GetIndex(tile.Row, nextColumn,columns)];
				priorityQueue.Enqueue(new Tile(tile.Row, nextColumn, Direction.Left, sameDirCount, tile.StepsTaken +1), heat + nextCityHeat);
			}
		}

		return -1;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static int GetIndex(int i, int j, int columns)
	{
		return i * columns + j;
	}


	//left, right, top, bottom can be 0 not visited from the left, to max 3
}