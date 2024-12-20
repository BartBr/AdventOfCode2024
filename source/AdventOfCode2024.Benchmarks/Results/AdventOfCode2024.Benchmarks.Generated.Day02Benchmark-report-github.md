```

BenchmarkDotNet v0.14.0, macOS Sequoia 15.1.1 (24B91) [Darwin 24.1.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method     | Categories | Input    | Mean      | Error    | StdDev   | StdErr   | Min       | Q1        | Median    | Q3        | Max       | Op/s     | Gen0    | Allocated |
|----------- |----------- |--------- |----------:|---------:|---------:|---------:|----------:|----------:|----------:|----------:|----------:|---------:|--------:|----------:|
| Bart_Part1 | Part 1     | Bart.txt |  23.79 μs | 0.108 μs | 0.090 μs | 0.025 μs |  23.60 μs |  23.74 μs |  23.79 μs |  23.84 μs |  23.95 μs | 42,033.8 |       - |         - |
| Jens_Part1 | Part 1     | Bart.txt |  27.01 μs | 0.086 μs | 0.076 μs | 0.020 μs |  26.87 μs |  26.97 μs |  27.00 μs |  27.05 μs |  27.15 μs | 37,017.2 |       - |         - |
| Jari_Part1 | Part 1     | Bart.txt | 177.60 μs | 0.558 μs | 0.495 μs | 0.132 μs | 176.83 μs | 177.32 μs | 177.61 μs | 177.91 μs | 178.60 μs |  5,630.7 | 65.6738 |  412712 B |
|            |            |          |           |          |          |          |           |           |           |           |           |          |         |           |
| Bart_Part1 | Part 1     | Jari.txt |  24.59 μs | 0.060 μs | 0.056 μs | 0.015 μs |  24.51 μs |  24.55 μs |  24.60 μs |  24.64 μs |  24.68 μs | 40,659.4 |       - |         - |
| Jens_Part1 | Part 1     | Jari.txt |  33.94 μs | 0.175 μs | 0.155 μs | 0.042 μs |  33.70 μs |  33.83 μs |  33.92 μs |  34.04 μs |  34.25 μs | 29,464.4 |       - |         - |
| Jari_Part1 | Part 1     | Jari.txt | 188.74 μs | 0.498 μs | 0.442 μs | 0.118 μs | 188.04 μs | 188.46 μs | 188.61 μs | 189.09 μs | 189.53 μs |  5,298.3 | 65.4297 |  411760 B |
|            |            |          |           |          |          |          |           |           |           |           |           |          |         |           |
| Bart_Part1 | Part 1     | Jens.txt |  24.14 μs | 0.080 μs | 0.075 μs | 0.019 μs |  24.03 μs |  24.09 μs |  24.12 μs |  24.18 μs |  24.30 μs | 41,431.6 |       - |         - |
| Jens_Part1 | Part 1     | Jens.txt |  29.59 μs | 0.090 μs | 0.084 μs | 0.022 μs |  29.47 μs |  29.51 μs |  29.59 μs |  29.65 μs |  29.73 μs | 33,796.4 |       - |         - |
| Jari_Part1 | Part 1     | Jens.txt | 178.31 μs | 1.062 μs | 0.829 μs | 0.239 μs | 176.99 μs | 177.85 μs | 178.28 μs | 178.72 μs | 180.11 μs |  5,608.1 | 65.6738 |  412808 B |
|            |            |          |           |          |          |          |           |           |           |           |           |          |         |           |
| Bart_Part2 | Part 2     | Bart.txt |  60.23 μs | 0.486 μs | 0.430 μs | 0.115 μs |  59.76 μs |  59.88 μs |  59.99 μs |  60.61 μs |  61.08 μs | 16,603.4 |       - |         - |
| Jens_Part2 | Part 2     | Bart.txt |  87.20 μs | 0.381 μs | 0.356 μs | 0.092 μs |  86.69 μs |  87.01 μs |  87.09 μs |  87.43 μs |  87.86 μs | 11,467.5 |       - |         - |
| Jari_Part2 | Part 2     | Bart.txt | 179.97 μs | 0.591 μs | 0.493 μs | 0.137 μs | 179.18 μs | 179.65 μs | 179.98 μs | 180.19 μs | 180.72 μs |  5,556.5 | 65.6738 |  412712 B |
|            |            |          |           |          |          |          |           |           |           |           |           |          |         |           |
| Bart_Part2 | Part 2     | Jari.txt |  53.61 μs | 0.126 μs | 0.118 μs | 0.030 μs |  53.46 μs |  53.51 μs |  53.58 μs |  53.70 μs |  53.87 μs | 18,654.9 |       - |         - |
| Jens_Part2 | Part 2     | Jari.txt |  78.85 μs | 0.383 μs | 0.299 μs | 0.086 μs |  78.41 μs |  78.57 μs |  78.89 μs |  79.02 μs |  79.30 μs | 12,683.1 |       - |         - |
| Jari_Part2 | Part 2     | Jari.txt | 179.56 μs | 0.494 μs | 0.438 μs | 0.117 μs | 178.85 μs | 179.27 μs | 179.57 μs | 179.89 μs | 180.35 μs |  5,569.2 | 65.4297 |  411760 B |
|            |            |          |           |          |          |          |           |           |           |           |           |          |         |           |
| Bart_Part2 | Part 2     | Jens.txt |  56.82 μs | 0.216 μs | 0.202 μs | 0.052 μs |  56.49 μs |  56.69 μs |  56.78 μs |  56.99 μs |  57.20 μs | 17,600.5 |       - |         - |
| Jens_Part2 | Part 2     | Jens.txt |  83.74 μs | 0.424 μs | 0.397 μs | 0.102 μs |  83.29 μs |  83.43 μs |  83.68 μs |  84.02 μs |  84.55 μs | 11,941.6 |       - |         - |
| Jari_Part2 | Part 2     | Jens.txt | 220.78 μs | 0.750 μs | 0.701 μs | 0.181 μs | 219.91 μs | 220.20 μs | 220.80 μs | 221.15 μs | 222.38 μs |  4,529.3 | 65.6738 |  412808 B |
