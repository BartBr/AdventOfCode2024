using AdventOfCode2024.Common;
using AdventOfCode2024.Puzzles.Bart;
using AdventOfCode2024.Puzzles.Combined;
using AdventOfCode2024.Puzzles.Jari;
using AdventOfCode2024.Puzzles.Jens;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Benchmarks;

[MemoryDiagnoser(true)]
[CategoriesColumn, AllStatisticsColumn, BaselineColumn, MinColumn, Q1Column, MeanColumn, Q3Column, MaxColumn, MedianColumn]
public class Day02Benchmark
{
	private readonly Input _input;
	private readonly BartDay02 _bartsPuzzle;
	private readonly JensDay01 _jensPuzzle;
	private readonly JariDay01 _jariDay01;
	private readonly CombinedDay01 _jensBartDay01;

	public Day02Benchmark()
	{
		_input = Helpers.GetInput("Day02.txt");
		_bartsPuzzle = new BartDay02();
		_jensPuzzle = new JensDay01();
		_jariDay01 = new JariDay01();
		_jensBartDay01 = new CombinedDay01();
	}

	[Benchmark]
	[BenchmarkCategory(Constants.PART1)]
	public object BartDay02Part1()
	{
		return _bartsPuzzle.SolvePart1(_input);
	}

	[Benchmark]
	[BenchmarkCategory(Constants.PART2)]
	public object BartDay02Part2()
	{
		return _bartsPuzzle.SolvePart2(_input);
	}
	/*
	[Benchmark]
	[BenchmarkCategory(Constants.PART1)]
	public object JensDay01Part1()
	{
		return _jensPuzzle.SolvePart1(_input);
	}

	[Benchmark]
	[BenchmarkCategory(Constants.PART2)]
	public object JensDay01Part2()
	{
		return _jensPuzzle.SolvePart2(_input);
	}

	[Benchmark]
	[BenchmarkCategory(Constants.PART1)]
	public object JariDay01Part1()
	{
		return _jariDay01.SolvePart1(_input);
	}

	[Benchmark]
	[BenchmarkCategory(Constants.PART2)]
	public object JariDay01Part2()
	{
		return _jariDay01.SolvePart2(_input);
	}

	[Benchmark]
	[BenchmarkCategory(Constants.PART1)]
	public object JensBartDay01Part1()
	{
		return _jensBartDay01.SolvePart1(_input);
	}

	[Benchmark]
	[BenchmarkCategory(Constants.PART2)]
	public object JensBartDay01Part2()
	{
		return _jensBartDay01.SolvePart2(_input);
	}
	*/
}