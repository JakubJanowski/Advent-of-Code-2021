using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task04: ITask {
        public string Solve(string[] data) {
            int[] numbers = data[0].Split(',').Where(d => !string.IsNullOrWhiteSpace(d)).Select(d => int.Parse(d)).ToArray();
            List<int[][]> bingoMaps = new List<int[][]>();
            List<bool[][]> markMaps = new List<bool[][]>();

            for (int i = 2; i < data.Length; i += 6) {
                int[][] bingoMap = new int[5][];
                bool[][] markMap = new bool[5][];
                for (int y = 0; y < 5; y++) {
                    bingoMap[y] = data[i + y].Split(' ').Where(d => !string.IsNullOrWhiteSpace(d)).Select(d => int.Parse(d)).ToArray();
                    markMap[y] = new bool[5];
                }
                bingoMaps.Add(bingoMap);
                markMaps.Add(markMap);
            }

            foreach (int number in numbers) {
                for (int i = 0; i < bingoMaps.Count; i++) {
                    if (MarkNumber(markMaps[i], bingoMaps[i], number) && CheckIfSolved(markMaps[i], bingoMaps[i])) {
                        return (CountSum(markMaps[i], bingoMaps[i]) * number).ToString();
                    }
                }
            }

            return "dupa";
        }

        private int CountSum(bool[][] markMap, int[][] bingoMap) {
            int sum = 0;
            for (int y = 0; y < 5; y++) {
                for (int x = 0; x < 5; x++) {
                    if (!markMap[y][x]) {
                        sum += bingoMap[y][x];
                    }
                }
            }
            return sum;
        }

        private bool MarkNumber(bool[][] markMap, int[][] bingoMap, int number) {
            bool result = false;
            for (int y = 0; y < 5; y++) {
                for (int x = 0; x < 5; x++) {
                    if (bingoMap[y][x] == number) {
                        markMap[y][x] = true;
                        result = true;
                    }
                }
            }
            return result;
        }

        private bool CheckIfSolved(bool[][] markMap, int[][] bingoMap) {
            for (int y = 0; y < 5; y++) {
                bool isRowMatched = true;

                for (int x = 0; x < 5; x++) {
                    if (!markMap[y][x]) {
                        isRowMatched = false;
                        break;
                    }
                }

                if (isRowMatched) {
                    return true;
                }
            }

            for (int x = 0; x < 5; x++) {
                bool isColumnMatched = true;

                for (int y = 0; y < 5; y++) {
                    if (!markMap[y][x]) {
                        isColumnMatched = false;
                        break;
                    }
                }

                if (isColumnMatched) {
                    return true;
                }
            }

            return false;
        }
    }
}
