using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task05: ITask {
        class Point {
            public int x;
            public int y;
        }
        class Line {
            public Point start;
            public Point end;
        }
        public string Solve(string[] data) {
            Line[] lines = data.Select(d => {
                int[] numbers = d.Split(',').Select(n => int.Parse(n)).ToArray();
                return new Line() {
                    start = new Point() { x = numbers [0], y = numbers [1]},
                    end = new Point() { x = numbers [2], y = numbers [3]}
                };
            }).ToArray();

            const int width = 1000;
            const int height = width;
            int[][] map = new int[height][];
            for (int i = 0; i < height; i++) {
                map[i] = new int[width];
            }

            foreach (Line line in lines) {
                if (line.start.x == line.end.x) {
                    int startY = Math.Min(line.start.y, line.end.y);
                    int endY = Math.Max(line.start.y, line.end.y);
                    for (int y = startY; y <= endY; y++) {
                        map[y][line.end.x]++;
                    }
                }
                else if (line.start.y == line.end.y) {
                    int startX = Math.Min(line.start.x, line.end.x);
                    int endX = Math.Max(line.start.x, line.end.x);
                    for (int x = startX; x <= endX; x++) {
                        map[line.end.y][x]++;
                    }
                }
            }

            return map.Sum(l => l.Count(c => c > 1)).ToString();
        }
    }
}
