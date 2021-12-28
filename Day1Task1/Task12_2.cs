using System.Collections.Generic;

namespace Tasks {
    internal class Task12_2: ITask {
        List<string> paths = new();

        public string Solve(string[] data) {
            Dictionary<string, List<string>> graph = new();
            foreach (string entry in data) {
                string[] caves = entry.Split('-');

                if (!graph.ContainsKey(caves[0])) {
                    graph[caves[0]] = new();
                }

                if (!graph.ContainsKey(caves[1])) {
                    graph[caves[1]] = new();
                }

                if (caves[0] != "end" && caves[1] != "start") {
                    graph[caves[0]].Add(caves[1]);
                }
                if (caves[1] != "end" && caves[0] != "start") {
                    graph[caves[1]].Add(caves[0]);
                }
            }

            FindPaths(graph, "start", "-");

            return paths.Count.ToString();
        }

        private void FindPaths(Dictionary<string, List<string>> graph, string currentCave, string path, bool hasDoubleSmallCave = false) {
            path += currentCave + "-";
            if (currentCave == "end") {
                if (!paths.Contains(path)) {
                    paths.Add(path);
                }
                return;
            }

            foreach (string cave in graph[currentCave]) {
                if (cave.ToLower() == cave && path.Contains("-" + cave + "-")) {
                    if (hasDoubleSmallCave) {
                        continue;
                    }
                    FindPaths(graph, cave, path, true);
                }
                else {
                    FindPaths(graph, cave, path, hasDoubleSmallCave);
                }
            }
        }
    }
}
