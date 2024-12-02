using System.Reflection;
using AdventOfCode2024.Common;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Benchmarks;

[MemoryDiagnoser(true)]
[CategoriesColumn, AllStatisticsColumn, BaselineColumn, MinColumn, Q1Column, MeanColumn, Q3Column, MaxColumn, MedianColumn]
public class SpecificDayPuzzleBenchmark
{
	public static string PuzzleNumber { get; set; } = "01";

	private readonly IList<object[]> _puzzleSolutions;
	private readonly Dictionary<string, Input> _inputDict;
	private readonly IList<PuzzleBenchyThingy> _puzzleBenchyThingies;

	public SpecificDayPuzzleBenchmark()
	{
		var resolvedPuzzles = typeof(HappyPuzzleBase)
			.Assembly
			.GetTypes()
			.Where(x => x.IsAssignableTo(typeof(HappyPuzzleBase)) && x is { IsClass: true, IsAbstract: false })
			.Where(x => x.Name.EndsWith(PuzzleNumber))
			.ToList();

		_puzzleBenchyThingies = new List<PuzzleBenchyThingy>();

		foreach (var resolvedPuzzle in resolvedPuzzles)
		{
			var puzzleNumber = resolvedPuzzle.Name[^2..];
			var name = resolvedPuzzle.Name[..^5];

			_puzzleBenchyThingies.Add(new PuzzleBenchyThingy
			{
				Puzzle = (Activator.CreateInstance(resolvedPuzzle) as HappyPuzzleBase)!,
				Input = Helpers.GetInput("Day" + puzzleNumber + ".txt"),
				Person = name,
				PuzzleNumber = puzzleNumber
			});
		}
	}

	//[ParamsAllValues]
	public IEnumerable<PuzzleBenchyThingy> PuzzlesChallenges => _puzzleBenchyThingies;

	[Benchmark]
	[BenchmarkCategory(Constants.PART1)]
	[ArgumentsSource(nameof(PuzzlesChallenges))]
	public object SolvePart1(PuzzleBenchyThingy p)
	{
		return p.Puzzle.SolvePart1(p.Input);
	}

	[Benchmark]
	[BenchmarkCategory(Constants.PART2)]
	[ArgumentsSource(nameof(PuzzlesChallenges))]
	public object SolvePart2(PuzzleBenchyThingy p)
	{
		return p.Puzzle.SolvePart2(p.Input);
	}

	public class PuzzleBenchyThingy
	{
		public HappyPuzzleBase Puzzle { get; set; }
		public Input Input { get; set; }
		public string PuzzleNumber { get; set; }
		public string Person { get; set; }

		public override string ToString()
		{
			return $"{PuzzleNumber} - {Person}";
		}
	}
}