```

BenchmarkDotNet v0.14.0, macOS Sequoia 15.1.1 (24B91) [Darwin 24.1.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method         | Categories | Input    | Mean     | Error    | StdDev   | StdErr   | Min      | Q1       | Median   | Q3       | Max      | Op/s     | Gen0   | Allocated |
|--------------- |----------- |--------- |---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|-------:|----------:|
| Jens_Part1     | Part 1     | Bart.txt | 22.93 μs | 0.432 μs | 0.383 μs | 0.102 μs | 22.09 μs | 22.94 μs | 23.03 μs | 23.14 μs | 23.44 μs | 43,603.5 |      - |         - |
| Jari_Part1     | Part 1     | Bart.txt | 30.04 μs | 0.203 μs | 0.180 μs | 0.048 μs | 29.79 μs | 29.92 μs | 30.04 μs | 30.15 μs | 30.38 μs | 33,292.9 | 1.2817 |    8112 B |
| Bart_Part1     | Part 1     | Bart.txt | 33.70 μs | 0.669 μs | 1.223 μs | 0.189 μs | 31.28 μs | 33.11 μs | 33.79 μs | 34.52 μs | 36.44 μs | 29,673.5 |      - |         - |
| Combined_Part1 | Part 1     | Bart.txt | 34.63 μs | 0.689 μs | 1.482 μs | 0.198 μs | 31.55 μs | 33.44 μs | 34.84 μs | 35.62 μs | 38.25 μs | 28,880.9 |      - |         - |
|                |            |          |          |          |          |          |          |          |          |          |          |          |        |           |
| Jens_Part1     | Part 1     | Jari.txt | 22.47 μs | 0.442 μs | 0.454 μs | 0.110 μs | 21.74 μs | 22.05 μs | 22.53 μs | 22.89 μs | 23.17 μs | 44,498.9 |      - |         - |
| Combined_Part1 | Part 1     | Jari.txt | 25.90 μs | 0.513 μs | 0.736 μs | 0.139 μs | 24.47 μs | 25.50 μs | 26.06 μs | 26.47 μs | 27.09 μs | 38,607.3 |      - |         - |
| Bart_Part1     | Part 1     | Jari.txt | 39.05 μs | 0.764 μs | 1.358 μs | 0.215 μs | 36.46 μs | 38.17 μs | 38.90 μs | 40.10 μs | 42.16 μs | 25,606.6 |      - |         - |
| Jari_Part1     | Part 1     | Jari.txt | 42.27 μs | 0.842 μs | 1.360 μs | 0.233 μs | 39.51 μs | 41.27 μs | 42.16 μs | 43.14 μs | 45.94 μs | 23,659.8 | 1.2817 |    8112 B |
|                |            |          |          |          |          |          |          |          |          |          |          |          |        |           |
| Jens_Part1     | Part 1     | Jens.txt | 22.65 μs | 0.341 μs | 0.319 μs | 0.082 μs | 21.97 μs | 22.49 μs | 22.53 μs | 22.95 μs | 23.10 μs | 44,148.4 |      - |         - |
| Combined_Part1 | Part 1     | Jens.txt | 34.05 μs | 0.659 μs | 1.222 μs | 0.186 μs | 31.53 μs | 33.17 μs | 34.00 μs | 34.91 μs | 36.67 μs | 29,365.8 |      - |         - |
| Bart_Part1     | Part 1     | Jens.txt | 40.62 μs | 0.792 μs | 1.209 μs | 0.217 μs | 38.76 μs | 39.77 μs | 40.52 μs | 41.20 μs | 43.18 μs | 24,618.7 |      - |         - |
| Jari_Part1     | Part 1     | Jens.txt | 42.78 μs | 0.839 μs | 1.448 μs | 0.235 μs | 39.68 μs | 41.99 μs | 43.13 μs | 43.72 μs | 46.01 μs | 23,373.9 | 1.2817 |    8112 B |
|                |            |          |          |          |          |          |          |          |          |          |          |          |        |           |
| Jens_Part2     | Part 2     | Bart.txt |       NA |       NA |       NA |       NA |       NA |       NA |       NA |       NA |       NA |       NA |     NA |        NA |
| Combined_Part2 | Part 2     | Bart.txt | 18.11 μs | 0.038 μs | 0.030 μs | 0.009 μs | 18.05 μs | 18.09 μs | 18.10 μs | 18.13 μs | 18.16 μs | 55,229.3 |      - |         - |
| Bart_Part2     | Part 2     | Bart.txt | 23.94 μs | 0.177 μs | 0.157 μs | 0.042 μs | 23.77 μs | 23.84 μs | 23.87 μs | 24.07 μs | 24.21 μs | 41,767.1 |      - |         - |
| Jari_Part2     | Part 2     | Bart.txt | 38.72 μs | 0.186 μs | 0.155 μs | 0.043 μs | 38.51 μs | 38.64 μs | 38.67 μs | 38.80 μs | 39.06 μs | 25,827.5 | 4.1504 |   26248 B |
|                |            |          |          |          |          |          |          |          |          |          |          |          |        |           |
| Jens_Part2     | Part 2     | Jari.txt |       NA |       NA |       NA |       NA |       NA |       NA |       NA |       NA |       NA |       NA |     NA |        NA |
| Combined_Part2 | Part 2     | Jari.txt | 18.21 μs | 0.090 μs | 0.080 μs | 0.021 μs | 18.11 μs | 18.14 μs | 18.21 μs | 18.26 μs | 18.37 μs | 54,901.4 |      - |         - |
| Bart_Part2     | Part 2     | Jari.txt | 23.88 μs | 0.167 μs | 0.139 μs | 0.039 μs | 23.73 μs | 23.76 μs | 23.85 μs | 23.92 μs | 24.23 μs | 41,879.6 |      - |         - |
| Jari_Part2     | Part 2     | Jari.txt | 39.61 μs | 0.128 μs | 0.107 μs | 0.030 μs | 39.38 μs | 39.58 μs | 39.62 μs | 39.70 μs | 39.76 μs | 25,243.6 | 4.1504 |   26248 B |
|                |            |          |          |          |          |          |          |          |          |          |          |          |        |           |
| Combined_Part2 | Part 2     | Jens.txt | 18.15 μs | 0.045 μs | 0.040 μs | 0.011 μs | 18.10 μs | 18.12 μs | 18.14 μs | 18.18 μs | 18.22 μs | 55,090.1 |      - |         - |
| Bart_Part2     | Part 2     | Jens.txt | 23.86 μs | 0.092 μs | 0.076 μs | 0.021 μs | 23.72 μs | 23.81 μs | 23.85 μs | 23.92 μs | 23.98 μs | 41,907.0 |      - |         - |
| Jens_Part2     | Part 2     | Jens.txt | 24.81 μs | 0.440 μs | 0.452 μs | 0.110 μs | 24.01 μs | 24.50 μs | 24.89 μs | 25.07 μs | 25.62 μs | 40,307.5 |      - |         - |
| Jari_Part2     | Part 2     | Jens.txt | 39.30 μs | 0.309 μs | 0.289 μs | 0.075 μs | 38.89 μs | 39.09 μs | 39.23 μs | 39.51 μs | 39.87 μs | 25,444.0 | 4.1504 |   26248 B |

Benchmarks with issues:
  Day01Benchmark.Jens_Part2: DefaultJob [Input=Bart.txt]
  Day01Benchmark.Jens_Part2: DefaultJob [Input=Jari.txt]
