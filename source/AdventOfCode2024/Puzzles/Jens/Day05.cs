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

		scoped Span<int> pageUpdateBuffer = stackalloc int[40];
		for (; i < inputLines.Length; i++)
		{
			currentLine = inputLines[i];

			var pageCount = 0;
			for (; pageCount < (currentLine.Length + 1) / 3; pageCount++)
			{
				pageUpdateBuffer[pageCount] = (currentLine[pageCount * 3] - '0') * 10 + (currentLine[pageCount * 3 + 1] - '0');
			}

			var addToSum = false;
			for (var j = 1; j < pageCount; j++)
			{
				var currentPage = pageUpdateBuffer[j];
				var lookupTableSlice = lookupTable.Slice(currentPage * 100, 100);

				for (var k = 0; k < j; k++)
				{
					var referencePage = pageUpdateBuffer[k];
					if (lookupTableSlice[referencePage])
					{
						for (var l = k; l < j - k; l++)
						{
							pageUpdateBuffer[l] = pageUpdateBuffer[l + 1];
						}

						pageUpdateBuffer[j] = referencePage;
						--j;

						addToSum = true;
					}
				}
			}

			if (addToSum) {
				// Rounding down with integers will make this go to the correct index
				sum += pageUpdateBuffer[(pageCount / 2)];
			}
		}

		return sum;
	}
}