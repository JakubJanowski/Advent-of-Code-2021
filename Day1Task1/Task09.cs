using System.Linq;

namespace Tasks {
    internal class Task09: ITask {
        public string Solve(string[] data) {
            int[][] heightMap = data.Select(row => row.Select(digit => (int)(digit - '0')).ToArray()).ToArray();

            int riskLevelSum = 0;
            for (int y = 0; y < heightMap.Length; y++) {
                for (int x = 0; x < heightMap[y].Length; x++) {
                    if (IsLowPoint(heightMap, y, x)) {
                        riskLevelSum += 1 + heightMap[y][x];
                    }
                }
            }

            return riskLevelSum.ToString();
        }

        private bool IsLowPoint(int[][] heightMap, int y, int x) {
            int height = heightMap[y][x];

            if (y > 0 && height >= heightMap[y - 1][x]) {
                return false;
            }
            if (x > 0 && height >= heightMap[y][x - 1]) {
                return false;
            }
            if (y < heightMap.Length - 1 && height >= heightMap[y + 1][x]) {
                return false;
            }
            if (x < heightMap[y].Length - 1 && height >= heightMap[y][x + 1]) {
                return false;
            }

            return true;
        }
    }
}
