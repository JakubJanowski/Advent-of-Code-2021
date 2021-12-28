using System.Linq;

namespace Tasks {
    internal class Task25: ITask {
        public enum Cucumber {
            None, East, South
        }
        public string Solve(string[] data) {
            Cucumber[][] cucumbers = data.Select(r => r.Select(c => {
                switch (c) {
                    case '>': return Cucumber.East;
                    case 'v': return Cucumber.South;
                    default: return Cucumber.None;
                }
            }).ToArray()).ToArray();

            Cucumber[][] cucumbersTmp = new Cucumber[cucumbers.Length][];
            for (int i = 0; i < cucumbersTmp.Length; i++) {
                cucumbersTmp[i] = new Cucumber[cucumbers[i].Length];
            }

            bool haveCucumbersMoved = true;
            int nSteps;
            for (nSteps = 0; haveCucumbersMoved; nSteps++) {
                haveCucumbersMoved = Step(cucumbers, cucumbersTmp);
            }

            return nSteps.ToString();
        }

        private bool Step(Cucumber[][] cucumbers, Cucumber[][] cucumbersTmp) {
            bool hasAnyMoved = false;
            for (int y = 0; y < cucumbers.Length; y++) {
                for (int x = 0; x < cucumbers[y].Length; x++) {
                    cucumbersTmp[y][x] = cucumbers[y][x];
                }
            }

            for (int y = 0; y < cucumbers.Length; y++) {
                for (int x = 0; x < cucumbers[y].Length; x++) {
                    int newX = (x + 1) % cucumbers[y].Length;
                    if (cucumbers[y][x] == Cucumber.East && cucumbers[y][newX] == Cucumber.None) {
                        cucumbersTmp[y][x] = Cucumber.None;
                        cucumbersTmp[y][newX] = Cucumber.East;
                        hasAnyMoved = true;
                    }
                }
            }

            for (int y = 0; y < cucumbersTmp.Length; y++) {
                for (int x = 0; x < cucumbersTmp[y].Length; x++) {
                    cucumbers[y][x] = cucumbersTmp[y][x];
                }
            }

            for (int y = 0; y < cucumbersTmp.Length; y++) {
                int newY = (y + 1) % cucumbers.Length;
                for (int x = 0; x < cucumbersTmp[y].Length; x++) {
                    if (cucumbersTmp[y][x] == Cucumber.South && cucumbersTmp[newY][x] == Cucumber.None) {
                        cucumbers[y][x] = Cucumber.None;
                        cucumbers[newY][x] = Cucumber.South;
                        hasAnyMoved = true;
                    }
                }
            }

            return hasAnyMoved;
        }
    }
}
