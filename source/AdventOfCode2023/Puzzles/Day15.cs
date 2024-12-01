using System.Runtime.CompilerServices;
using AdventOfCode2023.Common;

namespace AdventOfCode2023.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day15 : HappyPuzzleBase
{
	public override object SolvePart1(Input input)
	{
		long total = 0;
		var span = input.Lines[0].AsSpan();
		var indexOfSeparator = span.IndexOf(',');
		while (indexOfSeparator != -1)
		{
			var subSpan = span.Slice(0, indexOfSeparator);

			total += Hash(subSpan);

			span = span.Slice(indexOfSeparator+1, span.Length - indexOfSeparator-1);
			indexOfSeparator = span.IndexOf(',');
		}
		total += Hash(span);
		return total;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static int Hash(ReadOnlySpan<char> span)
	{
		int hash = 0;

		foreach (int t in span)
		{
			hash = ((hash + t) * 17) % 256;
		}

		return hash;
	}

	public override object SolvePart2(Input input)
	{
		var lenses = ReadLenses(input);

		var boxes = new List<List<Lens>>();
		for (int i = 0; i < 256; i++)
		{
			boxes.Add(new List<Lens>());
		}

		foreach (var lens in lenses)
		{
			var box = boxes[lens.LabelHash];
			var existingLensInBox = box.SingleOrDefault(x => x.Label == lens.Label);

			if (lens.LensAction == LensAction.Equals)
			{
				if (existingLensInBox != null)
				{
					var index = box.IndexOf(existingLensInBox);
					box[index] = lens;
				}
				else
				{
					box.Add(lens);
				}
			}
			else
			{
				if (existingLensInBox != null) box.Remove(existingLensInBox);
			}
		}

		int total=0;
		for (int i = 0; i < boxes.Count; i++)
		{
			for (int l = 0; l < boxes[i].Count; l++)
			{
				total += (i + 1) * (l + 1) * boxes[i][l].FocalStrength;
			}

			/*
			 *
			   rn: 1 (box 0) * 1 (first slot) * 1 (focal length) = 1
			   cm: 1 (box 0) * 2 (second slot) * 2 (focal length) = 4
			   ot: 4 (box 3) * 1 (first slot) * 7 (focal length) = 28
			   ab: 4 (box 3) * 2 (second slot) * 5 (focal length) = 40
			   pc: 4 (box 3) * 3 (third slot) * 6 (focal length) = 72

			 */
		}

		return total;
	}

	// private static void ReadLenses(Input input, scoped Span<Lens> lenses, out int amountOfLenses)
	// {
	// 	int lensNr = 0;
	// 	var span = input.Lines[0].AsSpan();
	// 	var indexOfSeparator = span.IndexOf(',');
	// 	while (indexOfSeparator != -1)
	// 	{
	// 		var subSpan = span.Slice(0, indexOfSeparator);
	// 		AddLens(subSpan, lensNr, lenses);
	//
	// 		span = span.Slice(indexOfSeparator + 1, span.Length - indexOfSeparator - 1);
	// 		indexOfSeparator = span.IndexOf(',');
	//
	// 		lensNr++;
	// 	}
	// 	var lastSpan = span[..span.Length];
	// 	AddLens(lastSpan, lensNr, lenses);
	// 	lensNr++;
	// 	amountOfLenses = lensNr;
	// }

	private static List<Lens> ReadLenses(Input input)
	{
		var lenses = new List<Lens>();

		var span = input.Lines[0].AsSpan();
		var indexOfSeparator = span.IndexOf(',');
		while (indexOfSeparator != -1)
		{
			var subSpan = span.Slice(0, indexOfSeparator);
			lenses.Add(AddLens(subSpan));

			span = span.Slice(indexOfSeparator + 1, span.Length - indexOfSeparator - 1);
			indexOfSeparator = span.IndexOf(',');
		}
		var lastSpan = span[..span.Length];
		lenses.Add(AddLens(lastSpan));

		return lenses;
	}

	private static Lens AddLens(ReadOnlySpan<char> lensSpan)
	{
		var equalsIndex = lensSpan.IndexOf('=');
		if (equalsIndex > 0)
		{
			var labelSpan = lensSpan[..equalsIndex];
			var focalStrength = lensSpan[^1] - '0';
			return new Lens(Hash(labelSpan), LensAction.Equals, focalStrength, new string(labelSpan));
		}
		else
		{
			var labelSpan = lensSpan[..^1];
			return new Lens(Hash(labelSpan), LensAction.Delete, 0, new string(labelSpan));
		}
	}


	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static int GetIndex(int i, int j, int columns)
	{
		return i * columns + j;
	}

	public record Lens(int LabelHash, LensAction LensAction, int FocalStrength, string Label);

	public enum LensAction
	{
		Equals=0,
		Delete=1
	}
}