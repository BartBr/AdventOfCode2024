```

BenchmarkDotNet v0.14.0, macOS Sequoia 15.1.1 (24B91) [Darwin 24.1.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method     | Categories | Input    | Mean           | Error       | StdDev      | StdErr      | Min            | Q1             | Median         | Q3             | Max            | Op/s        | Allocated |
|----------- |----------- |--------- |---------------:|------------:|------------:|------------:|---------------:|---------------:|---------------:|---------------:|---------------:|------------:|----------:|
| Jens_Part1 | Part 1     | Bart.txt |       6.196 μs |   0.0212 μs |   0.0198 μs |   0.0051 μs |       6.171 μs |       6.181 μs |       6.188 μs |       6.214 μs |       6.226 μs | 161,399.107 |         - |
| Jari_Part1 | Part 1     | Bart.txt |      18.583 μs |   0.0546 μs |   0.0511 μs |   0.0132 μs |      18.495 μs |      18.542 μs |      18.575 μs |      18.617 μs |      18.672 μs |  53,813.661 |         - |
| Bart_Part1 | Part 1     | Bart.txt |      33.846 μs |   0.1159 μs |   0.1084 μs |   0.0280 μs |      33.642 μs |      33.773 μs |      33.854 μs |      33.922 μs |      34.017 μs |  29,545.466 |      80 B |
|            |            |          |                |             |             |             |                |                |                |                |                |             |           |
| Jens_Part1 | Part 1     | Jari.txt |       7.010 μs |   0.0191 μs |   0.0159 μs |   0.0044 μs |       6.993 μs |       7.001 μs |       7.005 μs |       7.019 μs |       7.051 μs | 142,652.614 |         - |
| Jari_Part1 | Part 1     | Jari.txt |      18.905 μs |   0.0572 μs |   0.0535 μs |   0.0138 μs |      18.841 μs |      18.860 μs |      18.896 μs |      18.933 μs |      19.032 μs |  52,896.094 |         - |
| Bart_Part1 | Part 1     | Jari.txt |      34.631 μs |   0.1240 μs |   0.1099 μs |   0.0294 μs |      34.421 μs |      34.572 μs |      34.594 μs |      34.724 μs |      34.804 μs |  28,875.752 |      80 B |
|            |            |          |                |             |             |             |                |                |                |                |                |             |           |
| Jens_Part1 | Part 1     | Jens.txt |       6.809 μs |   0.0197 μs |   0.0165 μs |   0.0046 μs |       6.769 μs |       6.803 μs |       6.806 μs |       6.815 μs |       6.836 μs | 146,867.957 |         - |
| Jari_Part1 | Part 1     | Jens.txt |      22.953 μs |   0.0785 μs |   0.0613 μs |   0.0177 μs |      22.837 μs |      22.921 μs |      22.956 μs |      23.009 μs |      23.016 μs |  43,567.730 |         - |
| Bart_Part1 | Part 1     | Jens.txt |      34.554 μs |   0.1275 μs |   0.1193 μs |   0.0308 μs |      34.378 μs |      34.474 μs |      34.544 μs |      34.653 μs |      34.766 μs |  28,939.973 |      80 B |
|            |            |          |                |             |             |             |                |                |                |                |                |             |           |
| Jari_Part2 | Part 2     | Bart.txt |             NA |          NA |          NA |          NA |             NA |             NA |             NA |             NA |             NA |          NA |        NA |
| Jens_Part2 | Part 2     | Bart.txt |   9,647.412 μs |  23.0206 μs |  20.4072 μs |   5.4540 μs |   9,616.458 μs |   9,633.826 μs |   9,648.455 μs |   9,659.231 μs |   9,686.867 μs |     103.655 |      12 B |
| Bart_Part2 | Part 2     | Bart.txt |  97,105.207 μs | 417.5789 μs | 348.6973 μs |  96.7112 μs |  96,613.576 μs |  96,908.507 μs |  97,055.132 μs |  97,234.639 μs |  98,000.257 μs |      10.298 |     203 B |
|            |            |          |                |             |             |             |                |                |                |                |                |             |           |
| Jari_Part2 | Part 2     | Jari.txt |             NA |          NA |          NA |          NA |             NA |             NA |             NA |             NA |             NA |          NA |        NA |
| Jens_Part2 | Part 2     | Jari.txt |  11,444.935 μs |  43.5701 μs |  38.6237 μs |  10.3226 μs |  11,384.917 μs |  11,412.154 μs |  11,455.509 μs |  11,472.112 μs |  11,512.882 μs |      87.375 |      12 B |
| Bart_Part2 | Part 2     | Jari.txt | 113,091.020 μs | 163.3760 μs | 144.8285 μs |  38.7071 μs | 112,849.408 μs | 112,971.287 μs | 113,067.921 μs | 113,199.619 μs | 113,319.575 μs |       8.842 |     227 B |
|            |            |          |                |             |             |             |                |                |                |                |                |             |           |
| Jari_Part2 | Part 2     | Jens.txt |             NA |          NA |          NA |          NA |             NA |             NA |             NA |             NA |             NA |          NA |        NA |
| Jens_Part2 | Part 2     | Jens.txt |   9,819.373 μs |  23.3192 μs |  21.8128 μs |   5.6320 μs |   9,795.030 μs |   9,801.875 μs |   9,811.299 μs |   9,835.161 μs |   9,864.014 μs |     101.839 |      12 B |
| Bart_Part2 | Part 2     | Jens.txt | 105,131.031 μs | 659.8528 μs | 584.9423 μs | 156.3324 μs | 104,407.300 μs | 104,643.921 μs | 104,966.650 μs | 105,419.162 μs | 106,207.817 μs |       9.512 |     227 B |

Benchmarks with issues:
  Day06Benchmark.Jari_Part2: DefaultJob [Input=Bart.txt]
  Day06Benchmark.Jari_Part2: DefaultJob [Input=Jari.txt]
  Day06Benchmark.Jari_Part2: DefaultJob [Input=Jens.txt]
