using System.Diagnostics;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jens;

public class Day06 : HappyPuzzleBase<int>
{
	public override int SolvePart1(Input input)
	{
		var gridWidth = input.Lines[0].Length + 1; // +1 to account for \n at the end of each virtual grid row

		var inputText = input.Text;
		scoped Span<bool> travelledPathBuffer = stackalloc bool[inputText.Length];

		var startingIndex = inputText.IndexOf('^');

		var traversalDirection = TraversalDirection.UP;
		var directionOffset = -gridWidth;
		var currentIndex = startingIndex;

		var stepCount = 1;
		while (true)
		{
			travelledPathBuffer[currentIndex] = true;

			var nextIndex = currentIndex + directionOffset;
			if (nextIndex < 0 || nextIndex >= inputText.Length)
			{
				break;
			}

			var nextChar = inputText[nextIndex];
			if (nextChar == '#')
			{
				traversalDirection = RotateTravelDirectionClockwise(traversalDirection);
				directionOffset = TravelDirectionToDirectionOffset(traversalDirection, gridWidth);
			}
			else if (nextChar == '\n')
			{
				break;
			}
			else
			{
				currentIndex = nextIndex;
				if (!travelledPathBuffer[currentIndex])
				{
					++stepCount;
				}
			}
		}

		return stepCount;
	}

	public override int SolvePart2(Input input)
	{
		var gridWidth = input.Lines[0].Length + 1; // +1 to account for \n at the end of each virtual grid row

		var inputText = input.Text;
		// Verified travel direction buffer
		scoped Span<TraversalDirection> travelDirectionsBuffer = stackalloc TraversalDirection[inputText.Length];

		// Working copy of the travel direction buffer for use in loop finding
		scoped Span<TraversalDirection> travelDirectionsBufferWorkingCopy = stackalloc TraversalDirection[inputText.Length];

		var startingIndex = inputText.IndexOf('^');

		scoped Span<char> inputSpan = stackalloc char[inputText.Length];
		inputText.CopyTo(inputSpan);

		var travelDirection = TraversalDirection.UP;
		var travelDirectionIndexOffset = -gridWidth;

		var currentIndex = startingIndex;

		var possibleLoopCount = 0;
		while (true)
		{
			travelDirectionsBuffer[currentIndex] |= travelDirection;

			var nextIndex = currentIndex + travelDirectionIndexOffset;
			if (nextIndex < 0 || nextIndex >= inputText.Length)
			{
				// End of normal path without loops
				break;
			}

			var nextChar = inputText[nextIndex];
			if (nextChar == '#')
			{
				travelDirection = RotateTravelDirectionClockwise(travelDirection);
				travelDirectionIndexOffset = TravelDirectionToDirectionOffset(travelDirection, gridWidth);
				continue;
			}

			if (nextChar == '\n')
			{
				// End of normal path without loops
				break;
			}

			if (travelDirectionsBuffer[nextIndex] == TraversalDirection.UNMAPPED)
			{
				// Not yet visited, thus we could place an obstacle here to potentially trigger a loop
				// Assume we're facing rotated 90 degrees clockwise from the current direction
				var rotatedTravelDirection = RotateTravelDirectionClockwise(travelDirection);

				// Copy the current travel directions buffer to the working copy,
				// which should override the entire buffer of the previous loop finding algorithm run
				travelDirectionsBuffer.CopyTo(travelDirectionsBufferWorkingCopy);

				// Insert virtual obstacle, make sure to remove it again after verifying
				ref var nextCharRef = ref inputSpan[nextIndex];
				nextCharRef = '#';

				possibleLoopCount += (TraverseForLoop(
					ref inputSpan,
					gridWidth,
					travelDirectionsBufferWorkingCopy,
					currentIndex,
					rotatedTravelDirection));

				// Restore the original character
				nextCharRef = '.';
			}

			currentIndex = nextIndex;
		}

		return possibleLoopCount;
	}

	private static int TraverseForLoop(
		ref Span<char> inputText,
		int gridWidth,
		Span<TraversalDirection> travelDirectionsBuffer,
		int currentIndex,
		TraversalDirection travelDirection)
	{
		var travelDirectionIndexOffset = TravelDirectionToDirectionOffset(travelDirection, gridWidth);

		while (true)
		{
			travelDirectionsBuffer[currentIndex] |= travelDirection;

			var nextIndex = currentIndex + travelDirectionIndexOffset;
			if (nextIndex < 0 || nextIndex >= inputText.Length)
			{
				return 0;
			}

			var nextChar = inputText[nextIndex];
			if (nextChar == '#')
			{
				travelDirection = RotateTravelDirectionClockwise(travelDirection);
				travelDirectionIndexOffset = TravelDirectionToDirectionOffset(travelDirection, gridWidth);
				continue;
			}

			if (nextChar == '\n')
			{
				return 0;
			}

			if ((travelDirectionsBuffer[nextIndex] & travelDirection) == travelDirection)
			{
				return 1;
			}

			currentIndex = nextIndex;
		}
	}

	private static int TravelDirectionToDirectionOffset(TraversalDirection traversalDirection, int gridWidth)
	{
		return traversalDirection switch
		{
			TraversalDirection.LEFT => -1,
			TraversalDirection.UP => -gridWidth,
			TraversalDirection.RIGHT => 1,
			TraversalDirection.DOWN => gridWidth,
			_ => throw new UnreachableException()
		};
	}

	private static TraversalDirection RotateTravelDirectionClockwise(TraversalDirection value)
	{
		// Bitshifts to the left, then OR with the rightmost bit
		const int bitSize = 4; // Hardcoded 4 to only account for the first 4 least significant bits of the enum
		var uintValue = (uint) value;
		return (TraversalDirection) (((uintValue << 1) | (uintValue >> (bitSize - 1))) & 0x0F);
	}

	[Flags]
	private enum TraversalDirection : uint
	{
		UNMAPPED = 0,   // 0x00000000
		LEFT = 1 << 0,  // 0x00000001
		UP = 1 << 1,    // 0x00000010
		RIGHT = 1 << 2, // 0x00000100
		DOWN = 1 << 3   // 0x00001000
	}
}