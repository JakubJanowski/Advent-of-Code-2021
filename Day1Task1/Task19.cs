using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task19: ITask {
        public class Point: IEquatable<Point> {
            public Point(int x, int y, int z) {
                X = x;
                Y = y;
                Z = z;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }

            public static Point operator +(Point p1, Point p2) {
                return new Point(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
            }

            public override int GetHashCode() {
                return 91 * (X + (Y << 10) + (Z << 20));
            }

            public override bool Equals(object? obj) {
                if(obj is null) return false;
                if(obj == this) return true;
                if(obj.GetType() != typeof(Point)) return false;
                return Equals((Point)obj);
            }

            public bool Equals(Point? p) {
                if (p is null) return false;
                return X == p.X && Y == p.Y && Z == p.Z;
            }

            public override string ToString() {
                return $"{X},{Y},{Z}";
            }

            internal void Transform(Point translation, sbyte[][] rotation) {
                int[] coords = new int[] { X, Y, Z };
                X = rotation[1][0] * coords[rotation[0][0]] + translation.X;
                Y = rotation[1][1] * coords[rotation[0][1]] + translation.Y;
                Z = rotation[1][2] * coords[rotation[0][2]] + translation.Z;
            }
        }
        public class Beacon {
            public Beacon(Point position) {
                Position = position;
            }

            public Point Position { get; set; }

            public void Transform(Point translation,  sbyte[][] rotation) {
                Position.Transform(translation, rotation);
            }
        }

        public class Scanner {
            public Scanner() {
                Beacons = new();
            }

            public List<Beacon> Beacons { get; set; }
            public Point? Position { get; set; }

            public void Transform(Point translation,  sbyte[][] rotation) {
                foreach (Beacon beacon in Beacons) {
                    beacon.Transform(translation, rotation);
                }
            }
        }

        public string Solve(string[] data) {
            List<Scanner> scanners = new();
            Scanner scanner = new();
            scanner.Position = new Point(0, 0, 0);
            scanners.Add(scanner);

            for (int i = 1; i < data.Length; i++) {
                string row = data[i];
                if (string.IsNullOrWhiteSpace(row)) {
                    continue;
                }
                if (row.Contains("scanner")) {
                    scanner = new();
                    scanners.Add(scanner);
                    continue;
                }
                string[] split = data[i].Split(",");
                scanner!.Beacons.Add(new Beacon(new Point(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]))));
            }

            while (scanners.Any(s => s.Position is null)) {
                foreach (Scanner scanner1 in scanners.Where(s => s.Position is not null)) {
                    foreach (Scanner scanner2 in scanners.Where(s => s.Position is null)) {
                        Overlap(scanner1, scanner2);
                    }
                }
            }

            return scanners.SelectMany(s => s.Beacons).Select(b => b.Position).Distinct().Count().ToString();
        }


        private void Overlap(Scanner scanner1, Scanner scanner2) {
            if (scanner1.Position is null) {
                throw new Exception("Expected scanner to have defined position");
            }

            sbyte[][][] rotations = new sbyte[][][] {
                new sbyte[][] { new sbyte[] { 0, 1, 2 }, new sbyte[] { 1, 1, 1 } },
                new sbyte[][] { new sbyte[] { 0, 1, 2 }, new sbyte[] { -1, -1, 1 } },
                new sbyte[][] { new sbyte[] { 0, 1, 2 }, new sbyte[] { -1, 1, -1 } },
                new sbyte[][] { new sbyte[] { 0, 1, 2 }, new sbyte[] { 1, -1, -1 } },
                new sbyte[][] { new sbyte[] { 0, 2, 1 }, new sbyte[] { 1, 1, -1 } },
                new sbyte[][] { new sbyte[] { 0, 2, 1 }, new sbyte[] { -1, -1, -1 } },
                new sbyte[][] { new sbyte[] { 0, 2, 1 }, new sbyte[] { -1, 1, 1 } },
                new sbyte[][] { new sbyte[] { 0, 2, 1 }, new sbyte[] { 1, -1, 1 } },
                new sbyte[][] { new sbyte[] { 1, 0, 2 }, new sbyte[] { 1, -1, 1 } },
                new sbyte[][] { new sbyte[] { 1, 0, 2 }, new sbyte[] { -1, 1, 1 } },
                new sbyte[][] { new sbyte[] { 1, 0, 2 }, new sbyte[] { -1, -1, -1 } },
                new sbyte[][] { new sbyte[] { 1, 0, 2 }, new sbyte[] { 1, 1, -1 } },
                new sbyte[][] { new sbyte[] { 1, 2, 0 }, new sbyte[] { 1, 1, 1 } },
                new sbyte[][] { new sbyte[] { 1, 2, 0 }, new sbyte[] { -1, -1, 1 } },
                new sbyte[][] { new sbyte[] { 1, 2, 0 }, new sbyte[] { -1, 1, -1 } },
                new sbyte[][] { new sbyte[] { 1, 2, 0 }, new sbyte[] { 1, -1, -1 } },
                new sbyte[][] { new sbyte[] { 2, 0, 1 }, new sbyte[] { 1, 1, 1 } },
                new sbyte[][] { new sbyte[] { 2, 0, 1 }, new sbyte[] { -1, -1, 1 } },
                new sbyte[][] { new sbyte[] { 2, 0, 1 }, new sbyte[] { -1, 1, -1 } },
                new sbyte[][] { new sbyte[] { 2, 0, 1 }, new sbyte[] { 1, -1, -1 } },
                new sbyte[][] { new sbyte[] { 2, 1, 0 }, new sbyte[] { 1, 1, -1 } },
                new sbyte[][] { new sbyte[] { 2, 1, 0 }, new sbyte[] { -1, -1, -1 } },
                new sbyte[][] { new sbyte[] { 2, 1, 0 }, new sbyte[] { -1, 1, 1 } },
                new sbyte[][] { new sbyte[] { 2, 1, 0 }, new sbyte[] { 1, -1, 1 } }
            };

            foreach (sbyte[][] rotation in rotations) {
                Dictionary<Point, List<Beacon>> differences = new();
                for (int i = 0; i < scanner1.Beacons.Count; i++) {
                    Beacon beacon1 = scanner1.Beacons[i];
                    for (int j = 0; j < scanner2.Beacons.Count; j++) {
                        Beacon beacon2 = scanner2.Beacons[j];
                        Point translation = Difference(beacon1.Position, beacon2.Position, rotation);
                        if (differences.ContainsKey(translation)) {
                            differences[translation].Add(beacon2);
                        }
                        else {
                            differences[translation] = new() { beacon2 };
                        }
                    }
                }

                int max = differences.Max(x => x.Value.Count);
                if (max >= 12) {
                    Point translation = differences.Where(x => x.Value.Count == max).First().Key;
                    scanner2.Position = translation;
                    scanner2.Transform(translation, rotation);
                    return;
                }
            }
        }

        private Point Difference(Point p1, Point p2, sbyte[][] rotation) {
            int[] p2Coords = new int[] { p2.X, p2.Y, p2.Z };
            return new Point(p1.X - rotation[1][0] * p2Coords[rotation[0][0]], p1.Y - rotation[1][1] * p2Coords[rotation[0][1]], p1.Z - rotation[1][2] * p2Coords[rotation[0][2]]);
        }
    }
}
