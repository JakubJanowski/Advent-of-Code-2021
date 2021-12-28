using System;
using System.Linq;

namespace Tasks {
    internal class Task20: ITask {
        public string Solve(string[] data) {
            const int margin = 2;
            const int nEnhancements = 2;
            bool[] algorithm = data[0].Select(d => d == '#').ToArray();
            int originalWidth = data[2].Length;
            int originalHeight = data.Length - 2;
            bool[][] image = new bool[originalHeight + 2 * margin][];

            for (int i = 0; i < margin; i++) {
                image[i] = new bool[originalWidth + 2 * margin];
            }
            for (int i = margin; i < originalHeight + margin; i++) {
                image[i] = new bool[margin].Concat(data[i - margin + 2].Select(d => d == '#')).Concat(new bool[margin]).ToArray();
            }
            for (int i = originalHeight + margin; i < image.Length; i++) {
                image[i] = new bool[originalWidth + 2 * margin];
            }

            Print(image);
            for (int i = 0; i < nEnhancements; i++) {
                image = Enhance(image, algorithm);
                Print(image);
            }

            return image.Sum(i => i.Count(b => b)).ToString();
        }

        private static bool[][] Enhance(bool[][] image, bool[] algorithm) {
            bool[][] newImage = new bool[image.Length + 2][];
            bool fillInValue = image[0][0] ? algorithm[^1] : algorithm[0];
            newImage[0] = Enumerable.Repeat(fillInValue, image[0].Length + 2).ToArray();
            newImage[1] = Enumerable.Repeat(fillInValue, image[0].Length + 2).ToArray();
            newImage[^2] = Enumerable.Repeat(fillInValue, image[0].Length + 2).ToArray();
            newImage[^1] = Enumerable.Repeat(fillInValue, image[0].Length + 2).ToArray();
            for (int y = 1; y < image.Length - 1; y++) {
                newImage[y+1] = new bool[image[y].Length + 2];
                newImage[y + 1][0] = fillInValue;
                newImage[y + 1][1] = fillInValue;
                newImage[y + 1][^2] = fillInValue;
                newImage[y + 1][^1] = fillInValue;
                for (int x = 1; x < image[y].Length - 1; x++) {
                    int index = 0;
                    if (image[y - 1][x - 1]) index |= 1 << 8;
                    if (image[y - 1][x]) index |= 1 << 7;
                    if (image[y - 1][x + 1]) index |= 1 << 6;
                    if (image[y][x - 1]) index |= 1 << 5;
                    if (image[y][x]) index |= 1 << 4;
                    if (image[y][x + 1]) index |= 1 << 3;
                    if (image[y + 1][x - 1]) index |= 1 << 2;
                    if (image[y + 1][x]) index |= 1 << 1;
                    if (image[y + 1][x + 1]) index |= 1;

                    newImage[y + 1][x + 1] = algorithm[index];
                }
            }

            return newImage;
        }

        private static void Print(bool[][] image) {
            for (int y = 0; y < image.Length; y++) {
                for (int x = 0; x < image[y].Length; x++) {
                    Console.Write(image[y][x] ? '#' : '.');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
