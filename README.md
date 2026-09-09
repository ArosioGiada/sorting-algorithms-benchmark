# Sorting Algorithms Benchmark

A C# program that reads integers from a text file and benchmarks two sorting algorithms — Selection Sort and Quick Sort — comparing their performance on random vs. mostly-sorted data.

## Overview

The program reads a file containing a count on the first line and that many space-separated integers on the second line, sorts a copy of the data with Selection Sort, sorts a fresh copy with Quick Sort, times both with `Stopwatch`, and reports which was faster. It runs interactively, so you can test multiple files in the same session.

## Algorithms

- **Selection Sort**: repeatedly scans the unsorted portion of the array to find its maximum value, then swaps it into place at the end. O(n²) always — its performance doesn't improve on partially sorted data, since it always scans the full remaining range regardless of order.
- **Quick Sort**: recursive partition-based sort. The pivot is chosen as the **median of the first, middle, and last elements** of each segment rather than a fixed position. This avoids Quick Sort's classic worst case (O(n²)) on data that is already sorted or nearly sorted — which matters directly here, since one of the test files is mostly sorted.

## Sample results

| Dataset | Selection Sort | Quick Sort | Faster |
|---|---|---|---|
| `input_random.txt` (fully random) | 35.003 ms | 0.729 ms | Quick Sort |
| `input_mostly_sorted.txt` (sorted, first value out of place) | 34.422 ms | 0.292 ms | Quick Sort |

Quick Sort wins in both cases, but the gap widens on the mostly-sorted file — its runtime actually *drops*, while Selection Sort's stays essentially flat. This is the expected behaviour: Selection Sort has no way to exploit existing order, while Quick Sort's median-of-three pivot keeps partitioning balanced even when the data is nearly in order.

## How to run

Run this command in the project folder:

    dotnet run

When prompted, enter the full path to an input file (no surrounding quotes) — e.g. `input_random.txt`. Press Enter with no input to exit.

## Input file format

    <count>
    <count space-separated integers, all on one line>

Two sample files are included:

- `input_random.txt` — fully random integers
- `input_mostly_sorted.txt` — sorted data with only the first value out of sequence

## Author

Giada Arosio — built for the Algorithms and Data Structures unit at Torrens University Australia.
