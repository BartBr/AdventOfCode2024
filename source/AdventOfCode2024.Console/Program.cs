using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using AdventOfCode2024.Benchmarks;
using AdventOfCode2024.Common;
using AdventOfCode2024.Console;
using Microsoft.Extensions.Configuration;

var puzzleNumbers = HappyPuzzleHelpers.DiscoverPuzzleNumbers(true).ToArray();

var benchmarkTypes = puzzleNumbers.Select(number =>
{
	var assemblyName = new AssemblyName("DynamicAssembly");
	var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
	var moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicModule");
	// Define a new type
	var typeBuilder = moduleBuilder.DefineType("HappyPuzzleBenchmark" + number, TypeAttributes.Public);
	typeBuilder.SetParent(typeof(HappyPuzzleNumberBaseBenchmark));
	return typeBuilder.CreateType();
}).ToArray();

foreach (var benchmarkType in benchmarkTypes)
{
	var instance = (HappyPuzzleNumberBaseBenchmark)Activator.CreateInstance(benchmarkType)!;
	Console.WriteLine(instance.SolveBartPart1().ToString());
}



Console.WriteLine(Directory.GetCurrentDirectory());

var yourName = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
	.AddUserSecrets<Program>(optional: true, reloadOnChange: false)
	.Build()["YourName"] ?? throw new ValidationException("Could not find a 'YourName' key in appsettings.json");

Console.WriteLine($"Hello {yourName}");

var activatedPuzzleRecords = HappyPuzzleHelpers
	.DiscoverPuzzles(true)
	.Select(puzzles =>
	{
		var yourPuzzle = puzzles.Find(puzzle => puzzle.Name.StartsWith(yourName))
		                 ?? throw new MissingMethodException("Could not find a puzzle beginning with 'Bart'");
		return new ActivatorRecord(yourPuzzle.Name, (HappyPuzzleBase) Activator.CreateInstance(yourPuzzle)!);
	})
	.ToList();

foreach (var puzzleRecord in activatedPuzzleRecords)
{
	Console.WriteLine($"=== {puzzleRecord.Name} ".PadRight(80, '='));

	Console.WriteLine("Reading input");
	var input = Helpers.GetInput(puzzleRecord.ActivatedPuzzle.AssetName);

	Console.WriteLine("Solving part 1...");
	Console.WriteLine(puzzleRecord.ActivatedPuzzle.SolvePart1(input));

	Console.WriteLine("\nSolving part 2...");
	Console.WriteLine(puzzleRecord.ActivatedPuzzle.SolvePart2(input));

	Console.WriteLine();
}