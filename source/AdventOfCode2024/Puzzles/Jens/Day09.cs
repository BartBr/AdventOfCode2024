using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jens;

public class Day09 : HappyPuzzleBase<long, ulong>
{
	public override long SolvePart1(Input input)
	{
		// Plan of attack to solve part 1
		// 1.0. Expand and calculate checksum of left region
		// 2.0. Expand the next free memory region of the left side and keep track of the width
		// 3.0. Only expand and keep track of the width of the right region, if the existing width was 0
		// 4.0. Reduce the width of the right region (read: calculate checksum on-the-fly) until either the free memory region on the left side is filled up or the right region is empty
		// 4.1. In case of the latter, skip the next empty block on the right and continue with step 3.
		// 5.0. Drain remaining memory space from right region

		var checkSum = 0L;
		var checkSumAdditions = 0;

		var leftRegionIndex = 0;
		var leftRegionId = leftRegionIndex / 2;

		var rightRegionIndex = (input.Text.Length - 1);
		var rightRegionId = rightRegionIndex / 2;

		var leftRegionWidth = 0;
		var rightRegionWidth = 0;

		do
		{
			// 1.0. Read first memory block
			leftRegionWidth = input.Text[leftRegionIndex] - '0';
			++leftRegionIndex;

			for (var i = 0; i < leftRegionWidth; i++)
			{
				checkSum += leftRegionId * checkSumAdditions;
				++checkSumAdditions;
			}

			++leftRegionId;

			// 2.0. Read first empty space
			leftRegionWidth = input.Text[leftRegionIndex] - '0';
			++leftRegionIndex;

			rightHandProcessor:
			if (rightRegionWidth <= 0 && rightRegionIndex > leftRegionIndex)
			{
				// 3.0 Read last memory block
				rightRegionWidth = input.Text[rightRegionIndex] - '0';

				// 4.1. Skip the next empty block on the right (micro optimization)
				rightRegionIndex -= 2;
			}

			// 4.0. Reduce the width of the right region until either left region is filled up or right region is empty
			var currentRightRegionDrainSize = Math.Min(leftRegionWidth, rightRegionWidth);
			for (var i = 0; i < currentRightRegionDrainSize; i++)
			{
				// Console.Write(rightRegionId);
				checkSum += rightRegionId * checkSumAdditions;
				++checkSumAdditions;
			}

			rightRegionWidth -= currentRightRegionDrainSize;
			leftRegionWidth -= currentRightRegionDrainSize;

			if (rightRegionId <= leftRegionId)
			{
				break;
			}

			if (rightRegionWidth <= 0)
			{
				--rightRegionId;
			}

			if (leftRegionWidth > 0)
			{
				goto rightHandProcessor;
			}
		} while (true);

		while (rightRegionWidth > 0)
		{
			checkSum += rightRegionId * checkSumAdditions;
			++checkSumAdditions;
			--rightRegionWidth;
		}

		return checkSum;
	}

	private const int BIT_SHIFT_OFFSET = 16;
	private const int BIT_MASK = (1 << BIT_SHIFT_OFFSET) - 1;
	public override ulong SolvePart2(Input input)
	{
		var bufferSize = input.Text.Length;
		scoped Span<ulong> memoryRegions = stackalloc ulong[bufferSize];

		// 1.0 Encode regionId and regionWidth into memoryRegions for used regions
		for (var i = 0; i < input.Text.Length; i+=2)
		{
			// Encode regionWidth in the 5 least significant bits, the remaining bits can be used for regionId
			memoryRegions[i] = (uint) (i / 2) << BIT_SHIFT_OFFSET | (uint) (input.Text[i] - '0');
		}

		// Encode regionWidth into memoryRegions for unused regions (regionId = 0)
		for (var i = 1; i < input.Text.Length; i += 2)
		{
			// Encode regionWidth in the 5 least significant bits
			memoryRegions[i] = (uint) (input.Text[i] - '0');
		}

		var lastProcessedRegionId = (ulong) bufferSize;
		const int leftRegionIndex = 1;
		var sourceMemoryRegionIndex = input.Text.Length - 1;
		do
		{
			var sourceMemoryRegion = memoryRegions[sourceMemoryRegionIndex];
			var sourceMemoryRegionId = memoryRegions[sourceMemoryRegionIndex] >> BIT_SHIFT_OFFSET;
			if (sourceMemoryRegionId == 0 || sourceMemoryRegionId >= lastProcessedRegionId)
			{
				--sourceMemoryRegionIndex;
				continue;
			}

			var sourceMemoryRegionWidth = sourceMemoryRegion & BIT_MASK;

			var futureMemoryRegionIndex = 1;
			for (; futureMemoryRegionIndex < sourceMemoryRegionIndex; futureMemoryRegionIndex++)
			{
				var targetMemoryRegion = memoryRegions[futureMemoryRegionIndex];
				var targetMemoryRegionId = (long) memoryRegions[futureMemoryRegionIndex] >> BIT_SHIFT_OFFSET;
				if (targetMemoryRegionId != 0)
				{
					continue;
				}

				var targetMemoryRegionWidth = targetMemoryRegion & BIT_MASK;
				if (targetMemoryRegionWidth > sourceMemoryRegionWidth)
				{
					// Found a region that can house our source region albeit not perfectly, we can stop here and trigger a shift
					// Shift all memory regions towards the end
					for (var i = sourceMemoryRegionIndex - 1; i >= futureMemoryRegionIndex; i--)
					{
						memoryRegions[i + 1] = memoryRegions[i];
					}

					// Extend the unused memory space at the now original source region index
					// Verify that the region is not already filled up, otherwise... shift right and merge all unused spaces
					if (memoryRegions[sourceMemoryRegionIndex] >> BIT_SHIFT_OFFSET != 0)
					{
						// Trigger compaction of entire region and shift of entire region to the right
						MergeSequentialUnusedSpaces(memoryRegions.Slice(sourceMemoryRegionIndex + 1));
						memoryRegions[sourceMemoryRegionIndex + 1] = sourceMemoryRegionWidth;
					}
					else
					{
						// Extend the unused memory space
						memoryRegions[sourceMemoryRegionIndex] += sourceMemoryRegionWidth;
					}

					// Overwrite the now duplicated target region
					memoryRegions[futureMemoryRegionIndex] = sourceMemoryRegion;

					// Shorten the original source region
					memoryRegions[futureMemoryRegionIndex + 1] -= sourceMemoryRegionWidth;

					break;
				}

				if (targetMemoryRegionWidth == sourceMemoryRegionWidth)
				{
					// Found a perfect match, we swap the block entirely
					memoryRegions[futureMemoryRegionIndex] = sourceMemoryRegion;
					memoryRegions[sourceMemoryRegionIndex] = targetMemoryRegion;
					break;
				}
			}

			lastProcessedRegionId = sourceMemoryRegionId;
			--sourceMemoryRegionIndex;
		} while (leftRegionIndex < sourceMemoryRegionIndex);

		ulong checkSum = 0L;
		uint checkSumAdditions = 0;
		for (var region = 0; region < memoryRegions.Length; region++)
		{
			var regionId = (ulong) memoryRegions[region] >> BIT_SHIFT_OFFSET;
			var regionWidth = (uint) memoryRegions[region] & BIT_MASK;
			if (regionId == 0)
			{
				checkSumAdditions += regionWidth;
				continue;
			}

			for (var i = 0; i < regionWidth; i++)
			{
				checkSum += regionId * checkSumAdditions;
				++checkSumAdditions;
			}
		}

		return checkSum;
	}

	private static void MergeSequentialUnusedSpaces(Span<ulong> memoryRegionsSlice)
	{
		// Plan of attack to merge sequential unused spaces
		// 1.0. Find the first unused space
		// 2.0. Shift everything forward until the first unused space that can be compacted
		ulong temp1 = memoryRegionsSlice[0];
		ulong temp2;

		for (var i = 0; i < memoryRegionsSlice.Length - 1; i++)
		{
			ref var currentMemoryRegion = ref memoryRegionsSlice[i];
			var currentMemoryRegionId = currentMemoryRegion >> BIT_SHIFT_OFFSET;

			if (currentMemoryRegionId != 0)
			{
				temp2 = currentMemoryRegion;
				currentMemoryRegion = temp1;
				temp1 = temp2;
				continue;
			}

			ref var nextMemoryRegion = ref memoryRegionsSlice[++i];
			var nextMemoryRegionId = nextMemoryRegion >> BIT_SHIFT_OFFSET;
			if (nextMemoryRegionId != 0)
			{
				temp2 = nextMemoryRegion;
				nextMemoryRegion = temp1;
				temp1 = temp2;

				continue;
			}

			nextMemoryRegion += currentMemoryRegion;
			break;
		}
	}
}