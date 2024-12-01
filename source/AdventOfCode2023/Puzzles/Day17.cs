using System.Runtime.CompilerServices;
using AdventOfCode2023.Common;

namespace AdventOfCode2023.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day17 : HappyPuzzleBase
{
	private const int maxPrioLength = 3000;

	enum Direction: ushort
	{
		Up = 1,
		Down = 2,
		Left = 3,
		Right = 4,
	}

	private readonly record struct Tile(int Row, int Column, Direction IncomingDirection, int SameDirCount, int Heat);

	public override object SolvePart1(Input input)
	{
		return -1;
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

		scoped Span<bool> hasVisiteds = stackalloc bool[columns * rows * 100];

		scoped Span<ulong> priorityQueue = stackalloc ulong[maxPrioLength+10];
		var rightTile = new Tile(0,1, Direction.Right, 1, cityBoard[GetIndex(0,1,columns)]);
		priorityQueue[0] = Compress(rightTile);
		var downTile = new Tile(1,0, Direction.Down, 1, cityBoard[GetIndex(1,0,columns)]);
		priorityQueue[1] = Compress(downTile);
		var priorityQueueLength = 2;

		while (priorityQueueLength > 0)
		{
			var hashedTile = priorityQueue[0];
			var tile = Decompress(hashedTile);
			var hasVisitedCompressed = CompressVisit(tile,columns);
			if(hasVisiteds[hasVisitedCompressed])
			{
				RemoveFirst(priorityQueue, priorityQueueLength);
				priorityQueueLength--; //remove the current max value
				continue;
			}

			hasVisiteds[hasVisitedCompressed] = true;

			if (tile.Row == rows - 1 && tile.Column == columns - 1)
			{
				return tile.Heat;
			}

			int addedTiles = 0;

			//GO down
			if (tile.IncomingDirection != Direction.Up && (tile.IncomingDirection != Direction.Down || tile.SameDirCount < 3) && tile.Row < rows -1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Down ? tile.SameDirCount + 1 : 1;
				var nextRow = tile.Row + 1;
				var nextCityHeat = cityBoard[GetIndex(nextRow, tile.Column,columns)];

				var nextTile = new Tile(nextRow, tile.Column, Direction.Down, sameDirCount, tile.Heat + nextCityHeat);
				priorityQueue[priorityQueueLength++] = Compress(nextTile);
				addedTiles++;
			}

			//GO up
			if (tile.IncomingDirection != Direction.Down && (tile.IncomingDirection != Direction.Up || tile.SameDirCount < 3) && tile.Row >= 1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Up ? tile.SameDirCount + 1 : 1;
				var nextRow = tile.Row - 1;
				var nextCityHeat = cityBoard[GetIndex(nextRow, tile.Column,columns)];
				var nextTile = new Tile(nextRow, tile.Column, Direction.Up, sameDirCount, tile.Heat + nextCityHeat);
				priorityQueue[priorityQueueLength++] = Compress(nextTile);
				addedTiles++;
			}

			// GO Right
			if (tile.IncomingDirection != Direction.Left && (tile.IncomingDirection != Direction.Right || tile.SameDirCount < 3) && tile.Column < columns -1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Right ? tile.SameDirCount + 1 : 1;
				var nextColumn = tile.Column + 1;
				var nextCityHeat = cityBoard[GetIndex(tile.Row, nextColumn,columns)];
				var nextTile = new Tile(tile.Row, nextColumn, Direction.Right, sameDirCount, tile.Heat + nextCityHeat);
				priorityQueue[priorityQueueLength++] = Compress(nextTile);
				addedTiles++;
			}

			//GO left
			if (tile.IncomingDirection != Direction.Right && (tile.IncomingDirection != Direction.Left || tile.SameDirCount < 3) && tile.Column >= 1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Left ? tile.SameDirCount + 1 : 1;
				var nextColumn = tile.Column - 1;
				var nextCityHeat = cityBoard[GetIndex(tile.Row, nextColumn,columns)];
				var nextTile = new Tile(tile.Row, nextColumn, Direction.Left, sameDirCount, tile.Heat + nextCityHeat);
				priorityQueue[priorityQueueLength++] = Compress(nextTile);
				addedTiles++;
			}

			RemoveFirst(priorityQueue,priorityQueueLength);
			priorityQueueLength--; //remove the current max value

			SortLastFourIntoArray(priorityQueue,priorityQueueLength, addedTiles);

			if (priorityQueueLength > maxPrioLength)
			{
				priorityQueueLength = maxPrioLength;
			}
		}

		return -1;
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

		scoped Span<bool> hasVisiteds = stackalloc bool[columns * rows *4];

		scoped Span<ulong> priorityQueue = stackalloc ulong[maxPrioLength+10];
		var rightTile = new Tile(0,1, Direction.Right, 1, cityBoard[GetIndex(0,1,columns)]);
		priorityQueue[0] = Compress(rightTile);
		var downTile = new Tile(1,0, Direction.Down, 1, cityBoard[GetIndex(1,0,columns)]);
		priorityQueue[1] = Compress(downTile);
		var priorityQueueLength = 2;

		while (priorityQueueLength > 0)
		{
			var hashedTile = priorityQueue[0];
			var tile = Decompress(hashedTile);
			var hasVisitedCompressed = CompressVisit2(tile,columns);
			if(hasVisiteds[hasVisitedCompressed])
			{
				RemoveFirst(priorityQueue, priorityQueueLength);
				priorityQueueLength--; //remove the current max value
				continue;
			}

			hasVisiteds[hasVisitedCompressed] = true;

			if (tile.Row == rows - 1 && tile.Column == columns - 1)
			{
				return tile.Heat;
			}

			int addedTiles = 0;

			//GO down
			if ((tile.IncomingDirection == Direction.Down || tile.SameDirCount >= 4) && tile.IncomingDirection != Direction.Up && (tile.IncomingDirection != Direction.Down || tile.SameDirCount < 10) && tile.Row < rows -1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Down ? tile.SameDirCount + 1 : 1;
				var nextRow = tile.Row + 1;
				var nextCityHeat = cityBoard[GetIndex(nextRow, tile.Column,columns)];

				var nextTile = new Tile(nextRow, tile.Column, Direction.Down, sameDirCount, tile.Heat + nextCityHeat);
				priorityQueue[priorityQueueLength++] = Compress(nextTile);
				addedTiles++;
			}

			//GO up
			if ((tile.IncomingDirection == Direction.Up || tile.SameDirCount >= 4) && tile.IncomingDirection != Direction.Down && (tile.IncomingDirection != Direction.Up || tile.SameDirCount < 10) && tile.Row >= 1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Up ? tile.SameDirCount + 1 : 1;
				var nextRow = tile.Row - 1;
				var nextCityHeat = cityBoard[GetIndex(nextRow, tile.Column,columns)];
				var nextTile = new Tile(nextRow, tile.Column, Direction.Up, sameDirCount, tile.Heat + nextCityHeat);
				priorityQueue[priorityQueueLength++] = Compress(nextTile);
				addedTiles++;
			}

			// GO Right
			if ((tile.IncomingDirection == Direction.Right || tile.SameDirCount >= 4) && tile.IncomingDirection != Direction.Left && (tile.IncomingDirection != Direction.Right || tile.SameDirCount < 10) && tile.Column < columns -1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Right ? tile.SameDirCount + 1 : 1;
				var nextColumn = tile.Column + 1;
				var nextCityHeat = cityBoard[GetIndex(tile.Row, nextColumn,columns)];
				var nextTile = new Tile(tile.Row, nextColumn, Direction.Right, sameDirCount, tile.Heat + nextCityHeat);
				priorityQueue[priorityQueueLength++] = Compress(nextTile);
				addedTiles++;
			}

			//GO left
			if ((tile.IncomingDirection == Direction.Left || tile.SameDirCount >= 4) && tile.IncomingDirection != Direction.Right && (tile.IncomingDirection != Direction.Left || tile.SameDirCount < 10) && tile.Column >= 1)
			{
				var sameDirCount = tile.IncomingDirection == Direction.Left ? tile.SameDirCount + 1 : 1;
				var nextColumn = tile.Column - 1;
				var nextCityHeat = cityBoard[GetIndex(tile.Row, nextColumn,columns)];
				var nextTile = new Tile(tile.Row, nextColumn, Direction.Left, sameDirCount, tile.Heat + nextCityHeat);
				priorityQueue[priorityQueueLength++] = Compress(nextTile);
				addedTiles++;
			}

			RemoveFirst(priorityQueue,priorityQueueLength);
			priorityQueueLength--; //remove the current max value

			SortLastFourIntoArray(priorityQueue,priorityQueueLength, addedTiles);

			if (priorityQueueLength > maxPrioLength)
			{
				priorityQueueLength = maxPrioLength;
			}
		}

		return -1;
	}


	private static int GetIndex(int i, int j, int columns)
	{
		return i * columns + j;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static ulong Compress(Tile tile)
	{
		return
			(ulong) tile.Row
			+ (ulong) tile.Column * 10000
			+ (ulong) tile.IncomingDirection * 100000000
			+ (ulong) tile.SameDirCount * 1000000000
			+ (ulong) tile.Heat * 100000000000;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static Tile Decompress(ulong compressed)
	{
		var direction = (Direction) ((int)((compressed / 100000000) % 10));
		return new Tile(
			(int) (compressed % 10000),
			(int) ((compressed / 10000) % 10000),
			direction,
			(int) ((compressed / 1000000000) % 100),
			(int) (compressed / 100000000000)
		);
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static void SortLastFourIntoArray(scoped Span<ulong> array, int arrayCount, int amountToSort)
	{
		if (arrayCount < 4)
		{
			SortArray(array,arrayCount);
			return;
		}

		for (var i = amountToSort; i > 0; i--)
		{
			var value = array[arrayCount - i];
			var index = arrayCount - i - 1;
			while (index >= 0 && value < array[index])
			{
				array[index + 1] = array[index];
				index--;
			}
			array[index + 1] = value;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static void RemoveFirst(scoped Span<ulong> array, int arrayLength)
	{
		for (int i = 1; i < arrayLength; i++)
		{
			array[i - 1] = array[i];
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	public static void SortArray(scoped Span<ulong> array, int arrayLength)
	{
		for (int i = 1; i < arrayLength; i++)
		{
			var key = array[i];
			var flag = 0;
			for (int j = i - 1; j >= 0 && flag != 1;)
			{
				if (key < array[j])
				{
					array[j + 1] = array[j];
					j--;
					array[j + 1] = key;
				}
				else flag = 1;
			}
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static int CompressVisit(Tile tile, int columns)
	{
		return
			GetIndex(tile.Row, tile.Column, columns) * 100
			+ (tile.SameDirCount) * 10
			+ (int) tile.IncomingDirection;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static int CompressVisit2(Tile tile, int columns)
	{
		return GetIndex(tile.Row, tile.Column, columns)
		       * (int) tile.IncomingDirection
		       * (int) tile.SameDirCount;


		//columns * rows + (columns * rows) + (columns*4)
		return
			GetIndex(tile.Row, tile.Column, columns) * 1000
			+ (tile.SameDirCount)* 10
			+ (int) tile.IncomingDirection;
	}
}