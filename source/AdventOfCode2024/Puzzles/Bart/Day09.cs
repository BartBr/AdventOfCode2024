using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Bart;

public class Day09 : HappyPuzzleBase<ulong>
{
	public override ulong SolvePart1(Input input)
	{
		var size = 0;
		for (var i = 0; i < input.Lines[0].Length; i++)
		{
			size += (input.Lines[0][i] - '0');
		}

		scoped Span<int> nmbrs = stackalloc int[size];

		var index = 0;
		var memoryId = 0;
		var j = 0;
		while(j < input.Lines[0].Length)
		{
			//mem block
			var memSize = input.Lines[0][j] - '0';

			for (var i = 0; i < memSize; i++)
			{
				nmbrs[index++] = memoryId;
			}
			memoryId++;
			j++;

			//free memory block
			if (j < input.Lines[0].Length)
			{
				var freememSize = input.Lines[0][j] - '0';

				for (var i = 0; i < freememSize; i++)
				{
					nmbrs[index++] = -1;
				}
				j++;
			}
		}
		//PrintNmbrs(nmbrs);

		//Sort
		SortMemory(ref nmbrs);


		//PrintNmbrs(nmbrs);

		return CalculateChecksum(ref nmbrs);
	}

	private static ulong CalculateChecksum(ref Span<int> nmbrs)
	{
		ulong sum = 0;
		for (var i = 0; i < nmbrs.Length; i++)
		{
			if(nmbrs[i] == -1)
				return sum;

			sum += ((ulong)nmbrs[i] * (ulong)i);
		}

		return sum;
	}

	private static void PrintNmbrs(Span<int> nmbrs)
	{
		for (int i = 0; i < nmbrs.Length; i++)
		{
			if (nmbrs[i] == -1)
			{
				Console.Write(".");
			}
			else
				Console.Write(nmbrs[i]);
		}

		Console.WriteLine();
	}

	private static void SortMemory(ref Span<int> nmbrs)
	{
		var frontIndex = 0;
		var backIndex = nmbrs.Length - 1;
		while (frontIndex < backIndex)
		{
			while (nmbrs[frontIndex] != -1)
			{
				frontIndex++;
			}
			while (nmbrs[backIndex] == -1)
			{
				backIndex--;
			}

			if(frontIndex >= backIndex)
				return;

			//swap:
			nmbrs[frontIndex] = nmbrs[backIndex];
			nmbrs[backIndex] = -1;

			//PrintNmbrs(nmbrs);

			frontIndex++;
			backIndex--;
		}
	}

	public override ulong SolvePart2(Input input)
	{
		throw new NotImplementedException();
		var size = 0;
		for (var i = 0; i < input.Lines[0].Length; i++)
		{
			size += (input.Lines[0][i] - '0');
		}

		scoped Span<int> nmbrs = stackalloc int[size];

		var index = 0;
		var memoryId = 0;
		var j = 0;
		while(j < input.Lines[0].Length)
		{
			//mem block
			var memSize = input.Lines[0][j] - '0';

			for (var i = 0; i < memSize; i++)
			{
				nmbrs[index++] = memoryId;
			}
			memoryId++;
			j++;

			//free memory block
			if (j < input.Lines[0].Length)
			{
				var freememSize = input.Lines[0][j] - '0';

				for (var i = 0; i < freememSize; i++)
				{
					nmbrs[index++] = -1;
				}
				j++;
			}
		}
		//PrintNmbrs(nmbrs);

		//Sort
		Part2_SortMemory(ref nmbrs);

		//PrintNmbrs(nmbrs);

		return CalculateChecksum(ref nmbrs);
	}

	private void Part2_SortMemory(ref Span<int> nmbrs)
	{
		int backIndex = nmbrs.Length - 1;
		while (backIndex > 0)
		{
			while (nmbrs[backIndex] == -1 && backIndex > 0)
			{
				backIndex--;
			}
			var endBlockIndex = backIndex;
			while (nmbrs[backIndex] != -1 && backIndex > 0)
			{
				backIndex--;
			}
			var fromBlockIndex = backIndex;
			var blocksize = endBlockIndex - fromBlockIndex;

			var frontIndex = 0;
			// while ()
			// {
			//
			// }
		}
	}
}