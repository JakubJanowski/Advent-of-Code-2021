using System;
using System.Linq;

namespace Tasks {
    internal class Task06_2: ITask {
        public string Solve(string[] data) {
            const int days = 256;
            int[] timers = data[0].Split(',').Where(d => !string.IsNullOrWhiteSpace(d)).Select(d => int.Parse(d)).ToArray();
            long[] population = new long[9];

            foreach (int timer in timers) {
                population[timer]++;
            }

            for (int i = 0; i < days; i++) {
                Step(population);
            }

            return population.Sum().ToString();
        }

        private void Step(long[] population) {
            long newCount = population[0];
            for (int i = 1; i < 9; i++) {
                population[i - 1] = population[i];
            }
            population[6] += newCount;
            population[8] = newCount;
        }
        private void Print(long[] population) {
            for (int i = 0; i < 9; i++) {
                Console.Write(population[i] + ",");
            }
            Console.WriteLine();
        }


    }
}
