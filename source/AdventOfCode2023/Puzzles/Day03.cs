using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AdventOfCode2023.Common;

namespace AdventOfCode2023.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day03 : HappyPuzzleBase
{
	private static SearchValues<char> SpecialCharSearchValues = SearchValues.Create("-&/*@#%+$=");

	public override object SolvePart1(Input input)
	{
		int total = 0;
		for (int i = 0; i < input.Lines.Length; i++)
		{
			var j = 0;

			while (j < input.Lines[i].Length)
			{
				var prevLine = i > 1 ? input.Lines[i - 1].AsSpan() : ReadOnlySpan<char>.Empty;
				var nextLine = i + 1 < input.Lines.Length ? input.Lines[i + 1].AsSpan() : ReadOnlySpan<char>.Empty;
				var c = input.Lines[i].AsSpan();

				if (c[j] >= '0' && c[j] <= '9')
				{
					if (j+1 < c.Length && c[j + 1] >= '0' && c[j + 1] <= '9')
					{
						if (j+2 < c.Length && c[j + 2] >= '0' && c[j + 2] <= '9')
						{
							//three numbers
							if (HasAdjacentSpecChar(prevLine, c, nextLine, j, 3)) total += (c[j] - '0') * 100 + (c[j + 1] - '0') * 10 + (c[j + 2] - '0');
							j += 2;
						}
						else
						{
							if (HasAdjacentSpecChar(prevLine, c, nextLine, j, 2)) total += (c[j] - '0') * 10 + (c[j + 1] - '0');
							// two number
							j += 1;
						}
					}
					else
					{
						if (HasAdjacentSpecChar(prevLine, c, nextLine, j, 1)) total += (c[j] - '0');
						// one number
					}
				}
				j++;
			}
		}

		return total;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private bool HasAdjacentSpecChar(ReadOnlySpan<char> prevLine, ReadOnlySpan<char> currentLine, ReadOnlySpan<char> nextLine, int startNumberIndex, int length)
	{
		var searchStartIndex = startNumberIndex - 1 >= 0
			? startNumberIndex - 1
			: startNumberIndex;
		var searchEndIndex = startNumberIndex + length + 1 < currentLine.Length
			? startNumberIndex + length + 1
			: startNumberIndex + length;

		var searchLength = searchEndIndex - searchStartIndex;

		if (!prevLine.IsEmpty && prevLine.Slice(searchStartIndex, searchLength).IndexOfAny(SpecialCharSearchValues) >= 0) return true;
		if (currentLine.Slice(searchStartIndex, searchLength).IndexOfAny(SpecialCharSearchValues) >= 0) return true;
		if (!nextLine.IsEmpty && nextLine.Slice(searchStartIndex, searchLength).IndexOfAny(SpecialCharSearchValues) >= 0) return true;
		return false;
	}

	// public override object SolvePart2(Input input)
	// {
	// 	int total = 0;
	// 	var dict = new Dictionary<int, int>();
	//
	// 	for (int i = 0; i < input.Lines.Length; i++)
	// 	{
	// 		var j = 0;
	//
	// 		while (j < input.Lines[i].Length)
	// 		{
	// 			var prevLine = i > 1 ? input.Lines[i - 1].AsSpan() : ReadOnlySpan<char>.Empty;
	// 			var nextLine = i + 1 < input.Lines.Length ? input.Lines[i + 1].AsSpan() : ReadOnlySpan<char>.Empty;
	// 			var c = input.Lines[i].AsSpan();
	//
	// 			if (c[j] >= '0' && c[j] <= '9')
	// 			{
	// 				IEnumerable<int> adjacentLocations;
	// 				int number;
	// 				if (j+1 < c.Length && c[j + 1] >= '0' && c[j + 1] <= '9')
	// 				{
	// 					if (j+2 < c.Length && c[j + 2] >= '0' && c[j + 2] <= '9')
	// 					{
	// 						//three numbers
	// 						adjacentLocations = GetLocationFromAdjacent(prevLine, c, nextLine, j, 3, i);
	// 						number = (c[j] - '0') * 100 + (c[j + 1] - '0') * 10 + (c[j + 2] - '0');
	// 						j += 2;
	// 					}
	// 					else
	// 					{
	// 						adjacentLocations = GetLocationFromAdjacent(prevLine, c, nextLine, j, 2, i);
	// 						number = (c[j] - '0') * 10 + (c[j + 1] - '0');
	// 						// two number
	// 						j += 1;
	// 					}
	// 				}
	// 				else
	// 				{
	// 					adjacentLocations = GetLocationFromAdjacent(prevLine, c, nextLine, j, 1, i);
	// 					number = (c[j] - '0');
	// 					// one number
	// 				}
	//
	// 				foreach (var adjacentLocation in adjacentLocations)
	// 				{
	// 					if (dict.ContainsKey(adjacentLocation))
	// 					{
	// 						total += dict[adjacentLocation] * number;
	// 					}
	// 					else
	// 					{
	// 						dict[adjacentLocation] = number;
	// 					}
	// 				}
	// 			}
	// 			j++;
	// 		}
	// 	}
	//
	// 	return total;
	// }
	//
	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// private List<int> GetLocationFromAdjacent(ReadOnlySpan<char> prevLine, ReadOnlySpan<char> currentLine, ReadOnlySpan<char> nextLine, int startNumberIndex, int length, int currentLineNumber)
	// {
	// 	var list = new List<int>();
	// 	var searchStartIndex = startNumberIndex - 1 >= 0
	// 		? startNumberIndex - 1
	// 		: startNumberIndex;
	// 	var searchEndIndex = startNumberIndex + length + 1 < currentLine.Length
	// 		? startNumberIndex + length + 1
	// 		: startNumberIndex + length;
	//
	// 	var searchLength = searchEndIndex - searchStartIndex;
	// 	int index;
	// 	if (!prevLine.IsEmpty)
	// 	{
	// 		index = prevLine.Slice(searchStartIndex, searchLength).IndexOfAny(SpecialCharSearchValues);
	// 		if (index >= 0)
	// 		{
	// 			list.Add((currentLineNumber) * 1000 + searchStartIndex + index);
	// 		}
	// 	}
	//
	// 	index = currentLine.Slice(searchStartIndex, searchLength).IndexOfAny(SpecialCharSearchValues);
	// 	if (index >= 0) list.Add( (currentLineNumber +1) * 1000 + searchStartIndex + index);
	//
	// 	if (!nextLine.IsEmpty)
	// 	{
	// 		index = nextLine.Slice(searchStartIndex, searchLength).IndexOfAny(SpecialCharSearchValues);
	// 		if (index >= 0)
	// 		{
	// 			list.Add( (currentLineNumber + 2) * 1000 + searchStartIndex + index);
	// 		}
	// 	}
	//
	// 	return list;
	// }

	public override object SolvePart2(Input input)
	{
		int total = 0;
		var dict = new Dictionary<int, int>();

		for (int i = 0; i < input.Lines.Length; i++)
		{
			var j = 0;

			while (j < input.Lines[i].Length)
			{
				var prevLine = i > 1 ? input.Lines[i - 1].AsSpan() : ReadOnlySpan<char>.Empty;
				var nextLine = i + 1 < input.Lines.Length ? input.Lines[i + 1].AsSpan() : ReadOnlySpan<char>.Empty;
				var c = input.Lines[i].AsSpan();

				if (c[j] >= '0' && c[j] <= '9')
				{
					int adjacentLocation;
					int number;
					if (j+1 < c.Length && c[j + 1] >= '0' && c[j + 1] <= '9')
					{
						if (j+2 < c.Length && c[j + 2] >= '0' && c[j + 2] <= '9')
						{
							//three numbers
							adjacentLocation = GetLocationFromAdjacent(prevLine, c, nextLine, j, 3, i);
							number = (c[j] - '0') * 100 + (c[j + 1] - '0') * 10 + (c[j + 2] - '0');
							j += 2;
						}
						else
						{
							adjacentLocation = GetLocationFromAdjacent(prevLine, c, nextLine, j, 2, i);
							number = (c[j] - '0') * 10 + (c[j + 1] - '0');
							// two number
							j += 1;
						}
					}
					else
					{
						adjacentLocation = GetLocationFromAdjacent(prevLine, c, nextLine, j, 1, i);
						number = (c[j] - '0');
						// one number
					}

					if(adjacentLocation > 0)
					{
						if (dict.ContainsKey(adjacentLocation))
						{
							total += dict[adjacentLocation] * number;
						}
						else
						{
							dict[adjacentLocation] = number;
						}
					}
				}
				j++;
			}
		}

		return total;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private int GetLocationFromAdjacent(ReadOnlySpan<char> prevLine, ReadOnlySpan<char> currentLine, ReadOnlySpan<char> nextLine, int startNumberIndex, int length, int currentLineNumber)
	{
		var searchStartIndex = startNumberIndex - 1 >= 0
			? startNumberIndex - 1
			: startNumberIndex;
		var searchEndIndex = startNumberIndex + length + 1 < currentLine.Length
			? startNumberIndex + length + 1
			: startNumberIndex + length;

		var searchLength = searchEndIndex - searchStartIndex;
		int index;
		if (!prevLine.IsEmpty)
		{
			index = prevLine.Slice(searchStartIndex, searchLength).IndexOfAny(SpecialCharSearchValues);
			if (index >= 0)
			{
				return ((currentLineNumber) * 1000 + searchStartIndex + index);
			}
		}

		index = currentLine.Slice(searchStartIndex, searchLength).IndexOfAny(SpecialCharSearchValues);
		if (index >= 0) return ( (currentLineNumber +1) * 1000 + searchStartIndex + index);

		if (!nextLine.IsEmpty)
		{
			index = nextLine.Slice(searchStartIndex, searchLength).IndexOfAny(SpecialCharSearchValues);
			if (index >= 0)
			{
				return ((currentLineNumber + 2) * 1000 + searchStartIndex + index);
			}
		}

		return -1;
	}
}