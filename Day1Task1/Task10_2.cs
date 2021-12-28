using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task10_2: ITask {
        public string Solve(string[] data) {

            List<long> scores = new();
            foreach (string line in data) {
                long score = Autocomplete(line);
                if (score >= 0) {
                    scores.Add(score);
                }
            }

            return scores.OrderBy(s=>s).ToArray()[(scores.Count)/2].ToString();
        }

        private long Autocomplete(string line) {
            char[] openingBrackets = new char[4] { '(', '[', '{', '<' };
            Dictionary<char, char> matchingBrackets = new Dictionary<char, char>() {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' }
            };
            Dictionary<char, int> autocompletePoints = new Dictionary<char, int>() {
                { '(', 1 },
                { '[', 2 },
                { '{', 3 },
                { '<', 4 }
            };
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < line.Length; i++) {
                char c = line[i];
                if (openingBrackets.Contains(c)) {
                    stack.Push(c);
                }
                else if (c != matchingBrackets[stack.Pop()]) {
                    return -1;
                }
            }

            long autocompleteScore = 0;
            foreach(char bracket in stack) {
                autocompleteScore *= 5;
                autocompleteScore += autocompletePoints[bracket];
            }
            return autocompleteScore;
        }
    }
}
