using System.Diagnostics;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jens;

public class Day08 : HappyPuzzleBase<int>
{
	private const int MAX_ANTENNA_FREQUENCIES = 26 + 26 + 10;
	private const int MAX_ANTENNAS_PER_FREQUENCY = 20;
	private const int MAX_FREQUENCY_BUFFER_SIZE = MAX_ANTENNAS_PER_FREQUENCY + 1;

	public override int SolvePart1(Input input)
	{
		var gridWidth = input.Lines[0].Length;
		var gridHeight = input.Lines.Length;

		var rawGridWidth = gridWidth + 1;

		scoped Span<int> antennaBuffers = stackalloc int[MAX_ANTENNA_FREQUENCIES * MAX_FREQUENCY_BUFFER_SIZE];

		for (var i = 0; i < input.Text.Length; i++)
		{
			var c = input.Text[i];
			if (c == '.' || c == '\n')
			{
				continue;
			}

			var x = i % rawGridWidth;
			var y = i / rawGridWidth;
			var convertedLocationIndex = y * gridWidth + x;

			var antennaBufferSizeIndex = GetAntennaBufferSizeIndex(c);
			ref var antennaBufferSize = ref antennaBuffers[antennaBufferSizeIndex];
			++antennaBufferSize;
			Debug.Assert(antennaBufferSize <= MAX_ANTENNAS_PER_FREQUENCY);

			antennaBuffers[antennaBufferSizeIndex + antennaBufferSize] = convertedLocationIndex;
		}

		var distinctCount = 0;
		scoped Span<bool> antiNodeBuffer = stackalloc bool[gridWidth * gridHeight];
		for (var bufferIndex = 0; bufferIndex < antennaBuffers.Length; bufferIndex += MAX_FREQUENCY_BUFFER_SIZE)
		{
			var antennaBufferSize = antennaBuffers[bufferIndex];
			var antennaBufferSpan = antennaBuffers.Slice(bufferIndex + 1, antennaBufferSize);
			for (var i = 0; i < antennaBufferSpan.Length; i++)
			{
				var x1 = antennaBufferSpan[i] % gridWidth;
				var y1 = antennaBufferSpan[i] / gridWidth;

				// We can skip the i first entries here, as they have already been handled
				// This also results in us being able to omit some y-value out-of-bound checks later on
				for (var j = i + 1; j < antennaBufferSpan.Length; j++)
				{
					var x2 = antennaBufferSpan[j] % gridWidth;
					var y2 = antennaBufferSpan[j] / gridWidth;

					var dx = x2 - x1;
					var dy = y2 - y1;

					// We can ignore the check for going positively out of bounds on y-axis as we know the input is ordered in ascending order
					// and we either stay at the same y-value or go down
					if (x1 - dx >= 0 && x1 - dx < gridWidth && y1 - dy >= 0)
					{
						ref var antiNodeEntry = ref antiNodeBuffer[(y1 - dy) * gridWidth + (x1 - dx)];
						if (!antiNodeEntry)
						{
							++distinctCount;
							antiNodeEntry = true;
						}
					}

					// We can ignore the check for going negatively out of bounds on y-axis as we know the input is ordered in ascending order
					// and we either stay at the same y-value or go up
					if (x2 + dx >= 0 && x2 + dx < gridWidth && y2 + dy < gridHeight)
					{
						ref var antiNodeEntry = ref antiNodeBuffer[(y2 + dy) * gridWidth + (x2 + dx)];
						if (!antiNodeEntry)
						{
							++distinctCount;
							antiNodeEntry = true;
						}
					}
				}
			}
		}

		return distinctCount;
	}

	public override int SolvePart2(Input input)
	{
		var gridWidth = input.Lines[0].Length;
		var gridHeight = input.Lines.Length;

		var rawGridWidth = gridWidth + 1;

		scoped Span<int> antennaBuffers = stackalloc int[MAX_ANTENNA_FREQUENCIES * MAX_FREQUENCY_BUFFER_SIZE];

		var distinctCount = 0;
		scoped Span<bool> antiNodeBuffer = stackalloc bool[gridWidth * gridHeight];

		for (var i = 0; i < input.Text.Length; i++)
		{
			var c = input.Text[i];
			if (c == '.' || c == '\n')
			{
				continue;
			}

			var x = i % rawGridWidth;
			var y = i / rawGridWidth;
			var convertedLocationIndex = y * gridWidth + x;

			var antennaBufferSizeIndex = GetAntennaBufferSizeIndex(c);
			ref var antennaBufferSize = ref antennaBuffers[antennaBufferSizeIndex];
			++antennaBufferSize;
			Debug.Assert(antennaBufferSize <= MAX_ANTENNAS_PER_FREQUENCY);

			antennaBuffers[antennaBufferSizeIndex + antennaBufferSize] = convertedLocationIndex;
		}

		for (var bufferIndex = 0; bufferIndex < antennaBuffers.Length; bufferIndex += MAX_FREQUENCY_BUFFER_SIZE)
		{
			var antennaBufferSize = antennaBuffers[bufferIndex];
			var antennaBufferSpan = antennaBuffers.Slice(bufferIndex + 1, antennaBufferSize);
			for (var i = 0; i < antennaBufferSpan.Length; i++)
			{
				var x1Original = antennaBufferSpan[i] % gridWidth;
				var y1Original = antennaBufferSpan[i] / gridWidth;

				if (antennaBufferSpan.Length >= 2)
				{
					ref var antiNodeEntry = ref antiNodeBuffer[y1Original * gridWidth + x1Original];
					if (!antiNodeEntry)
					{
						++distinctCount;
						antiNodeEntry = true;
					}
				}

				// We can skip the i first entries, as they have already been handled
				// This also results in us being able to omit some y-value out-of-bound checks later on
				for (var j = i + 1; j < antennaBufferSpan.Length; j++)
				{
					var x1 = x1Original;
					var y1 = y1Original;

					var x2 = antennaBufferSpan[j] % gridWidth;
					var y2 = antennaBufferSpan[j] / gridWidth;

					var dx = x2 - x1;
					var dy = y2 - y1;

					// We can ignore the check for going positively out of bounds on y-axis as we know the input is ordered in ascending order
					// and we either stay at the same y-value or go down
					while (x1 - dx >= 0 && x1 - dx < gridWidth && y1 - dy >= 0)
					{
						ref var antiNodeEntry = ref antiNodeBuffer[(y1 - dy) * gridWidth + (x1 - dx)];
						if (!antiNodeEntry)
						{
							++distinctCount;
							antiNodeEntry = true;
						}

						x1 -= dx;
						y1 -= dy;
					}

					// We can ignore the check for going negatively out of bounds on y-axis as we know the input is ordered in ascending order
					// and we either stay at the same y-value or go up
					while (x2 + dx >= 0 && x2 + dx < gridWidth && y2 + dy < gridHeight)
					{
						ref var antiNodeEntry = ref antiNodeBuffer[(y2 + dy) * gridWidth + (x2 + dx)];
						if (!antiNodeEntry)
						{
							++distinctCount;
							antiNodeEntry = true;
						}

						x2 += dx;
						y2 += dy;
					}
				}
			}
		}

		return distinctCount;
	}

	// Returns the index of the first element for a particular frequency, being the count
	private static int GetAntennaBufferSizeIndex(char c)
	{
		return MAX_FREQUENCY_BUFFER_SIZE * c switch
		{
			>= 'a' and <= 'z' => c - 'a',
			>= 'A' and <= 'Z' => c - 'A' + 26,
			>= '0' and <= '9' => c - '0' + 26 + 26,
			_ => throw new UnreachableException()
		};
	}
}