using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jari;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public class JariDay01 : HappyPuzzleBase
{
	public override object SolvePart1(Input input)
	{
		var sum = 0;
		var list1 = new List<int>(input.Lines.Length);
		var list2 = new List<int>(input.Lines.Length);

		foreach (string line in input.Lines)
		{
			(int first, int last) = ParseNumbers(line);
			list1.Add(first);
			list2.Add(last);
		}

		list1.Sort();
		list2.Sort();
		for (var i = 0; i < list1.Count; i++)
		{
			var a = list1[i];
			var b = list2[i];
			sum += (Math.Max(a, b) - Math.Min(a, b));
		}

		return sum;
	}


	public override object SolvePart2(Input input)
	{
		var sum = 0;
		var list1 = new List<int>(input.Lines.Length);
		var freq = new Dictionary<int, int>(input.Lines.Length);

		foreach (string line in input.Lines)
		{
			(int first, int last) = ParseNumbers(line);
			list1.Add(first);

			if (freq.ContainsKey(last))
			{
				freq[last]++;
			}
			else
			{
				freq.Add(last, 1);
			}
		}

		foreach (var left in list1)
		{
			if (freq.TryGetValue(left, out var countRight))
			{
				sum += (countRight * left);
			}
		}

		return sum;
	}

	private (int first, int last) ParseNumbers(string line)
	{
		int start = 0;
		int end = line.IndexOf(' ');
		var first = int.Parse(line.AsSpan(start, end - start));

		start = end + 3;
		end = line.Length;
		var last = int.Parse(line.AsSpan(start, end - start));

		return (first, last);
	}
}