using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task08_2: ITask {
        class Entry {
            public string[] Digits;
            public string[] Output;
        }

        public string Solve(string[] data) {
            Entry[] entries = data.Select(d => {
                string[] split = d.Split('|');
                return new Entry() {
                    Digits = split[0].Split(' ').Where(d => !string.IsNullOrWhiteSpace(d)).ToArray(),
                    Output = split[1].Split(' ').Where(d => !string.IsNullOrWhiteSpace(d)).ToArray()
                };
            }).ToArray();



            return entries.Sum(entry => Decode(entry.Digits, entry.Output)).ToString();
        }

        private int Decode(string[] digits, string[] output) {
            string[] assignments = GetAssignments(digits);

            for (int i = 0; i < assignments.Length; i++) {
                assignments[i] = new string(assignments[i].OrderBy(a => a).ToArray());
            }

            int outputNumber = 0;

            for (int i = 0; i < output.Length; i++) {
                output[i] = new string(output[i].OrderBy(o => o).ToArray()); 
                for (int j = 0; j < assignments.Length; j++) {
                    if (assignments[j] == output[i]) {
                        outputNumber += j * (int)Math.Round(Math.Pow(10, 3-i));
                    }
                }
            }

            return outputNumber;
        }

        private string[] GetAssignments(string[] digits) {
            List<string>[] assignments = new List<string>[digits.Length];
            int[] digitSegments = { 6, 2, 5, 5, 4, 5, 6, 3, 7, 6 };

            for (int i = 0; i < digits.Length; i++) {
                assignments[i] = new List<string>(digits.Where(d => d.Length == digitSegments[i]));
            }

            //1, 4, 7, 8
            assignments[9] = assignments[9].Where(a => ContainsAllLetters(a, assignments[4].First())).ToList();
            assignments[6] = assignments[6].Where(a => !assignments[9].Contains(a) && !ContainsAllLetters(a, assignments[1].First())).ToList();
            assignments[0] = assignments[0].Where(a => !assignments[6].Contains(a) && !assignments[9].Contains(a)).ToList();
            assignments[3] = assignments[3].Where(a => ContainsAllLetters(a, assignments[1].First())).ToList();
            assignments[2] = assignments[2].Where(a => !assignments[3].Contains(a) && CountCommonLetters(a, assignments[4].First()) == 2).ToList();
            assignments[5] = assignments[5].Where(a => !assignments[2].Contains(a) && !assignments[3].Contains(a)).ToList();


            return assignments.Select(a => a.First()).ToArray();
        }

        private bool ContainsAllLetters(string text, string letters) {
            foreach (char c in letters) {
                if (!text.Contains(c)) {
                    return false;
                }
            }
            return true;
        }

        private int CountCommonLetters(string text, string letters) {
            int count = 0;
            foreach (char c in letters) {
                if (text.Contains(c)) {
                    count++;
                }
            }
            return count;
        }
    }
}
