using System.Linq;

namespace Tasks {
    internal class Task08: ITask {
        public string Solve(string[] data) {
            string[][] digits = data.Select(d => {
                return d.Split('|')[1].Split(' ').Where(d => !string.IsNullOrWhiteSpace(d)).ToArray();
            }).ToArray();

            return digits.Sum(display => display.Count(d => d.Length <= 4 || d.Length == 7)).ToString();
        }
    }
}
