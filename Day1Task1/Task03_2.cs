using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task03_2: ITask {
        public string Solve(string[] data) {
            List<string> oxygenData = new List<string>(data);
            List<string> co2Data = new List<string>(data);

            for (int i = 0; i < data[0].Length; i++) {
                int zeros = 0;
                int ones = 0;
                foreach (string d in oxygenData) {
                    if (d[i] == '1') {
                        ones++;
                    }
                    else {
                        zeros++;
                    }
                }
                if (ones >= zeros) {
                    oxygenData = oxygenData.Where(d => d[i] == '1').ToList();
                } else {
                    oxygenData = oxygenData.Where(d => d[i] == '0').ToList();
                }
                if(oxygenData.Count == 1) {
                    break;
                }
            }

            for (int i = 0; i < data[0].Length; i++) {
                int zeros = 0;
                int ones = 0;
                foreach (string d in co2Data) {
                    if (d[i] == '1') {
                        ones++;
                    }
                    else {
                        zeros++;
                    }
                }
                if (ones >= zeros) {
                    co2Data = co2Data.Where(d => d[i] == '0').ToList();
                }
                else {
                    co2Data = co2Data.Where(d => d[i] == '1').ToList();
                }
                if (co2Data.Count == 1) {
                    break;
                }
            }

            int oxygen = Convert.ToInt32(oxygenData[0], 2);
            int co2 = Convert.ToInt32(co2Data[0], 2);
            return (oxygen * co2).ToString();
        }


    }
}
