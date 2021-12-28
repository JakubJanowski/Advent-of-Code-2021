using System;
using System.IO;

namespace Tasks {
    class Program {
        static void Main(string[] args) {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Console.WriteLine(new Task25().Solve(File.ReadAllLines("input.txt")));
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            Console.ReadKey();
        }
    }

}