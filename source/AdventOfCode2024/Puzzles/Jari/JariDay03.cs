using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jari;

public class JariDay03 : HappyPuzzleBase
{
	public override object SolvePart1(Input input)
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

	public override object SolvePart2(Input input)
	{
		throw new NotImplementedException();
	}
}