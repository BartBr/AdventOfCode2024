// See https://aka.ms/new-console-template for more information

using AdventOfCode2024.Benchmarks;
using AdventOfCode2024.Common;
using BenchmarkDotNet.Running;

var benchmarkCases = HappyPuzzleHelpers
	.DiscoverPuzzles(true)
	.Select(x => typeof(HappyPuzzleBaseBenchmark<>).MakeGenericType(x))
	.ToArray();

BenchmarkRunner.Run(benchmarkCases);