using System.Collections.Generic;

namespace Tasks {
    internal class Task17_2: ITask {
        public string Solve(string[] data) {
            int minX = int.Parse(data[0][(data[0].IndexOf('=') + 1)..(data[0].IndexOf(".."))]);
            int maxX = int.Parse(data[0][(data[0].IndexOf("..") + 2)..(data[0].IndexOf(","))]);
            int minY = int.Parse(data[0][(data[0].LastIndexOf('=') + 1)..(data[0].LastIndexOf(".."))]);
            int maxY = int.Parse(data[0][(data[0].LastIndexOf("..") + 2)..]);

            int count = 0;
            List<int> yValues = new();

            for (int y = minY; y <= maxY; y++) {
                yValues.Add(y);
            }
            //for (int y = -4; y <= 3; y++) {
            //    yValues.Add(y);
            //}
            for (int y = -72; y <= 71; y++) {
                yValues.Add(y);
            }
            for (int y = -maxY - 1; y <= -minY - 1; y++) {
                yValues.Add(y);
            }

            for (int x = 0; x <= maxX; x++) {
                foreach (int y in yValues) {
                    if (CanXHit(x, minX, maxX, GetYSteps(y, maxY), GetYSteps(y, minY - 1) - 1)) {
                        count++;
                        //Console.WriteLine(x + "," + y);
                    }
                }
            }

            return count.ToString();
        }

        private int GetYSteps(int y, int limit) {
            int steps = 0;
            if (y >= 0) {
                steps = y * 2 + 1;
                y = -y - 1;
            }
            for (; 0 > limit; y--, steps++) {
                limit -= y;
            }
            return steps;
        }

        private bool CanXHit(int x, int minX, int maxX, int minSteps, int maxSteps) {
            int sum = 0;
            for (int i = 1; i <= maxSteps && x > 0; i++) {
                sum += x--;
                if (i >= minSteps && sum >= minX && sum <= maxX) {
                    return true;
                }
            }

            return sum >= minX && sum <= maxX;
        }
    }
}
