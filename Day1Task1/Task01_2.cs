using System.Linq;

namespace Tasks {
    internal class Task01_2: ITask {
        public string Solve(string[] data) {
            int[] values = data.Select(d => int.Parse(d)).ToArray();
            int count = 0;
            for (int i = 3; i < values.Length; i++) {
                if (values[i] + values[i - 1] + values[i - 2] > values[i - 1] + values[i - 2] + values[i - 3])
                    count++;
            }
            return count.ToString();
        }
    }
}
