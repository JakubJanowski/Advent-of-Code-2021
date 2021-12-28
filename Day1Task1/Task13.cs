using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task13: ITask {
        class Point {
            public int x;
            public int y;

            public Point(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }

        enum Axis {
            X, Y
        }

        class FoldInstruction {
            public Axis Axis;
            public int Coordinate;
        }

        public string Solve(string[] data) {
            bool[][] map = new bool[895][];
            //bool[][] map = new bool[15][];
            for (int i = 0; i < map.Length; i++) {
                map[i] = new bool[1311];
                //map[i] = new bool[11];
            }

            int dataIndex = 0;
            for (; ; dataIndex++) {
                string entry = data[dataIndex];
                if (string.IsNullOrEmpty(entry)) {
                    break;
                }
                int[] coordinates = entry.Split(',').Select(e => int.Parse(e)).ToArray();
                map[coordinates[1]][coordinates[0]] = true;
            }
            dataIndex++;

            List<FoldInstruction> foldInstructions = new();
            FoldInstruction fold = new();
            if (data[dataIndex].Contains("x=")) {
                fold.Axis = Axis.X;
            }
            else {
                fold.Axis = Axis.Y;
            }
            fold.Coordinate = int.Parse(data[dataIndex].Substring(data[dataIndex].LastIndexOf('=') + 1));
            foldInstructions.Add(fold);

            int height = map.Length;
            int width = map[0].Length;
            //Print(map, height, width);
            foreach (FoldInstruction f in foldInstructions) {
                Fold(map, f, height, width);
                switch (fold.Axis) {
                    case Axis.X:
                        width = f.Coordinate;
                        break;
                    case Axis.Y:
                        height = f.Coordinate;
                        break;
                }
                //Print(map, height, width);
            }

            int count = 0;

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if (map[y][x]) {
                        count++;
                    }
                }
            }
            return count.ToString();
        }

        private void Fold(bool[][] map, FoldInstruction fold, int height, int width) {
            switch (fold.Axis) {
                case Axis.X:
                    for (int y = 0; y < height; y++) {
                        for (int x = 0; x < fold.Coordinate; x++) {
                            map[y][x] |= map[y][width - x - 1];
                        }
                    }
                    break;
                case Axis.Y:
                    for (int y = 0; y < fold.Coordinate; y++) {
                        for (int x = 0; x < width; x++) {
                            map[y][x] |= map[height - y - 1][x];
                        }
                    }
                    break;
            }
        }

        private void Print(bool[][] map, int height, int width) {
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if (map[y][x]) {
                        Console.Write("#");
                    } else {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
