using System;

namespace Tasks {
    internal class Task17: ITask {
        public string Solve(string[] data) {
            int minY = int.Parse(data[0][(data[0].LastIndexOf('=') + 1)..(data[0].LastIndexOf(".."))]);
            minY = Math.Abs(minY) - 1;
            return ((minY * minY + minY) / 2).ToString();
        }
    }
}
