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

		//Sort
		SortMemory(ref nmbrs);

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

			frontIndex++;
			backIndex--;
		}
	}

	public override ulong SolvePart2(Input input)
	{
		var inputLength = input.Lines[0].Length;

		scoped Span<(int memoryId, int start, int length)> files = stackalloc (int memoryId, int start,int length)[(inputLength+1)/2];
		scoped Span<(int start, int length)> freeSpaces = stackalloc (int start, int length)[(inputLength+1)/2];

		ReadData(ref input, inputLength, ref files, ref freeSpaces);
		SortFiles(ref files, ref freeSpaces);
		return CalculateChecksum2(ref files);
	}

	private static void SortFiles(ref Span<(int memoryId, int start, int length)> files, ref Span<(int start, int length)> freeSpaces)
	{
		for (var i = files.Length-1; i >= 0; i--)
		{
			for (var j = 0; j < freeSpaces.Length; j++)
			{
				if (freeSpaces[j].length >= files[i].length && freeSpaces[j].start < files[i].start)
				{
					files[i].start = freeSpaces[j].start;

					freeSpaces[j].length -= files[i].length;
					freeSpaces[j].start += files[i].length;
					break;
				}
			}
		}
	}

	private static void ReadData(ref Input input, int inputLength,ref Span<(int memoryId, int start, int length)> files, ref Span<(int start, int length)> freeSpaces)
	{
		var index = 0;
		for (var i = 0; i < (inputLength+1)/2; i++)
		{
			var amount = input.Lines[0][2*i] - '0';
			files[i] = (memoryId: i, start: index, amount);
			index += amount;

			if( 2*i+1 < inputLength)
			{
				var amountFreeSpaces = input.Lines[0][2*i + 1] - '0';
				freeSpaces[i] = (start: index, length: amountFreeSpaces);
				index += amountFreeSpaces;
			}
			else
			{
				freeSpaces[i] = (start: index, length: 0);
			}
		}
	}

	private static ulong CalculateChecksum2(ref Span<(int memoryId, int start, int length)> files)
	{
		ulong sum = 0;
		for (var i = 0; i < files.Length; i++)
		{
			for (var j = 0; j < files[i].length; j++)
			{
				sum += (ulong) files[i].memoryId * (ulong) (files[i].start + j);
			}
		}

		return sum;
	}
}