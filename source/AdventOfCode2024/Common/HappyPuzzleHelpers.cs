using System.Collections.ObjectModel;

namespace AdventOfCode2024.Common;

public static class HappyPuzzleHelpers
{
	public static IEnumerable<List<Type>> DiscoverPuzzles(bool onlyLast = false)
	{
		var resolvedPuzzles = typeof(HappyPuzzleBase)
			.Assembly
			.GetTypes()
			.Where(x => x.IsAssignableTo(typeof(HappyPuzzleBase)) && x is { IsClass: true, IsAbstract: false })
			.OrderBy(x => x.Name)
			.GroupBy(x => x.Name[^2..])
			.Select(group => group.ToList())
			.AsEnumerable();

		if (onlyLast)
		{
			resolvedPuzzles = resolvedPuzzles.TakeLast(1);
		}

		return resolvedPuzzles;
	}

	public static IEnumerable<string> DiscoverPuzzleNumbers(bool onlyLast = false)
	{
		var resolvedPuzzles = typeof(HappyPuzzleBase)
			.Assembly
			.GetTypes()
			.Where(x => x.IsAssignableTo(typeof(HappyPuzzleBase)) && x is { IsClass: true, IsAbstract: false })
			.OrderBy(x => x.Name)
			.Select(x => x.Name[^2..])
			.Distinct();

		if (onlyLast)
		{
			resolvedPuzzles = resolvedPuzzles.TakeLast(1);
		}

		return resolvedPuzzles;
	}
}