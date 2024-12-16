using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles.Jari;

public class Day13 : HappyPuzzleBase<int>
{
	public override int SolvePart1(Input input)
	{
		const int costA = 3;

		int sumTokens = 0;

		for (int i = 0; i < input.Lines.Length; i++)
		{
			int ax = 0, bx = 0, ay = 0, by = 0;
			int px = 0, py = 0;

			// read button a line
			// read button A x
			int charIndex = "Button A: X+".Length;
			while (input.Lines[i][charIndex] != ',')
			{
				ax = ax * 10 + (input.Lines[i][charIndex++] - '0');
			}

			// read button A y
			charIndex += 4;
			while (charIndex < input.Lines[i].Length)
			{
				ay = ay * 10 + (input.Lines[i][charIndex++] - '0');
			}

			// move to button B line
			i++;

			// read button B x
			charIndex = "Button B: X+".Length;
			while (input.Lines[i][charIndex] != ',')
			{
				bx = bx * 10 + (input.Lines[i][charIndex++] - '0');
			}

			// read button B y
			charIndex += ", Y+".Length;
			while (charIndex < input.Lines[i].Length)
			{
				by = by * 10 + (input.Lines[i][charIndex++] - '0');
			}

			// move to prize line
			i++;

			// read prize x
			charIndex = "Prize: X=".Length;
			while (input.Lines[i][charIndex] != ',')
			{
				px = px * 10 + (input.Lines[i][charIndex++] - '0');
			}

			// read price y
			charIndex += ", Y=".Length;
			while (charIndex < input.Lines[i].Length)
			{
				py = py * 10 + (input.Lines[i][charIndex++] - '0');
			}

			int a = (px * by - py * bx) / (ax * by - ay * bx);
			int b = (ax * py - ay * px) / (ax * by - ay * bx);

			bool isSolvable = (a * ax + b * bx == px) && (a * ay + b * by == py);
			if (isSolvable)
			{
				sumTokens += a * costA + b;
			}

			// move to next machine
			i++;
		}

		return sumTokens;
	}

	public override int SolvePart2(Input input)
	{
		throw new NotImplementedException();
	}
}