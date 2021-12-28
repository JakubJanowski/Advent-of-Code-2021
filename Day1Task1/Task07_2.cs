using System;
using System.Linq;

namespace Tasks {
    internal class Task07_2: ITask {
        public string Solve(string[] data) {
            int[] numbers = data[0].Split(',').Where(d => !string.IsNullOrWhiteSpace(d)).Select(d => int.Parse(d)).ToArray();

            long minCost = long.MaxValue;

            for (int i = numbers.Min(); i <= numbers.Max(); i++) {
                long cost = CalculateCost(numbers, i);
                minCost = Math.Min(minCost, cost);
            }


            return minCost.ToString(); ;
        }

        private long CalculateCost(int[] numbers, int target) {
            long totalCost = 0;
            foreach (int number in numbers) {
                totalCost += FuelCost(number, target);
            }
            return totalCost;
        }

        private long FuelCost(int from, int to) {
            int difference = Math.Abs(from - to);
            long sum = 0;
            for (int i = 1; i <= difference; i++) {
                sum += i;
            }
            return sum;
        }
    }
}
