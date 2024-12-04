using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jari;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public class JariDay02 : HappyPuzzleBase<int>
{
	public override int SolvePart1(Input input)
	{
		int numberOfSaveReports = 0;
		for (var i = 0; i < input.Lines.Length; i++)
		{
			if (IsLineSafe(input.Lines[i]))
			{
				numberOfSaveReports++;
			}
		}

		return numberOfSaveReports;
	}

	public bool IsLineSafe(string line)
	{
		List<int> numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
			.Select(int.Parse)
			.ToList();

		int a;
		int b;
		int dir = Math.Sign(numbers[0] - numbers[1]);

		for (int i = 1; i < numbers.Count; i++)
		{
			a = numbers[i - 1];
			b = numbers[i];
			if (dir == 0 || Math.Abs(a - b) > 3 || dir != Math.Sign(a - b))
			{
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// todo this implementation doesn't work for the moment
	/// tried to determine if line is safe for part 1 by only looping once through line
	/// => both parsing can validation rules at once
	/// </summary>
	private bool IsLineSafeOptimized(string line)
	{
		int pos = 0;
		int a = 0;
		int b = 0;

		while (pos < line.Length && line[pos] != ' ')
		{
			a *= 10;
			a += (line[pos] - '0');
			pos++;
		}

		pos++;
		while (pos < line.Length && line[pos] != ' ')
		{
			b *= 10;
			b += (line[pos] - '0');
			pos++;
		}

		pos++;

		int diff = a - b;
		int dir = Math.Sign(diff);
		if (dir == 0 || Math.Abs(diff) > 3)
		{
			return false;
		}

		a = b;
		b = 0;

		while (pos < line.Length)
		{
			if (line[pos] == ' ')
			{
				diff = a - b;
				if (Math.Abs(diff) > 3 || dir != Math.Sign(diff))
				{
					return false;
				}

				a = b;
				b = 0;
			}
			else
			{
				b *= 10;
				b += (line[pos] - '0');
			}

			pos++;
		}

		return true;
	}


	public override int SolvePart2(Input input)
	{
		int numberOfSaveReports = 0;
		for (var i = 0; i < input.Lines.Length; i++)
		{
			if (IsLineSafePart2(input.Lines[i]))
			{
				numberOfSaveReports++;
			}
		}

		return numberOfSaveReports;
	}

	public bool IsLineSafePart2(string line)
	{
		List<int> numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
			.Select(int.Parse)
			.ToList();

		int a;
		int b;
		int dir = Math.Sign(numbers[0] - numbers[1]);

		for (int i = 1; i < numbers.Count; i++)
		{
			a = numbers[i - 1];
			b = numbers[i];
			if (dir == 0 || Math.Abs(a - b) > 3 || dir != Math.Sign(a - b))
			{
				return false;
			}
		}

		return true;
	}
}