using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace AdventOfCode2024.Benchmarks.SourceGenerators.Helpers;

internal abstract class DiagnosticsWrapper
{
	public List<Diagnostic> Diagnostics { get; } = [];
}