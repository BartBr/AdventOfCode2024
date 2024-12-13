using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jari;

public class Day09 : HappyPuzzleBase<long>
{
	// first try to solve it by not generating the entire string
	/*public override int SolvePart1(Input input)
	{
		int checksum = 0;
		int index = 0;
		int idCounter = 0;
		bool isFile = true;
		scoped Span<int> memory = stackalloc int[input.Text.Length];

		for (int i = 0; i < input.Text.Length; i++)
		{
			int size = input.Text[i] - '0';
			if (size != 0)
			{
				if (isFile)
				{
					memory[index++] = size;
					idCounter++;
				}
				else
				{
					memory[index++] = -size;
				}
			}

			isFile = !isFile;
		}

		int pos = 0;
		idCounter--;
		var filedMemory = memory.Slice(0, index);
		int endPos = filedMemory.Length - 1;
		int x = 0;
		for (int i = 0; i < filedMemory.Length; i++)
		{
			if (filedMemory[i] > 0)
			{
				for (int z = 0; z < filedMemory[i]; z++)
				{
					checksum += pos++ * x;
				}
			}
			else if (filedMemory[i] < 0)
			{
				x++;
				for (int y = 0; y < Math.Abs(filedMemory[i]); y++)
				{
					if (filedMemory[endPos] == 0)
					{
						endPos--;
						idCounter--;
						if (filedMemory[endPos] < 0)
						{
							endPos--;
						}
					}

					filedMemory[endPos]--;
					checksum += pos * idCounter;
					pos++;
				}
			}
		}

		return checksum;
	}*/

	public override long SolvePart1(Input input)
	{
		long checksum = 0;
		int size = 0;
		for (int i = 0; i < input.Text.Length; i++)
		{
			size += input.Text[i] - '0';
		}

		// expand compressed data
		scoped Span<int> memory = stackalloc int[size];
		int memoryPos = 0, counter = 0;
		bool isFile = true;

		for (int charIndex = 0; charIndex < input.Text.Length; charIndex++)
		{
			int value = isFile
				? counter++
				: -1;
			for (int j = 0; j < input.Text[charIndex] - '0'; j++, memoryPos++)
			{
				memory[memoryPos] = value;
			}

			isFile = !isFile;
		}

		// optimize memory
		for (int i = 0, k = memory.Length - 1; i < memory.Length && k > i; i++)
		{
			// free space -> can fill
			if (memory[i] == -1)
			{
				memory[i] = memory[k];
				memory[k] = -1;

				// move end index to the last non-free space
				while (memory[k] == -1)
				{
					k--;
				}
			}
		}

		// calculate checksum
		for (int i = 0; i < memory.Length; i++)
		{
			if (memory[i] == -1)
			{
				return checksum;
			}

			checksum += memory[i] * i;
		}

		return checksum;
	}

	public override long SolvePart2(Input input)
	{
		throw new NotImplementedException();
	}
}