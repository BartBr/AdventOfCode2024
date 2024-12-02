using System.Reflection;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Benchmarks;

public class HappyPuzzleNumberBaseBenchmark
{
	private readonly HappyPuzzleBase? _bartPuzzle;
	private readonly HappyPuzzleBase? _jensPuzzle;
	private readonly HappyPuzzleBase? _jariPuzzle;
	private readonly Input _input;

	public HappyPuzzleNumberBaseBenchmark()
	{
		string puzzleNumber = GetType().Name[^2..];

		var resolvedPuzzles = typeof(HappyPuzzleBase)
			.Assembly
			.GetTypes()
			.Where(x => x.IsAssignableTo(typeof(HappyPuzzleBase)) && x is { IsClass: true, IsAbstract: false })
			.Where(x => x.Name[^2..] == puzzleNumber)
			.ToList();

		var bartPuzzleType = resolvedPuzzles.FirstOrDefault(x => x.Name.StartsWith("Bart"));
		var jensPuzzleType = resolvedPuzzles.FirstOrDefault(x => x.Name.StartsWith("Jens"));
		var jariPuzzleType = resolvedPuzzles.FirstOrDefault(x => x.Name.StartsWith("Jari"));

		if (bartPuzzleType != null)
		{
			_bartPuzzle = (HappyPuzzleBase)Activator.CreateInstance(bartPuzzleType)!;
		}
		if (jensPuzzleType != null)
		{
			_jensPuzzle = (HappyPuzzleBase)Activator.CreateInstance(jensPuzzleType)!;
		}
		if (jariPuzzleType != null)
		{
			_jariPuzzle = (HappyPuzzleBase)Activator.CreateInstance(jariPuzzleType)!;
		}
		_input = Helpers.GetInput(_bartPuzzle?.AssetName ?? _jensPuzzle?.AssetName ?? _jariPuzzle?.AssetName ?? string.Empty);
	}

	public object SolveBartPart1() => _bartPuzzle?.SolvePart1(_input);
}