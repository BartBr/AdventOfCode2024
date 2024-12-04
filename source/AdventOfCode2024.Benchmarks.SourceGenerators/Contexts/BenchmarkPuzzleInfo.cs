namespace AdventOfCode2024.Benchmarks.SourceGenerators.Contexts;

public sealed class BenchmarkPuzzleInfo
{
	public readonly string GroupKey;
	public readonly string Username;
	public readonly string FullyQualifiedPuzzleTypeName;

	public readonly string FirstPartReturnType;
	public readonly string SecondPartReturnType;

	public BenchmarkPuzzleInfo(string groupKey, string username, string fullyQualifiedPuzzleTypeName, string firstPartReturnType, string secondPartReturnType)
	{
		GroupKey = groupKey;
		Username = username;
		FullyQualifiedPuzzleTypeName = fullyQualifiedPuzzleTypeName;
		FirstPartReturnType = firstPartReturnType;
		SecondPartReturnType = secondPartReturnType;
	}
}