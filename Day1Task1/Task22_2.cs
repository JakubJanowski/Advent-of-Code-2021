using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task22_2: ITask {
        public class Point: IEquatable<Point> {
            public Point(int x, int y, int z) {
                X = x;
                Y = y;
                Z = z;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }

            public override int GetHashCode() {
                return 91 * (X + (Y << 10) + (Z << 20));
            }

            public override bool Equals(object? obj) {
                if (obj is null) return false;
                if (obj == this) return true;
                if (obj.GetType() != typeof(Point)) return false;
                return Equals((Point)obj);
            }

            public bool Equals(Point? p) {
                if (p is null) return false;
                return X == p.X && Y == p.Y && Z == p.Z;
            }

            public override string ToString() {
                return $"{X},{Y},{Z}";
            }
        }

        public class Cuboid {
            public Cuboid(Point lowCorner, Point highCorner) {
                Corners = new Point[] { lowCorner, highCorner };
            }

            public Point[] Corners { get; set; }

            public long Volume {
                get {
                    return (Corners[1].X - Corners[0].X + 1L) * (Corners[1].Y - Corners[0].Y + 1L) * (Corners[1].Z - Corners[0].Z + 1L);
                }
            }

            public bool Overlaps(Cuboid other) {
                return Corners[0].X <= other.Corners[1].X && Corners[1].X >= other.Corners[0].X
                    && Corners[0].Y <= other.Corners[1].Y && Corners[1].Y >= other.Corners[0].Y
                    && Corners[0].Z <= other.Corners[1].Z && Corners[1].Z >= other.Corners[0].Z;
            }
        }

        public string Solve(string[] data) {
            List<Cuboid> cuboids = new();

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

                Cuboid newCuboid = new(new(xLow, yLow, zLow), new(xHigh, yHigh, zHigh));
                List<Cuboid> partitions = new() { newCuboid };

                if (state) {
                    foreach (Cuboid cuboid in cuboids) {
                        List<Cuboid> newPartitions = new();
                        foreach (Cuboid partition in partitions) {
                            newPartitions.AddRange(Partition(cuboid, partition));
                        }
                        partitions = newPartitions;
                    }
                    cuboids.AddRange(partitions);
                }
                else {
                    List<Cuboid> newPartitions = new();
                    foreach (Cuboid cuboid in cuboids) {
                        newPartitions.AddRange(Partition(newCuboid, cuboid));
                    }
                    cuboids = newPartitions;
                }
            }

            return cuboids.Sum(c => c.Volume).ToString();
        }

        private List<Cuboid> Partition(Cuboid a, Cuboid b) {
            List<Cuboid> partitions = new();

            if (!a.Overlaps(b)) {
                partitions.Add(b);
                return partitions;
            }

            int xLow = b.Corners[0].X;
            int xHigh = b.Corners[1].X;
            int yLow = b.Corners[0].Y;
            int yHigh = b.Corners[1].Y;

            if (xLow < a.Corners[0].X) {
                partitions.Add(new(new(xLow, yLow, b.Corners[0].Z), new(a.Corners[0].X - 1, yHigh, b.Corners[1].Z))); // low X cuboid
                xLow = a.Corners[0].X;
            }
            if (xHigh > a.Corners[1].X) {
                partitions.Add(new(new(a.Corners[1].X + 1, yLow, b.Corners[0].Z), new(xHigh, yHigh, b.Corners[1].Z))); // high X cuboid
                xHigh = a.Corners[1].X;
            }
            if (yLow < a.Corners[0].Y) {
                partitions.Add(new(new(xLow, yLow, b.Corners[0].Z), new(xHigh, a.Corners[0].Y - 1, b.Corners[1].Z))); // low Y cuboid
                yLow = a.Corners[0].Y;
            }
            if (yHigh > a.Corners[1].Y) {
                partitions.Add(new(new(xLow, a.Corners[1].Y + 1, b.Corners[0].Z), new(xHigh, yHigh, b.Corners[1].Z))); // high Y cuboid
                yHigh = a.Corners[1].Y;
            }
            if (b.Corners[0].Z < a.Corners[0].Z) {
                partitions.Add(new(new(xLow, yLow, b.Corners[0].Z), new(xHigh, yHigh, a.Corners[0].Z - 1))); // low Z cuboid
            }
            if (b.Corners[1].Z > a.Corners[1].Z) {
                partitions.Add(new(new(xLow, yLow, a.Corners[1].Z + 1), new(xHigh, yHigh, b.Corners[1].Z))); // high Z cuboid
            }

            return partitions;
        }
    }
}
