namespace Tasks {
    internal class Task21: ITask {
        private byte dice = 1;

        public string Solve(string[] data) {
            byte[] position = new byte[] { (byte)(data[0][^1] - '1'), (byte)(data[1][^1] - '1') };
            short[] score = new short[2];

            for (int player = 0, nTurns = 1; ; player = 1 - player, nTurns++) {
                position[player] = (byte)((position[player] + Roll3DeterministicDice()) % 10);
                score[player] += (short)(position[player] + 1);
                if (score[player] >= 1000) {
                    return (3 * nTurns * score[1 - player]).ToString();
                }
            }
        }

        private byte Roll3DeterministicDice() {
            byte result = (byte)(3 * (dice + 1) % 10);
            dice = (byte)((dice + 3) % 10);
            return result;
        }
    }
}
