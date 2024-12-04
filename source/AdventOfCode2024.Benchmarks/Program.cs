using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

var config = DefaultConfig.Instance
	.AddColumn(CategoriesColumn.Default)
	.AddColumn(StatisticColumn.AllStatistics)
	.AddColumn(BaselineRatioColumn.RatioMean)
	.AddDiagnoser(MemoryDiagnoser.Default)
	.WithOrderer(new DefaultOrderer());

// Runs latest benchmark
BenchmarkRunner.Run<AdventOfCode2024.Benchmarks.Generated.LastPuzzleBenchmark>(config);

// To run a specific benchmark
// BenchmarkRunner.Run<AdventOfCode2024.Benchmarks.Generated.Day01Benchmark>(config);

// To run multiple days
// Type[] benchmarkTypes = [
// 	typeof(AdventOfCode2024.Benchmarks.Generated.Day01Benchmark),
// 	typeof(AdventOfCode2024.Benchmarks.Generated.Day02Benchmark)
// ];
// BenchmarkRunner.Run(benchmarkTypes, config);

// To run all benchmarks
// BenchmarkRunner.Run(typeof(Program).Assembly, config);