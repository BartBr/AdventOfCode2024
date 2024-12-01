using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day05 : HappyPuzzleBase
{
	public override object SolvePart1(Input input)
	{
		const int amountOfSeeds = 20;

		// Begin reading seeds
		var seeds = new List<long>(amountOfSeeds);
		var seedsLine = input.Lines[0].AsSpan();
		ReadSeedsLine(seedsLine, seeds);
		// End reading seeds

		var lineNumber = 3;

		var mappers = new List<Mapping>[7];

		mappers[0] = ReadMappings(ref lineNumber, input.Lines);
		mappers[1] = ReadMappings(ref lineNumber, input.Lines);
		mappers[2] = ReadMappings(ref lineNumber, input.Lines);
		mappers[3] = ReadMappings(ref lineNumber, input.Lines);
		mappers[4] = ReadMappings(ref lineNumber, input.Lines);
		mappers[5] = ReadMappings(ref lineNumber, input.Lines);
		mappers[6] = ReadMappings(ref lineNumber, input.Lines);

		long lowestSeed = long.MaxValue;
		for (var j = 0; j < seeds.Count; j++)
		{
			var seed = seeds[j];
			for (int i = 0; i < 7; i++)
			{
				seed = Map(seed, mappers[i]);
			}
			if (lowestSeed > seed) lowestSeed = seed;
		}

		return (int)lowestSeed;
	}

	private long Map(long seed, List<Mapping> mappers)
	{
		foreach (var mapping in mappers)
		{
			if (seed >= mapping.SourceRangeStart && seed < mapping.SourceRangeStart + mapping.RangeLength)
			{
				return seed - mapping.SourceRangeStart + mapping.DestinationRangeStart;
			}
		}
		return seed;
	}

	private List<Mapping> ReadMappings(ref int lineNumber, string[] input)
	{
		var mappings = new List<Mapping>();
		while (lineNumber < input.Length && input[lineNumber].Length > 0) //  && input[lineNumber][0] >= '0' && input[lineNumber][0] <= '9'
		{
			mappings.Add(ReadMapping(input[lineNumber]));
			lineNumber++;
		}
		lineNumber+=2;
		return mappings;
	}

	private Mapping ReadMapping(string mappingLine)
	{
		var c = 0;

		long destinationRangeStart = 0;
		while (c < mappingLine.Length && mappingLine[c] != ' ')
		{
			destinationRangeStart = destinationRangeStart * 10 + (mappingLine[c] - '0');
			c++;
		}
		c++;

		long sourceRangeStart = 0;
		while (c < mappingLine.Length && mappingLine[c] != ' ')
		{
			sourceRangeStart = sourceRangeStart * 10 + (mappingLine[c] - '0');

			c++;
		}
		c++;

		long rangeLength = 0;
		while (c < mappingLine.Length && mappingLine[c] != ' ')
		{
			rangeLength = rangeLength * 10 + (mappingLine[c] - '0');
			c++;
		}

		return new Mapping(destinationRangeStart, sourceRangeStart, rangeLength);
	}

	private static void ReadSeedsLine(ReadOnlySpan<char> seedsLine, List<long> seeds)
	{
		long seedsNumber = 0;
		for (var c = 7; c < seedsLine.Length; c++)
		{
			if (seedsLine[c] == ' ')
			{
				seeds.Add(seedsNumber);
				seedsNumber = 0;
			}

			if (seedsLine[c] >= '0' && seedsLine[c] <= '9')
			{
				seedsNumber = seedsNumber * 10 + (seedsLine[c] - '0');
			}
		}

		seeds.Add(seedsNumber);
	}


	private readonly record struct Mapping(long DestinationRangeStart, long SourceRangeStart, long RangeLength);

	public override object SolvePart2(Input input)
	{
		const int amountOfSeeds = 20;

		// Begin reading seeds
		var seeds = new List<long>(amountOfSeeds);
		var seedsLine = input.Lines[0].AsSpan();
		ReadSeedsLine(seedsLine, seeds);
		// End reading seeds

		var lineNumber = 3;

		var mappers = new List<Mapping>[7];

		mappers[0] = ReadMappings(ref lineNumber, input.Lines);
		mappers[1] = ReadMappings(ref lineNumber, input.Lines);
		mappers[2] = ReadMappings(ref lineNumber, input.Lines);
		mappers[3] = ReadMappings(ref lineNumber, input.Lines);
		mappers[4] = ReadMappings(ref lineNumber, input.Lines);
		mappers[5] = ReadMappings(ref lineNumber, input.Lines);
		mappers[6] = ReadMappings(ref lineNumber, input.Lines);

		long lowestSeed = long.MaxValue;
		for (var j = 0; j < seeds.Count/2; j++)
		{
			for (int k = 0; k < seeds[j*2 + 1]; k++)
			{
				var seed = seeds[j*2] + k;
				for (int i = 0; i < 7; i++)
				{
					seed = Map(seed, mappers[i]);
				}
				if (lowestSeed > seed) lowestSeed = seed;
			}
		}

		return (int)lowestSeed;
	}
}