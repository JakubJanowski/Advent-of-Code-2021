namespace Tasks {
    internal class Task02: ITask {
        public string Solve(string[] data) {
            int forward = 0;
            int down = 0;
            foreach (var d in data) {
                string[] instruction = d.Split(' ');
                switch (instruction[0]) {
                    case "forward":
                        forward += int.Parse(instruction[1]);
                        break;
                    case "up":
                        down -= int.Parse(instruction[1]);
                        break;
                    case "down":
                        down += int.Parse(instruction[1]);
                        break;
                }
            }
            return (forward * down).ToString();
        }
    }
}
