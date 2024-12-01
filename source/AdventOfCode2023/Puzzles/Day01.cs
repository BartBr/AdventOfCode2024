using AdventOfCode2023.Common;

namespace AdventOfCode2023.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public class Day01 : HappyPuzzleBase
{
	public override object SolvePart1(Input input)
	{
		var total = 0;
		for (int i = 0; i < input.Lines.Length; i++)
		{
			int j = 0;
			while (input.Lines[i][j] < '0' || input.Lines[i][j] > '9')
			{
				j++;
			}

			int k = input.Lines[i].Length - 1;
			while (input.Lines[i][k] < '0' || input.Lines[i][k] > '9')
			{
				k--;
			}

			int lineNumber = (input.Lines[i][j] - '0') * 10 + (input.Lines[i][k] - '0');
			total += lineNumber;
		}
		return total;
	}

	public override object SolvePart2(Input input)
	{
		var total = 0;
		for (int i = 0; i < input.Lines.Length; i++)
		{
			if (input.Lines[i].Length < 1)
			{
				continue;
			}

			int j = 0;
			int? firstNumber = null;
			while (firstNumber==null)
			{
				firstNumber = GetNumber(input.Lines[i], j);
				j++;
			}

			j = input.Lines[i].Length-1;
			int? secondNumber = null;
			while (secondNumber==null)
			{
				secondNumber = GetReverseNumber(input.Lines[i], j);
				j--;
			}

			int lineNumber = firstNumber.Value * 10 + secondNumber.Value;
			total += lineNumber;
		}

		return total;
	}

	static int? GetNumber(string s, int j)
	{
	    if (s[j] >= '0' && s[j] <= '9')
	    {
	        return s[j] - '0';
	    }
	    if (s.Length - j >= 3 && s[j] == 'o' && s[j+1] == 'n' && s[j+2] == 'e')
	    {
	        return 1;
	    }
	    if (s.Length - j >= 3 && s[j] == 't' && s[j+1] == 'w' && s[j+2] == 'o')
	    {
	        return 2;
	    }
	    //three
	    if (s.Length - j >= 5 && s[j] == 't' && s[j+1] == 'h' && s[j+2] == 'r' && s[j+3] == 'e'&& s[j+4] == 'e')
	    {
	        return 3;
	    }
	    //four
	    if (s.Length - j >= 4 && s[j] == 'f' && s[j+1] == 'o' && s[j+2] == 'u' && s[j+3] == 'r')
	    {
	        return 4;
	    }
	    //five
	    if (s.Length - j >= 4 && s[j] == 'f' && s[j+1] == 'i' && s[j+2] == 'v' && s[j+3] == 'e')
	    {
	        return 5;
	    }
	    if (s.Length - j >= 3 && s[j] == 's' && s[j+1] == 'i' && s[j+2] == 'x')
	    {
	        return 6;
	    }
	    //seven
	    if (s.Length - j > 5 && s[j] == 's' && s[j+1] == 'e' && s[j+2] == 'v' && s[j+3] == 'e' && s[j+4] == 'n')
	    {
	        return 7;
	    }
	    if (s.Length - j >= 5 && s[j] == 'e' && s[j+1] == 'i' && s[j+2] == 'g' && s[j+3] == 'h' && s[j+4] == 't')
	    {
	        return 8;
	    }
	    if (s.Length - j >= 4 && s[j] == 'n' && s[j+1] == 'i' && s[j+2] == 'n' && s[j+3] == 'e')
	    {
	        return 9;
	    }
	    return null;
	}

	static int? GetReverseNumber(string s, int j)
	{
	    if (s[j] >= '0' && s[j] <= '9')
	    {
	        return s[j] - '0';
	    }
	    if (j >= 2 && s[j-2] == 'o' && s[j-1] == 'n' && s[j] == 'e')
	    {
	        return 1;
	    }
	    if (j >= 2 && s[j-2] == 't' && s[j-1] == 'w' && s[j] == 'o')
	    {
	        return 2;
	    }
	    //three
	    if (j >= 4 && s[j-4] == 't' && s[j-3] == 'h' && s[j-2] == 'r' && s[j-1] == 'e'&& s[j] == 'e')
	    {
	        return 3;
	    }
	    //four
	    if (j >= 3 && s[j-3] == 'f' && s[j-2] == 'o' && s[j-1] == 'u' && s[j] == 'r')
	    {
	        return 4;
	    }
	    //five
	    if (j >= 3 && s[j-3] == 'f' && s[j-2] == 'i' && s[j-1] == 'v' && s[j] == 'e')
	    {
	        return 5;
	    }
	    if (j >= 2 && s[j-2] == 's' && s[j-1] == 'i' && s[j] == 'x')
	    {
	        return 6;
	    }
	    //seven
	    if (j >= 4 && s[j-4] == 's' && s[j-3] == 'e' && s[j-2] == 'v' && s[j-1] == 'e' && s[j] == 'n')
	    {
	        return 7;
	    }
	    if (j >= 4 && s[j-4] == 'e' && s[j-3] == 'i' && s[j-2] == 'g' && s[j-1] == 'h' && s[j] == 't')
	    {
	        return 8;
	    }
	    if (j >= 3 && s[j-3] == 'n' && s[j-2] == 'i' && s[j-1] == 'n' && s[j] == 'e')
	    {
	        return 9;
	    }
	    return null;
	}
}