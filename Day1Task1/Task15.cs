using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task15: ITask {
        class Point {
            public int x;
            public int y;

            public Point(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }

        public string Solve(string[] data) {
            byte[][] map = data.Select(row => row.Select(digit => (byte)(digit - '0')).ToArray()).ToArray();
            
            int[][] totalRiskMap = new int[map.Length][];
            for (int i = 0; i < map.Length; i++) {
                totalRiskMap[i] = new int[map[i].Length];
                for (int j = 0; j < totalRiskMap[i].Length; j++) {
                    totalRiskMap[i][j] = int.MaxValue;
                }
            }

            totalRiskMap[0][0] = 0;
            Queue<Point> cavesToCheck = new();
            cavesToCheck.Enqueue(new Point(0, 0));

            while (cavesToCheck.Count > 0) {
                Point coord = cavesToCheck.Dequeue();
                int cost;

                if (coord.y > 0) {
                    cost = totalRiskMap[coord.y][coord.x] + map[coord.y - 1][coord.x];
                    if (cost < totalRiskMap[coord.y - 1][coord.x] ) {
                        totalRiskMap[coord.y - 1][coord.x] = cost;
                        cavesToCheck.Enqueue(new Point(coord.x, coord.y - 1));
                    }
                }
                if (coord.x > 0) {
                    cost = totalRiskMap[coord.y][coord.x] + map[coord.y][coord.x - 1];
                    if (cost < totalRiskMap[coord.y][coord.x - 1]) {
                        totalRiskMap[coord.y][coord.x - 1] = cost;
                        cavesToCheck.Enqueue(new Point(coord.x - 1, coord.y));
                    }
                }
                if (coord.y < map.Length - 1) {
                    cost = totalRiskMap[coord.y][coord.x] + map[coord.y + 1][coord.x];
                    if (cost < totalRiskMap[coord.y + 1][coord.x]) {
                        totalRiskMap[coord.y + 1][coord.x] = cost;
                        cavesToCheck.Enqueue(new Point(coord.x, coord.y + 1));
                    }
                }
                if (coord.x < map[coord.y].Length - 1) {
                    cost = totalRiskMap[coord.y][coord.x] + map[coord.y][coord.x + 1];
                    if (cost < totalRiskMap[coord.y][coord.x + 1]) {
                        totalRiskMap[coord.y][coord.x + 1] = cost;
                        cavesToCheck.Enqueue(new Point(coord.x + 1, coord.y));
                    }
                }
            }


            return totalRiskMap[^1][^1].ToString();
        }
    }
}
