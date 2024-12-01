// See https://aka.ms/new-console-template for more information

using AdventOfCode2023.Benchmarks.Standalone.Puzzles;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.NativeAot;

Console.WriteLine("Hello, World!");

BenchmarkRunner.Run<Day01Benchmark>();