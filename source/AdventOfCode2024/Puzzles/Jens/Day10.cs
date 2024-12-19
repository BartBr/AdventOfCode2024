using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jens;

public class Day10 : HappyPuzzleBase<int>
{
	public override int SolvePart1(Input input)
	{
		var inputSpan = input.Text.AsSpan();
		var inputHeight = input.Lines.Length;
		var inputWidth = input.Lines[0].Length + 1;

		var sum = 0;
		for (var i = 0; i < inputSpan.Length; i++)
		{
			if (inputSpan[i] != '0')
			{
				continue;
			}

			sum += Part1_CountTrailHeads(ref inputSpan, inputWidth, inputHeight, i);
		}

		return sum;
	}

	private static int Part1_CountTrailHeads(ref ReadOnlySpan<char> inputSpan, int inputWidth, int inputHeight, int startingIndex)
	{
		// Implement simple BFS algorithm
		const int waveSearchBufferMaxSize = 20;

		// Queue-like stoof for BFS
		// Contains the indexes of map tiles that need to have their neighbours searched for the current waveStep
		var waveSearchBufferSize = 0;
		Span<int> waveSearchBuffer = stackalloc int[waveSearchBufferMaxSize];
		// Placeholder buffer for the next waveStep round checks
		var nextWaveSearchBufferSize = 0;
		Span<int> nextWaveSearchBuffer = stackalloc int[waveSearchBufferMaxSize];

		// Indicates whether a given index has already been visited
		Span<bool> terrainVisitedData = stackalloc bool[inputSpan.Length];

		// Add the starting index to the waveSearchBuffer
		waveSearchBuffer[waveSearchBufferSize++] = startingIndex;
		terrainVisitedData[startingIndex] = true;

		var nextTarget = '1';

		for (var currentWaveStep = 0; currentWaveStep < 9; currentWaveStep++)
		{
			for (var waveSearchBufferIndex = 0; waveSearchBufferIndex < waveSearchBufferSize; waveSearchBufferIndex++)
			{
				ref var terrainIndex = ref waveSearchBuffer[waveSearchBufferIndex];

				// Get the x-y coordinates for the current index
				var terrainRow = terrainIndex / inputWidth;
				var terrainColumn = terrainIndex % inputWidth;

				// Check top
				if (terrainRow > 0)
				{
					var topIndex = terrainIndex - inputWidth;
					var topValue = inputSpan[topIndex];

					if (topValue == nextTarget && !terrainVisitedData[topIndex])
					{
						nextWaveSearchBuffer[nextWaveSearchBufferSize++] = topIndex;
						terrainVisitedData[topIndex] = true;
					}
				}

				// Check bottom
				if (terrainRow < inputHeight - 1)
				{
					var bottomIndex = terrainIndex + inputWidth;
					var bottomValue = inputSpan[bottomIndex];

					if (bottomValue == nextTarget && !terrainVisitedData[bottomIndex])
					{
						nextWaveSearchBuffer[nextWaveSearchBufferSize++] = bottomIndex;
						terrainVisitedData[bottomIndex] = true;
					}
				}

				// Check left
				if (terrainColumn > 0)
				{
					var leftIndex = terrainIndex - 1;
					var leftValue = inputSpan[leftIndex];

					if (leftValue == nextTarget && !terrainVisitedData[leftIndex])
					{
						nextWaveSearchBuffer[nextWaveSearchBufferSize++] = leftIndex;
						terrainVisitedData[leftIndex] = true;
					}
				}

				// Check right
				if (terrainColumn < inputWidth - 2) // Account for the newline character
				{
					var rightIndex = terrainIndex + 1;
					var rightValue = inputSpan[rightIndex];

					if (rightValue == nextTarget && !terrainVisitedData[rightIndex])
					{
						nextWaveSearchBuffer[nextWaveSearchBufferSize++] = rightIndex;
						terrainVisitedData[rightIndex] = true;
					}
				}
			}

			++nextTarget;

			// Copy the nextWaveSearchBuffer to the waveSearchBuffer so we can reuse it
			for (var i = 0; i < nextWaveSearchBufferSize; i++)
			{
				waveSearchBuffer[i] = nextWaveSearchBuffer[i];
			}

			waveSearchBufferSize = nextWaveSearchBufferSize;
			nextWaveSearchBufferSize = 0;
		}

		return waveSearchBufferSize;
	}

	public override int SolvePart2(Input input)
	{
		var inputSpan = input.Text.AsSpan();
		var inputHeight = input.Lines.Length;
		var inputWidth = input.Lines[0].Length + 1;

		var sum = 0;
		for (var i = 0; i < inputSpan.Length; i++)
		{
			if (inputSpan[i] != '0')
			{
				continue;
			}

			sum += Part2_CountTrailHeads(ref inputSpan, inputWidth, inputHeight, i);
		}

		return sum;
	}

	private static int Part2_CountTrailHeads(ref ReadOnlySpan<char> inputSpan, int inputWidth, int inputHeight, int startingIndex)
	{
		// Implement simple BFS algorithm
		const int waveSearchBufferMaxSize = 80;

		// Queue-like stoof for BFS
		// Contains the indexes of map tiles that need to have their neighbours searched for the current waveStep
		var waveSearchBufferSize = 0;
		Span<int> waveSearchBuffer = stackalloc int[waveSearchBufferMaxSize];
		// Placeholder buffer for the next waveStep round checks
		var nextWaveSearchBufferSize = 0;
		Span<int> nextWaveSearchBuffer = stackalloc int[waveSearchBufferMaxSize];

		// Add the starting index to the waveSearchBuffer
		waveSearchBuffer[waveSearchBufferSize++] = startingIndex;

		var nextTarget = '1';

		for (var currentWaveStep = 0; currentWaveStep < 9; currentWaveStep++)
		{
			for (var waveSearchBufferIndex = 0; waveSearchBufferIndex < waveSearchBufferSize; waveSearchBufferIndex++)
			{
				ref var terrainIndex = ref waveSearchBuffer[waveSearchBufferIndex];

				// Get the x-y coordinates for the current index
				var terrainRow = terrainIndex / inputWidth;
				var terrainColumn = terrainIndex % inputWidth;

				// Check top
				if (terrainRow > 0)
				{
					var topIndex = terrainIndex - inputWidth;
					var topValue = inputSpan[topIndex];

					if (topValue == nextTarget)
					{
						nextWaveSearchBuffer[nextWaveSearchBufferSize++] = topIndex;
					}
				}

				// Check bottom
				if (terrainRow < inputHeight - 1)
				{
					var bottomIndex = terrainIndex + inputWidth;
					var bottomValue = inputSpan[bottomIndex];

					if (bottomValue == nextTarget)
					{
						nextWaveSearchBuffer[nextWaveSearchBufferSize++] = bottomIndex;
					}
				}

				// Check left
				if (terrainColumn > 0)
				{
					var leftIndex = terrainIndex - 1;
					var leftValue = inputSpan[leftIndex];

					if (leftValue == nextTarget)
					{
						nextWaveSearchBuffer[nextWaveSearchBufferSize++] = leftIndex;
					}
				}

				// Check right
				if (terrainColumn < inputWidth - 2) // Account for the newline character
				{
					var rightIndex = terrainIndex + 1;
					var rightValue = inputSpan[rightIndex];

					if (rightValue == nextTarget)
					{
						nextWaveSearchBuffer[nextWaveSearchBufferSize++] = rightIndex;
					}
				}
			}

			++nextTarget;

			// Copy the nextWaveSearchBuffer to the waveSearchBuffer so we can reuse it
			for (var i = 0; i < nextWaveSearchBufferSize; i++)
			{
				waveSearchBuffer[i] = nextWaveSearchBuffer[i];
			}

			waveSearchBufferSize = nextWaveSearchBufferSize;
			nextWaveSearchBufferSize = 0;
		}

		return waveSearchBufferSize;
	}
}