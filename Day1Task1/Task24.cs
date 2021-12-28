using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task24: ITask {
        public enum InstructionType {
            Inp,
            Add,
            Mul,
            Div,
            Mod,
            Eql
        }

        public class Instruction {
            public InstructionType Type { get; set; }
            public int A { get; set; }
            public int B { get; set; }
            public bool IsNumber { get; set; }
        }

        public string Solve(string[] data) {
            //byte[] input = new byte[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 };
            //byte[] input = new byte[] { 9, 9, 9, 8, 1, 9, 9, 9, 9, 9, 9, 9, 9, 9 };
            byte[] input = new byte[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 4, 9, 9, 5, 9 }; //program(99999898949959) = 645
            List<Instruction> program = new();

            foreach (string line in data) {
                Instruction instruction = new();
                instruction.A = line[4] - 'w';
                if (line.Length > 5) {
                    int b;
                    if (!(instruction.IsNumber = int.TryParse(line[6..^0], out b))) {
                        b = line[6] - 'w';
                    }
                    instruction.B = b;
                }
                switch (line[0..3]) {
                    case "inp":
                        instruction.Type = InstructionType.Inp;
                        break;
                    case "add":
                        instruction.Type = InstructionType.Add;
                        break;
                    case "mul":
                        instruction.Type = InstructionType.Mul;
                        break;
                    case "div":
                        instruction.Type = InstructionType.Div;
                        break;
                    case "mod":
                        instruction.Type = InstructionType.Mod;
                        break;
                    case "eql":
                        instruction.Type = InstructionType.Eql;
                        break;
                }
                program.Add(instruction);
            }

            long[][] registers = new long[15][];
            for (int i = 0; i < registers.Length; i++) {
                registers[i] = new long[4];
            }

            bool result = true;
            int inputIndex = 0;
            int[] skipTable = new int[registers.Length];

            skipTable[0] = 0;
            skipTable[1] = program.FindIndex(p => p.Type == InstructionType.Inp);
            for (int i = 2; i < skipTable.Length; i++) {
                skipTable[i] = program.FindIndex(skipTable[i - 1] + 1, p => p.Type == InstructionType.Inp);
            }

            long minResult = long.MaxValue;
            long resultValue;
            while (true) {
                if (inputIndex < 1) {
                    Console.WriteLine(new string(input.Select(i => (char)(i + '0')).ToArray()));
                }

                resultValue = RunProgram(program, input, registers, inputIndex, skipTable);
                result = resultValue == 0;
                if (Math.Abs(resultValue) < Math.Abs(minResult)) {
                    minResult = resultValue;
                    Console.WriteLine("program(" + new string(input.Select(i => (char)(i + '0')).ToArray()) + ") = " + minResult);
                }
                if (result) {
                    return new string(input.Select(i => (char)(i + '0')).ToArray());
                }
                inputIndex = Decrease(input, 5);
            }
        }

        private int Decrease(byte[] input, int skip = 0) {
            for (int i = 1 + skip; i <= input.Length; i++) {
                input[^i]--;
                if (input[^i] == 0) {
                    input[^i] = 9;
                }
                else {
                    return input.Length - i;
                }
            }
            return 0;
        }

        private static long RunProgram(List<Instruction> program, byte[] input, long[][] registers, int inputIndex, int[] skipTable) {
            foreach (Instruction instruction in program.Skip(skipTable[inputIndex + 1])) {
                switch (instruction.Type) {
                    case InstructionType.Inp:
                        for (int i = 0; i < registers[inputIndex].Length; i++) {
                            registers[inputIndex + 1][i] = registers[inputIndex][i];
                        }
                        registers[inputIndex + 1][instruction.A] = input[inputIndex];
                        inputIndex++;
                        break;
                    case InstructionType.Add:
                        registers[inputIndex][instruction.A] += instruction.IsNumber ? instruction.B : registers[inputIndex][instruction.B];
                        break;
                    case InstructionType.Mul:
                        registers[inputIndex][instruction.A] *= instruction.IsNumber ? instruction.B : registers[inputIndex][instruction.B];
                        break;
                    case InstructionType.Div:
                        registers[inputIndex][instruction.A] /= instruction.IsNumber ? instruction.B : registers[inputIndex][instruction.B];
                        break;
                    case InstructionType.Mod:
                        registers[inputIndex][instruction.A] %= instruction.IsNumber ? instruction.B : registers[inputIndex][instruction.B];
                        break;
                    case InstructionType.Eql:
                        registers[inputIndex][instruction.A] = (instruction.IsNumber ? registers[inputIndex][instruction.A] == instruction.B : registers[inputIndex][instruction.A] == registers[inputIndex][instruction.B]) ? 1 : 0;
                        break;
                }
            }
            return registers[^1][^1];
        }
    }
}
