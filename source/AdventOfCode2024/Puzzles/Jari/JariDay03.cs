using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jari;

public class JariDay03 : HappyPuzzleBase<int>
{
	public override int SolvePart1(Input input)
	{
		int sum = 0;
		for (int i = 3; i < input.Text.Length; i++)
		{
			if (input.Text[i] == '(' && input.Text[i - 1] == 'l' && input.Text[i - 2] == 'u' && input.Text[i - 3] == 'm')
			{
				i = TryGetProduct(i, input.Text, out int product);
				sum += product;
			}
		}

		return sum;
	}

	public int TryGetProduct(int pos, string instructions, out int product)
	{
		pos++;
		int x = 0;
		int y = 0;
		while (instructions[pos] >= '0' && instructions[pos] <= '9')
		{
			x *= 10;
			x += instructions[pos] - '0';
			pos++;
		}

		if (instructions[pos] != ',')
		{
			product = 0;
			return pos;
		}

		pos++;

		while (instructions[pos] >= '0' && instructions[pos] <= '9')
		{
			y *= 10;
			y += instructions[pos] - '0';
			pos++;
		}

		if (instructions[pos] != ')')
		{
			product = 0;
			return pos;
		}

		pos++;

		product = x * y;
		return pos;
	}

	public override int SolvePart2(Input input)
	{
		int sum = 0;
		bool instructionsEnabled = true;

		for (int i = 3; i < input.Text.Length - 2; i++)
		{
			if (instructionsEnabled == false && input.Text[i - 2] == 'd' && input.Text[i - 1] == 'o' && input.Text[i] == '(' && input.Text[i + 1] == ')')
			{
				instructionsEnabled = true;
			}
			else if (instructionsEnabled && input.Text[i - 3] == 'm' && input.Text[i - 2] == 'u' && input.Text[i - 1] == 'l' && input.Text[i] == '(')
			{
				i = TryGetProduct(i, input.Text, out int product);
				sum += product;
			}
			else if (i + 3 < input.Text.Length && input.Text[i - 3] == 'd' && input.Text[i - 2] == 'o' && input.Text[i - 1] == 'n' && input.Text[i] == '\'' && input.Text[i + 1] == 't' && input.Text[i + 2] == '(' && input.Text[i + 3] == ')')
			{
				instructionsEnabled = false;
			}
		}

		return sum;
	}
}