using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks {
    internal class Task16_2: ITask {
        public string Solve(string[] data) {
            string message = string.Join(string.Empty, data[0].Select(d => Convert.ToString(Convert.ToInt32(d.ToString(), 16), 2).PadLeft(4, '0')));

            int index = 0;
            return ParsePacket(message, ref index).ToString();
        }

        private long ParsePacket(string message, ref int index) {
            long value = 0;
            index += 3;
            int typeId = Convert.ToInt32(message.Substring(index, 3), 2);
            index += 3;

            switch (typeId) {
                case 4:
                    string number = "";
                    char prefix;
                    do {
                        string group = message.Substring(index, 5);
                        prefix = group[0];
                        number += group[1..];
                        index += 5;
                    } while (prefix == '1');
                    value = Convert.ToInt64(number, 2);
                    break;
                default:
                    List<long> subpacketValues = new();
                    switch (message[index++]) {
                        case '0':
                            int length = Convert.ToInt32(message.Substring(index, 15), 2);
                            index += 15;
                            int endIndex = index + length;
                            while (index < endIndex) {
                                subpacketValues.Add(ParsePacket(message, ref index));
                            }
                            break;
                        case '1':
                            int count = Convert.ToInt32(message.Substring(index, 11), 2);
                            index += 11;
                            for (int i = 0; i < count; i++) {
                                subpacketValues.Add(ParsePacket(message, ref index));
                            }
                            break;
                    }

                    switch(typeId) {
                        case 0:
                            value = subpacketValues.Sum();
                            break;
                        case 1:
                            value = subpacketValues.Aggregate(1L, (x, y) => x * y);
                            break;
                        case 2:
                            value = subpacketValues.Min();
                            break;
                        case 3:
                            value = subpacketValues.Max();
                            break;
                        case 5:
                            value = subpacketValues[0] > subpacketValues[1] ? 1 : 0;
                            break;
                        case 6:
                            value = subpacketValues[0] < subpacketValues[1] ? 1 : 0;
                            break;
                        case 7:
                            value = subpacketValues[0] == subpacketValues[1] ? 1 : 0;
                            break;
                    }

                    break;
            }

            return value;
        }
    }
}
