using System;
using System.Linq;

namespace Tasks {
    internal class Task03: ITask {
        public string Solve(string[] data) {
            int[] bits = new int[data[0].Length];
            foreach (string d in data) {
                for (int i = 0; i < d.Length; i++) {
                    if (d[i] == '1') {
                        bits[i]++;
                    }
                }
            }
            string gammaStr = "";
            string epsilonStr = "";

            for (int i = 0; i < bits.Length; i++) {
                if (bits[i] > data.Length/2) {
                    gammaStr += "1";
                    epsilonStr += "0";
                } else {
                    gammaStr += "0";
                    epsilonStr += "1";
                }
            }
            int gamma = Convert.ToInt32(gammaStr, 2);
            int epsilon = Convert.ToInt32(epsilonStr, 2);
            return (gamma * epsilon).ToString();
        }
    }
}
