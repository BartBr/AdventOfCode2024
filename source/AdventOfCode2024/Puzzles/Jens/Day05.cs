using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jens;

public class Day05 : HappyPuzzleBase<int>
{
	public override int SolvePart1(Input input)
	{
		var inputLines = input.Lines.AsSpan();

		#region Parse first section of input

		scoped Span<bool> lookupTable = stackalloc bool[100 * 100];

		var i = 0;
		var currentLine = inputLines[i];

		do
		{
			var firstPage = (currentLine[0] - '0') * 10 + (currentLine[1] - '0');
			var secondPage = (currentLine[3] - '0') * 10 + (currentLine[4] - '0');

			lookupTable[firstPage * 100 + secondPage] = true;

			++i;
			currentLine = inputLines[i];
		} while (currentLine != "");

		#endregion

		++i;

		var sum = 0;

		scoped Span<int> numberVisitedPagesBuffer = stackalloc int[40];
		for (; i < inputLines.Length; i++)
		{
			currentLine = inputLines[i];
			numberVisitedPagesBuffer[0] = (currentLine[0] - '0') * 10 + (currentLine[1] - '0');

			var j = 3;
			for (; j < currentLine.Length; j += 3)
			{
				var currentNumber = (currentLine[j] - '0') * 10 + (currentLine[j + 1] - '0');
				var lookupTableSlice = lookupTable.Slice(currentNumber * 100, 100);

				foreach (var page in numberVisitedPagesBuffer.Slice(0, j / 3))
				{
					if (lookupTableSlice[page])
					{
						goto outerLoopLabel;
					}
				}

				numberVisitedPagesBuffer[j / 3] = currentNumber;
			}

			// Rounding down with integers will make this go to the correct index
			sum += numberVisitedPagesBuffer[(j / 6)];

			outerLoopLabel: ;
		}

		return sum;
	}

	public override int SolvePart2(Input input)
	{
		var inputLines = input.Lines.AsSpan();

		#region Parse first section of input

		scoped Span<bool> lookupTable = stackalloc bool[100 * 100];

		var i = 0;
		var currentLine = inputLines[i];

		do
		{
			var firstPage = (currentLine[0] - '0') * 10 + (currentLine[1] - '0');
			var secondPage = (currentLine[3] - '0') * 10 + (currentLine[4] - '0');

			lookupTable[firstPage * 100 + secondPage] = true;

			++i;
			currentLine = inputLines[i];
		} while (currentLine != "");

		#endregion

		++i;

		var sum = 0;

		scoped Span<int> pageUpdateBuffer = stackalloc int[60];
		for (; i < inputLines.Length; i++)
		{
			currentLine = inputLines[i];
			pageUpdateBuffer[0] = (currentLine[0] - '0') * 10 + (currentLine[1] - '0');

			var addToSum = false;
			var pageCount = 1;
			for (; pageCount < (currentLine.Length + 1) / 3;)
			{
				// Parse the number
				var currentNumber = (currentLine[pageCount * 3] - '0') * 10 + (currentLine[pageCount * 3 + 1] - '0');

				// Lookup relevant slice of pages that have to be after the current number
				var lookupTableSlice = lookupTable.Slice(currentNumber * 100, 100);

				pageUpdateBuffer[pageCount] = currentNumber;
				++pageCount;

				// Check if any of the previous pages are in the lookup and have to be after the current number
				// If so, move the offending page to after the current number with an offset (starting with 0, and incrementing for each found page)
				// If the page is in the correct place orderwise, then move the page to its correct index in the buffer, based on the current offset
				var offset = 0;
				for (var kIndex = 0; kIndex < pageCount; kIndex++)
				{
					var referencePage = pageUpdateBuffer[kIndex];
					if (lookupTableSlice[referencePage])
					{
						pageUpdateBuffer[pageCount + offset] = referencePage;
						++offset;

						addToSum = true;
					}
					else
					{
						// This could technically be guarded by an offset != 0 check,
						// but it's faster to just do it every time since the result will stay the same regardless,
						// and it also results in less misses in the branch predictor of the CPU
						pageUpdateBuffer[kIndex - offset] = referencePage;
					}
				}

				// If any of the pages were moved, then shift them back inside the scope of the current page count
				for (var kIndex = pageCount; kIndex < pageCount + offset; kIndex++)
				{
					pageUpdateBuffer[kIndex - 1] = pageUpdateBuffer[kIndex];
				}
			}

			if (addToSum)
			{
				// Rounding down with integers will make this go to the correct index
				sum += pageUpdateBuffer[(pageCount / 2)];
			}
		}

		return sum;
	}
}