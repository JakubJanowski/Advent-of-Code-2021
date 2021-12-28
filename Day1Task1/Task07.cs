using System;
using System.Linq;

namespace Tasks {
    internal class Task07: ITask {
        public string Solve(string[] data) {
            int[] numbers = data[0].Split(',').Where(d => !string.IsNullOrWhiteSpace(d)).Select(d => int.Parse(d)).OrderBy(d => d).ToArray();
            int median;
            if (numbers.Length % 2 == 0) {
                median = numbers[numbers.Length / 2];
            } else {
                median = numbers[(numbers.Length + 1) / 2] + numbers[(numbers.Length - 1) / 2];
            }

            return numbers.Select(n => Math.Abs(n - median)).Sum().ToString(); ;
        }
    }
}
