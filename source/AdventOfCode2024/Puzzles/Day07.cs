using System.Collections;
using System.Runtime.CompilerServices;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day07 : HappyPuzzleBase
{
	//private static readonly HandComparer _comparer = new HandComparer();

	public override object SolvePart1(Input input)
	{
		int total = 0;

		scoped Span<Hand> hands = stackalloc Hand[input.Lines.Length];

		for (var line = 0; line < input.Lines.Length; line++)
		{
			var span = input.Lines[line].AsSpan();
			hands[line] = ReadHand1(span);
		}

		QuickSort(hands);
		//hands.Sort(_comparer);

		for (int i = 0; i < hands.Length; i++)
		{
			total += (i+1) * hands[i].Bid;
		}

		return total;
	}

	private Hand ReadHand1(ReadOnlySpan<char> span)
	{
		scoped Span<int> cards = stackalloc int[5];
		for (int i = 0; i < 5; i++)
		{
			cards[i] = CardAsNumber1(span[i]);
		}

		var sortableScore = CardsAsSortableScore1(ref cards);
		var bid = AsNumber(span.Slice(6));
		return new Hand(sortableScore, bid);
	}

	private long CardsAsSortableScore1(scoped ref Span<int> cards)
	{
		Span<int> amountOfCards = stackalloc int[15];
		for (int i = 0; i < 5; i++)
		{
			amountOfCards[cards[i]]++;
		}

		var highestAmount = 0;
		var highestNumber = 0;
		var secondHighestAmount = 0;
		for (int i = 1; i < 15; i++)
		{
			if (amountOfCards[i] > highestAmount) //Could improve to let the highest number of equal amount be the first
			{
				highestAmount = amountOfCards[i];
				highestNumber = i;
			}
		}

		for (int i = 1; i < 15; i++)
		{
			if (amountOfCards[i] > secondHighestAmount && amountOfCards[i] <= highestAmount && i != highestNumber)
			{
				secondHighestAmount = amountOfCards[i];
			}
		}
		long sortableScore = highestAmount switch
		{
			5 => 90000000000,
			4 => 80000000000,
			3 when secondHighestAmount == 2 => 70000000000,
			3 => 60000000000,
			2 when secondHighestAmount == 2 => 50000000000,
			2 => 40000000000,
			_ => 0
		};

		sortableScore += cards[4];
		sortableScore += cards[3] * 100;
		sortableScore += cards[2] * 10000;
		sortableScore += cards[1] * 1000000;
		sortableScore += cards[0] * 100000000;

		return sortableScore;
	}

	private static int CardAsNumber1(char c)
	{
		return c switch
		{
			'A' => 14,
			'K' => 13,
			'Q' => 12,
			'J' => 11,
			'T' => 10,
			'9' => 9,
			'8' => 8,
			'7' => 7,
			'6' => 6,
			'5' => 5,
			'4' => 4,
			'3' => 3,
			'2' => 2,
			_ => 0
		};
	}

	public override object SolvePart2(Input input)
	{
		int total = 0;

		scoped Span<Hand> hands = stackalloc Hand[input.Lines.Length];

		for (var line = 0; line < input.Lines.Length; line++)
		{
			var span = input.Lines[line].AsSpan();
			hands[line] = ReadHand2(span);
		}

		QuickSort(hands);
		//hands.Sort(Comparison);

		for (int i = 0; i < hands.Length; i++)
		{
			total += (i+1) * hands[i].Bid;
		}

		return total;
	}

	private Hand ReadHand2(ReadOnlySpan<char> span)
	{
		scoped Span<int> cards = stackalloc int[5];
		for (int i = 0; i < 5; i++)
		{
			cards[i] = CardAsNumber2(span[i]);
		}

		var sortableScore = CardsAsSortableScore2(ref cards);
		var bid = AsNumber(span.Slice(6));
		return new Hand(sortableScore, bid);
	}

	private long CardsAsSortableScore2(scoped ref Span<int> cards)
	{
		Span<int> amountOfCards = stackalloc int[15];
		for (int i = 0; i < 5; i++)
		{
			amountOfCards[cards[i]]++;
		}

		var amountOfJokers = amountOfCards[1];

		var highestAmount = 0;
		var highestNumber = 0;
		var secondHighestAmount = 0;
		for (int i = 2; i < 15; i++)
		{
			if (amountOfCards[i] > highestAmount) //Could improve to let the highest number of equal amount be the first
			{
				highestAmount = amountOfCards[i];
				highestNumber = i;
			}
		}

		for (int i = 2; i < 15; i++)
		{
			if (amountOfCards[i] > secondHighestAmount && amountOfCards[i] <= highestAmount && i != highestNumber)
			{
				secondHighestAmount = amountOfCards[i];
			}
		}

		highestAmount += amountOfJokers;

		long sortableScore = highestAmount switch
		{
			5 => 90000000000,
			4 => 80000000000,
			3 when secondHighestAmount == 2 => 70000000000,
			3 => 60000000000,
			2 when secondHighestAmount == 2 => 50000000000,
			2 => 40000000000,
			_ => 0
		};

		sortableScore += cards[4];
		sortableScore += cards[3] * 100;
		sortableScore += cards[2] * 10000;
		sortableScore += cards[1] * 1000000;
		sortableScore += cards[0] * 100000000;

		return sortableScore;
	}

	private static int CardAsNumber2(char c)
	{
		return c switch
		{
			'A' => 14,
			'K' => 13,
			'Q' => 12,
			'J' => 1,
			'T' => 10,
			'9' => 9,
			'8' => 8,
			'7' => 7,
			'6' => 6,
			'5' => 5,
			'4' => 4,
			'3' => 3,
			'2' => 2,
			_ => 0
		};
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int AsNumber(ReadOnlySpan<char> span)
	{
		var number = 0;
		for (int i = 0; i < span.Length; i++)
		{
			if (span[i] != ' ')
			{
				var newNumber = (span[i] - '0');
				number = number * 10 + newNumber;
			}
		}
		return number;
	}

	private readonly record struct Hand(long SortableScore, int Bid);

	// private sealed class HandComparer : Comparer<Hand>
	// {
	// 	// Compares by Length, Height, and Width.
	// 	public override int Compare(Hand a, Hand b)
	// 	{
	// 		if (a.SortableScore > b.SortableScore) return 1;
	// 		if (a.SortableScore == b.SortableScore) return 0;
	// 		return -1;
	// 	}
	// }

	private void QuickSort(Span<Hand> arr)
	{
		if (arr.IsEmpty || arr.Length == 1)
			return;
		var q = Partition(arr);
		QuickSort(arr[..q++]);
		if (q < arr.Length - 1) // not already sorted
			QuickSort(arr[q..]);
	}
	private int Partition(Span<Hand> arr)
	{
		ref var pivot = ref arr[^1];
		var i = -1; // current end of lessThan array part
		for (int j = 0; j < arr.Length - 1; j++)
		{
			if (arr[j].SortableScore < pivot.SortableScore)
			{
				i++;
				(arr[i], arr[j]) = (arr[j], arr[i]);
			}
		}
		var q = i + 1; //pivotPosition
		(arr[q], pivot) = (pivot, arr[q]);
		return q;
	}
}