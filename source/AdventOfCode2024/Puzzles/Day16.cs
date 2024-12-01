using System.Runtime.CompilerServices;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Puzzles;

/// <remarks>
/// By default, the <see cref="Input"/> parameter of both <see cref="SolvePart1"/> and <see cref="SolvePart2"/> will give access to the file
/// in the Assets folder that has the same name as the class, followed by the .txt extension.
/// However, if you want to use a different name, then you can override the virtual property <see cref="HappyPuzzleBase.AssetName"/>
/// </remarks>
public partial class Day16 : HappyPuzzleBase
{
	public override object SolvePart1(Input input)
	{
		return -1;
	}

	public override object SolvePart2(Input input)
	{
		return -1;
	}

	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	private static int GetIndex(int i, int j, int columns)
	{
		return i * columns + j;
	}
}