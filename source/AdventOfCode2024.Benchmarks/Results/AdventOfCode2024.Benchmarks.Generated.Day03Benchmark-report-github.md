```

BenchmarkDotNet v0.14.0, macOS Sequoia 15.1.1 (24B91) [Darwin 24.1.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method     | Categories | Input    | Mean      | Error     | StdDev    | StdErr    | Min       | Q1        | Median    | Q3        | Max       | Op/s      | Allocated |
|----------- |----------- |--------- |----------:|----------:|----------:|----------:|----------:|----------:|----------:|----------:|----------:|----------:|----------:|
| Jens_Part1 | Part 1     | Bart.txt | 10.787 μs | 0.1001 μs | 0.0937 μs | 0.0242 μs | 10.610 μs | 10.715 μs | 10.780 μs | 10.842 μs | 10.962 μs |  92,701.9 |         - |
| Bart_Part1 | Part 1     | Bart.txt | 12.047 μs | 0.0422 μs | 0.0353 μs | 0.0098 μs | 11.993 μs | 12.025 μs | 12.046 μs | 12.071 μs | 12.106 μs |  83,010.4 |         - |
| Jari_Part1 | Part 1     | Bart.txt | 13.374 μs | 0.2176 μs | 0.2036 μs | 0.0526 μs | 13.010 μs | 13.244 μs | 13.373 μs | 13.498 μs | 13.838 μs |  74,769.6 |         - |
|            |            |          |           |           |           |           |           |           |           |           |           |           |           |
| Jens_Part1 | Part 1     | Jari.txt | 10.764 μs | 0.0937 μs | 0.0876 μs | 0.0226 μs | 10.589 μs | 10.724 μs | 10.762 μs | 10.825 μs | 10.891 μs |  92,904.1 |         - |
| Bart_Part1 | Part 1     | Jari.txt | 12.131 μs | 0.0359 μs | 0.0318 μs | 0.0085 μs | 12.082 μs | 12.110 μs | 12.130 μs | 12.144 μs | 12.202 μs |  82,433.0 |         - |
| Jari_Part1 | Part 1     | Jari.txt | 13.259 μs | 0.2645 μs | 0.2598 μs | 0.0650 μs | 12.740 μs | 13.107 μs | 13.191 μs | 13.392 μs | 13.796 μs |  75,419.9 |         - |
|            |            |          |           |           |           |           |           |           |           |           |           |           |           |
| Jens_Part1 | Part 1     | Jens.txt |  9.979 μs | 0.1014 μs | 0.0949 μs | 0.0245 μs |  9.808 μs |  9.941 μs |  9.977 μs | 10.032 μs | 10.161 μs | 100,210.2 |         - |
| Bart_Part1 | Part 1     | Jens.txt | 11.262 μs | 0.0678 μs | 0.0634 μs | 0.0164 μs | 11.196 μs | 11.217 μs | 11.233 μs | 11.307 μs | 11.382 μs |  88,791.4 |         - |
| Jari_Part1 | Part 1     | Jens.txt | 11.804 μs | 0.2224 μs | 0.2284 μs | 0.0554 μs | 11.316 μs | 11.721 μs | 11.814 μs | 11.935 μs | 12.195 μs |  84,717.8 |         - |
|            |            |          |           |           |           |           |           |           |           |           |           |           |           |
| Bart_Part2 | Part 2     | Bart.txt |  6.375 μs | 0.0134 μs | 0.0119 μs | 0.0032 μs |  6.351 μs |  6.371 μs |  6.378 μs |  6.381 μs |  6.395 μs | 156,874.3 |      32 B |
| Jens_Part2 | Part 2     | Bart.txt | 16.124 μs | 0.0327 μs | 0.0273 μs | 0.0076 μs | 16.075 μs | 16.109 μs | 16.129 μs | 16.134 μs | 16.184 μs |  62,018.3 |         - |
| Jari_Part2 | Part 2     | Bart.txt | 23.004 μs | 0.0522 μs | 0.0462 μs | 0.0124 μs | 22.915 μs | 22.978 μs | 23.014 μs | 23.026 μs | 23.089 μs |  43,470.8 |         - |
|            |            |          |           |           |           |           |           |           |           |           |           |           |           |
| Bart_Part2 | Part 2     | Jari.txt |  6.940 μs | 0.0231 μs | 0.0216 μs | 0.0056 μs |  6.911 μs |  6.923 μs |  6.940 μs |  6.952 μs |  6.985 μs | 144,092.2 |      32 B |
| Jens_Part2 | Part 2     | Jari.txt | 16.434 μs | 0.0346 μs | 0.0307 μs | 0.0082 μs | 16.400 μs | 16.408 μs | 16.426 μs | 16.454 μs | 16.501 μs |  60,850.0 |         - |
| Jari_Part2 | Part 2     | Jari.txt | 20.421 μs | 0.1289 μs | 0.1077 μs | 0.0299 μs | 20.293 μs | 20.366 μs | 20.381 μs | 20.438 μs | 20.708 μs |  48,968.3 |         - |
|            |            |          |           |           |           |           |           |           |           |           |           |           |           |
| Bart_Part2 | Part 2     | Jens.txt |  8.186 μs | 0.0176 μs | 0.0156 μs | 0.0042 μs |  8.160 μs |  8.179 μs |  8.188 μs |  8.192 μs |  8.219 μs | 122,157.5 |      32 B |
| Jens_Part2 | Part 2     | Jens.txt | 14.773 μs | 0.0426 μs | 0.0398 μs | 0.0103 μs | 14.719 μs | 14.748 μs | 14.761 μs | 14.792 μs | 14.853 μs |  67,690.2 |         - |
| Jari_Part2 | Part 2     | Jens.txt | 18.432 μs | 0.0470 μs | 0.0440 μs | 0.0114 μs | 18.345 μs | 18.411 μs | 18.427 μs | 18.460 μs | 18.509 μs |  54,253.4 |         - |
