namespace Tasks {
    internal class Task02_2: ITask {
        public string Solve(string[] data) {
            int hPosition = 0;
            int depth = 0;
            int aim = 0;
            foreach (var d in data) {
                string[] instruction = d.Split(' ');
                switch (instruction[0]) {
                    case "forward":
                        hPosition += int.Parse(instruction[1]);
                        depth += int.Parse(instruction[1]) * aim;
                        break;
                    case "up":
                        aim -= int.Parse(instruction[1]);
                        break;
                    case "down":
                        aim += int.Parse(instruction[1]);
                        break;
                }
            }
            return (hPosition * depth).ToString();
        }
    }
}
