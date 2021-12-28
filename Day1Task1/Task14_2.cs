using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task14_2: ITask {
        public string Solve(string[] data) {
            const int steps = 40;
            string polymer = data[0];
            Dictionary<string, char> rules = new();
            Dictionary<string, long> pairs = new();

            for (int i = 2; i < data.Length; i++) {
                string[] split = data[i].Split(" -> ");
                rules[split[0]] = split[1][0];
            }

            for (int i = 1; i < polymer.Length; i++) {
                string key = "" + polymer[i - 1] + polymer[i];
                if (pairs.ContainsKey(key)) {
                    pairs[key]++;
                }
                else {
                    pairs[key] = 1;
                }
            }

            for (int i = 0; i < steps; i++) {
                Step(ref pairs, rules);
            }

            Dictionary<char, long> charCount = new();

            foreach (KeyValuePair<string, long> pair in pairs) {
                if (charCount.ContainsKey(pair.Key[0])) {
                    charCount[pair.Key[0]] += pair.Value;
                }
                else {
                    charCount[pair.Key[0]] = pair.Value;
                }
            }
            if (charCount.ContainsKey(polymer[^1])) {
                charCount[polymer[^1]]++;
            }
            else {
                charCount[polymer[^1]] = 1;
            }

            return (charCount.Max(c => c.Value) - charCount.Min(c => c.Value)).ToString();
        }

        private void Step(ref Dictionary<string, long> pairs, Dictionary<string, char> rules) {
            Dictionary<string, long> newPairs = new();
            foreach (KeyValuePair<string, long> pair in pairs) {
                if (rules.ContainsKey(pair.Key)) {
                    string newPair1 = "" + pair.Key[0] + rules[pair.Key];
                    string newPair2 = "" + rules[pair.Key] + pair.Key[1];
                    if (newPairs.ContainsKey(newPair1)) {
                        newPairs[newPair1] += pair.Value;
                    }
                    else {
                        newPairs[newPair1] = pair.Value;
                    }

                    if (newPairs.ContainsKey(newPair2)) {
                        newPairs[newPair2] += pair.Value;
                    }
                    else {
                        newPairs[newPair2] = pair.Value;
                    }
                }
                else {
                    newPairs[pair.Key] = pair.Value;
                }
            }
            pairs = newPairs;
        }
    }
}
