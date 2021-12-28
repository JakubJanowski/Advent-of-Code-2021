using System.Linq;

namespace Tasks {
    internal class Task22: ITask {
        public string Solve(string[] data) {
            bool[][][] grid = new bool[101][][];
            for (int z = 0; z < grid.Length; z++) {
                grid[z] = new bool[101][];
                for (int y = 0; y < grid.Length; y++) {
                    grid[z][y] = new bool[101];
                }
            }

            foreach (string step in data) {
                bool state = step[0..2] == "on";
                int xIndex = step.IndexOf("x=");
                int xRangeIndex = step.IndexOf("..", xIndex);
                int yIndex = step.IndexOf("y=");
                int yRangeIndex = step.IndexOf("..", yIndex);
                int zIndex = step.IndexOf("z=");
                int zRangeIndex = step.IndexOf("..", zIndex);
                int xLow = int.Parse(step.Substring(xIndex + 2, xRangeIndex - (xIndex + 2)));
                int xHigh = int.Parse(step.Substring(xRangeIndex + 2, yIndex - 1 - (xRangeIndex + 2)));
                int yLow = int.Parse(step.Substring(yIndex + 2, yRangeIndex - (yIndex + 2)));
                int yHigh = int.Parse(step.Substring(yRangeIndex + 2, zIndex - 1 - (yRangeIndex + 2)));
                int zLow = int.Parse(step.Substring(zIndex + 2, zRangeIndex - (zIndex + 2)));
                int zHigh = int.Parse(step.Substring(zRangeIndex + 2));

                if (!(IsInRange(xLow, -50, 50) && IsInRange(xHigh, -50, 50)
                     && IsInRange(yLow, -50, 50) && IsInRange(yHigh, -50, 50)
                     && IsInRange(zLow, -50, 50) && IsInRange(zHigh, -50, 50))) {
                    continue;
                }

                for (int z = zLow + 50; z <= zHigh + 50; z++) {
                    for (int y = yLow + 50; y <= yHigh + 50; y++) {
                        for (int x = xLow + 50; x <= xHigh + 50; x++) {
                            grid[z][y][x] = state;
                        }
                    }
                }
            }

            return grid.Sum(p => p.Sum(r => r.Count(c => c))).ToString();
        }

        private static bool IsInRange(int n, int from, int to) {
            return n >= from && n <= to;
        }
    }
}
