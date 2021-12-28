using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task21_2: ITask {
        public class Game: IEquatable<Game> {
            public Game(byte position1, byte position2, short score1 = 0, short score2 = 0, bool move = false) {
                Move = move;
                Positions = new byte[2] { position1, position2 };
                Scores = new short[2] { score1, score2 };
            }
            public bool Move { get; set; }
            public byte[] Positions { get; set; }
            public short[] Scores { get; set; }

            public override int GetHashCode() {
                return Positions[0] + (Positions[1] << 4) + (Scores[0] << 8) + (Scores[1] << 18) + (Move ? 1 << 28 : 0);
            }

            public override bool Equals(object? obj) {
                if (obj is null) return false;
                if (obj == this) return true;
                if (obj.GetType() != typeof(Game)) return false;
                return Equals((Game)obj);
            }

            public bool Equals(Game? p) {
                if (p is null) return false;
                return GetHashCode() == p.GetHashCode();
            }

            public Game Clone() {
                return new(Positions[0], Positions[1], Scores[0], Scores[1], Move);
            }
        }

        public string Solve(string[] data) {
            Dictionary<Game, long> games = new();
            games.Add(new((byte)(data[0][^1] - '1'), (byte)(data[1][^1] - '1')), 1);
            long[] nTimesWon = new long[2];

            while (games.Count > 0) {
                Game game = games.Keys.MinBy(g => (g.Scores[0] << 16) + g.Scores[1])!;
                long gameCount = games[game];
                games.Remove(game);
                int player = game.Move ? 1 : 0;

                nTimesWon[player] += Roll3DiracDice(games, game, gameCount);
            }

            return nTimesWon.Max().ToString();
        }

        private long Roll3DiracDice(Dictionary<Game, long> games, Game game, long gameCount) {
            byte[][] splits = new byte[][] {
                new byte[] {3, 1},
                new byte[] {4, 3},
                new byte[] {5, 6},
                new byte[] {6, 7},
                new byte[] {7, 6},
                new byte[] {8, 3},
                new byte[] {9, 1},
            };
            long winCount = 0;

            for (int i = 0; i < splits.Length; i++) {
                winCount += HandleSplitUniverseDuplicateBranches(games, game, gameCount, splits[i][0], splits[i][1]);
            }

            return winCount;
        }

        private long HandleSplitUniverseDuplicateBranches(Dictionary<Game, long> games, Game game, long gameCount, byte diracDiceSum, byte duplicateCount) {
            int player = game.Move ? 1 : 0;
            Game newGame = game.Clone();
            newGame.Positions[player] = (byte)((newGame.Positions[player] + diracDiceSum) % 10);
            newGame.Scores[player] += (short)(newGame.Positions[player] + 1);
            if (newGame.Scores[player] >= 21) {
                return gameCount * duplicateCount;
            }
            else {
                newGame.Move = !newGame.Move;
                if (games.ContainsKey(newGame)) {
                    games[newGame] += gameCount * duplicateCount;
                }
                else {
                    games.Add(newGame, gameCount * duplicateCount);
                }
            }

            return 0;
        }
    }
}
