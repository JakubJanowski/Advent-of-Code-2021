using System;
using System.Linq;

namespace Tasks {
    internal class Task11_2: ITask {
        public string Solve(string[] data) {
            int[][] energyMap = data.Select(row => row.Select(digit => (int)(digit - '0')).ToArray()).ToArray();

            for (int i = 1; ; i++) {
                Print(energyMap);
                if(Step(energyMap) == energyMap.Length * energyMap[0].Length) {
                    Print(energyMap);
                    return i.ToString();
                }
            }
        }

        private int Step(int[][] energyMap) {
            int flashCount = 0;
            for (int y = 0; y < energyMap.Length; y++) {
                for (int x = 0; x < energyMap[y].Length; x++) {
                    energyMap[y][x]++;
                }
            }

            bool didAnyFlash;
            do {
                didAnyFlash = false;
                for (int y = 0; y < energyMap.Length; y++) {
                    for (int x = 0; x < energyMap[y].Length; x++) {
                        if (energyMap[y][x] > 9) {
                            UpdateNeighbors(energyMap, y, x);
                            flashCount++;
                            didAnyFlash = true;
                        }
                    }
                }
            } while (didAnyFlash);

            for (int y = 0; y < energyMap.Length; y++) {
                for (int x = 0; x < energyMap[y].Length; x++) {
                    if (energyMap[y][x] < 0) {
                        energyMap[y][x] = 0;
                    }
                }
            }

            return flashCount;
        }

        private void UpdateNeighbors(int[][] energyMap, int y, int x) {
            if (y > 0 && x > 0) {
                energyMap[y - 1][x - 1]++;
            }
            if (y > 0) {
                energyMap[y - 1][x]++;
            }
            if (y > 0 && x < energyMap[y].Length - 1) {
                energyMap[y - 1][x + 1]++;
            }
            if (x > 0) {
                energyMap[y][x - 1]++;
            }
            if (x < energyMap[y].Length - 1) {
                energyMap[y][x + 1]++;
            }
            if (y < energyMap.Length - 1 && x > 0) {
                energyMap[y + 1][x - 1]++;
            }
            if (y < energyMap.Length - 1) {
                energyMap[y + 1][x]++;
            }
            if (y < energyMap.Length - 1 && x < energyMap[y].Length - 1) {
                energyMap[y + 1][x + 1]++;
            }
            energyMap[y][x] = int.MinValue;
        }

        private void Print(int[][] energyMap) {
            for (int y = 0; y < energyMap.Length; y++) {
                for (int x = 0; x < energyMap[y].Length; x++) {
                    Console.Write(energyMap[y][x]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
