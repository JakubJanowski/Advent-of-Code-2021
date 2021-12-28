using System.Linq;

namespace Tasks {
    internal class Task01: ITask {
        public string Solve(string[] data) {
            int[] values = data.Select(d => int.Parse(d)).ToArray();
            int count = 0;
            for (int i = 1; i < values.Length; i++) {
                if (values[i] > values[i - 1])
                    count++;
            }
            return count.ToString();
        }
    }
}
