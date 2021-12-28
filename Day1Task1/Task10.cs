using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task10: ITask {
        public string Solve(string[] data) {
            Dictionary<char, int> errorPoints = new Dictionary<char, int>() {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 }
            };

            int score = 0;
            foreach (string line in data) {
                int position = CheckLine(line);
                if(position >= 0) {
                    score += errorPoints[line[position]];
                }
            }

            return score.ToString();
        }

        private int CheckLine(string line) {
            char[] openingBrackets = new char[4] { '(', '[', '{', '<' };
            Dictionary<char, char> matchingBrackets = new Dictionary<char, char>() {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' }
            };
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < line.Length; i++) {
                char c = line[i];
                if (openingBrackets.Contains(c)) {
                    stack.Push(c);
                } else if (c != matchingBrackets[stack.Pop()]) {
                    return i;
                }
            }

            return -1;
        }
    }
}
