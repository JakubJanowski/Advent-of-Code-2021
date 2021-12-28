using System;
using System.Linq;

namespace Tasks {
    internal class Task16: ITask {
        private int VersionSum = 0;
        public string Solve(string[] data) {
            string message = string.Join(string.Empty, data[0].Select(d => Convert.ToString(Convert.ToInt32(d.ToString(), 16), 2).PadLeft(4, '0')));

            ParsePacket(message);

            return VersionSum.ToString();
        }

        private int ParsePacket(string message, int index = 0) {
            VersionSum += Convert.ToInt32(message.Substring(index, 3), 2);
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

                    break;
                default:
                    switch(message[index++]) {
                        case '0':
                            int length = Convert.ToInt32(message.Substring(index, 15), 2);
                            index += 15;
                            int endIndex = index + length;
                            while (index < endIndex) {
                                index = ParsePacket(message, index);
                            }
                            break;
                        case '1':
                            int count = Convert.ToInt32(message.Substring(index, 11), 2);
                            index += 11;
                            for (int i = 0; i < count; i++) {
                                index = ParsePacket(message, index);
                            }
                            break;
                    }
                    break;
            }
            return index;
        }
    }
}
