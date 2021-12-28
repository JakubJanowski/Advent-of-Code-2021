using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks {
    internal class Task18_2: ITask {
        class Element {
            public Element(int value) {
                Value = value;
            }

            public Element(Pair pair) {
                Pair = pair;
            }

            public int? Value { get; set; }
            public Pair? Pair { get; set; }
        }

        class Pair {
            public Pair(Element leftElement, Element rightElement) {
                Children = new Element[2] { leftElement, rightElement };
            }

            public Element[] Children { get; set; }

            public static Pair operator +(Pair left, Pair right) {
                Pair pair = new(new Element(left.Copy()), new Element(right.Copy()));
                pair.Reduce();
                return pair;
            }

            public static Pair Parse(string text) {
                int index = 1;
                return Parse(text, ref index);
            }

            private static Pair Parse(string text, ref int index) {
                Element[] elements = new Element[2];
                int elementIndex = 0;
                while (true) {
                    char c = text[index];
                    index++;
                    switch (c) {
                        case '[':
                            elements[elementIndex] = new Element(Parse(text, ref index));
                            break;
                        case ']':
                            return new Pair(elements[0], elements[1]);
                        case ',':
                            elementIndex++;
                            break;
                        default:
                            elements[elementIndex] = new Element(c - '0');
                            break;
                    }
                }
            }

            public Pair Copy() {
                Element element1;
                Element element2;

                if (Children[0].Value.HasValue) {
                    element1 = new(Children[0].Value!.Value);
                }
                else if (Children[0].Pair is not null) {
                    element1 = new(Children[0].Pair!.Copy());
                }
                else {
                    throw new Exception("Element has no regular number nor a pair.");
                }

                if (Children[1].Value.HasValue) {
                    element2 = new(Children[1].Value!.Value);
                }
                else if (Children[1].Pair is not null) {
                    element2 = new(Children[1].Pair!.Copy());
                }
                else {
                    throw new Exception("Element has no regular number nor a pair.");
                }

                return new Pair(element1, element2); ;
            }

            public int Magnitude() {
                int[] magnitudes = new int[2];

                for (int i = 0; i < 2; i++) {
                    if (Children[i].Value.HasValue) {
                        magnitudes[i] = Children[i].Value!.Value;
                    }
                    else if (Children[i].Pair is not null) {
                        magnitudes[i] = Children[i].Pair!.Magnitude();
                    }
                    else {
                        throw new Exception("Element has no regular number nor a pair.");
                    }
                }
                return 3 * magnitudes[0] + 2 * magnitudes[1];
            }

            public void Reduce() {
                while (ReduceStep()) ;
            }

            private bool ReduceStep() {
                Stack<Tuple<Pair, int>> depthIndices = new();
                depthIndices.Push(new(this, 0));
                bool isExploding = false;
                int rightExplodeValue = 0;
                Element? previousValueElement = null;
                Element? elementToSplit = null;
                while (depthIndices.Count > 0) {
                    var entry = depthIndices.Pop();

                    if (!isExploding && depthIndices.Count == 4) {
                        // Explode
                        isExploding = true;
                        if (!entry.Item1.Children[0].Value.HasValue) {
                            throw new Exception("Left element at depth 4 is a pair.");
                        }
                        if (!entry.Item1.Children[1].Value.HasValue) {
                            throw new Exception("Left element at depth 4 is a pair.");
                        }

                        if (previousValueElement is not null) {
                            previousValueElement.Value += entry.Item1.Children[0].Value!.Value;
                        }
                        rightExplodeValue = entry.Item1.Children[1].Value!.Value;
                        entry = depthIndices.Pop();
                        entry.Item1.Children[entry.Item2 - 1].Value = 0;
                        entry.Item1.Children[entry.Item2 - 1].Pair = null;
                    }

                    if (entry.Item2 >= 2) {
                        continue;
                    }

                    Element element = entry.Item1.Children[entry.Item2];

                    if (element.Value.HasValue) {
                        if (isExploding) {
                            element.Value += rightExplodeValue;
                            return true;
                        }

                        previousValueElement = element;

                        if (elementToSplit is null && element.Value.Value > 9) {
                            // Split
                            elementToSplit = element;
                        }
                        if (entry.Item2 == 0) {
                            depthIndices.Push(new(entry.Item1, 1));
                        }
                        continue;
                    }
                    else if (element.Pair is not null) {
                        depthIndices.Push(new(entry.Item1, entry.Item2 + 1));
                        depthIndices.Push(new(element.Pair, 0));
                        continue;
                    }
                    else {
                        throw new Exception("Element has no regular number nor a pair.");
                    }
                }

                if (isExploding) {
                    return true;
                }

                if (elementToSplit is not null) {
                    int value = elementToSplit.Value!.Value;
                    elementToSplit.Pair = new(new Element(value / 2), new Element((value + 1) / 2));
                    elementToSplit.Value = null;
                    return true;
                }

                return false;
            }

            public override string ToString() {
                StringBuilder sb = new();
                sb.Append('[');
                for (int i = 0; i < 2; i++) {
                    if (Children[i].Value.HasValue) {
                        sb.Append(Children[i].Value!.Value);
                    }
                    else if (Children[i].Pair is not null) {
                        sb.Append(Children[i].Pair!.ToString());
                    }
                    else {
                        throw new Exception("Element has no regular number nor a pair.");
                    }
                    if (i < 1) {
                        sb.Append(',');
                    }
                }
                sb.Append(']');
                return sb.ToString();
            }
        }

        public string Solve(string[] data) {
            Pair[] snailNumbers = new Pair[data.Length];
            for (int i = 0; i < data.Length; i++) {
                snailNumbers[i] = Pair.Parse(data[i]);
            }
            int maxMagnitude = int.MinValue;

            for (int i = 0; i < snailNumbers.Length - 1; i++) {
                for (int j = i + 1; j < data.Length; j++) {
                    maxMagnitude = Math.Max(maxMagnitude, (snailNumbers[i] + snailNumbers[j]).Magnitude());
                    maxMagnitude = Math.Max(maxMagnitude, (snailNumbers[j] + snailNumbers[i]).Magnitude());
                }
            }

            return maxMagnitude.ToString();
        }
    }
}
