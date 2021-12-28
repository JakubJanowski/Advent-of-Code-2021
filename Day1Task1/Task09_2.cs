using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task09_2: ITask {
        class Point {
            public int x;
            public int y;

            public Point(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }

        public string Solve(string[] data) {
            int[][] heightMap = data.Select(row => row.Select(digit => (int)(digit - '0')).ToArray()).ToArray();
            List<Point> points = new();

            for (int y = 0; y < heightMap.Length; y++) {
                for (int x = 0; x < heightMap[y].Length; x++) {
                    if (IsLowPoint(heightMap, y, x)) {
                        points.Add(new Point(x, y));
                    }
                }
            }

            List<int> basins = new();
            foreach (Point point in points) {
                basins.Add(GetBasinSize(heightMap, point));
            }

            return basins.OrderByDescending(b => b).Take(3).Aggregate(1, (x, y) => x * y).ToString();
        }

        private int GetBasinSize(int[][] heightMap, Point point) {
            int basinSize = 1;
            heightMap[point.y][point.x] = 10;

            if (point.y > 0 && heightMap[point.y - 1][point.x] < 9) {
                basinSize += GetBasinSize(heightMap, new Point(point.x, point.y - 1));
            }
            if (point.x > 0 && heightMap[point.y][point.x - 1] < 9) {
                basinSize += GetBasinSize(heightMap, new Point(point.x - 1, point.y));
            }
            if (point.y < heightMap.Length - 1 && heightMap[point.y + 1][point.x] < 9) {
                basinSize += GetBasinSize(heightMap, new Point(point.x, point.y + 1));
            }
            if (point.x < heightMap[point.y].Length - 1 && heightMap[point.y][point.x + 1] < 9) {
                basinSize += GetBasinSize(heightMap, new Point(point.x + 1, point.y));
            }

            return basinSize;
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
