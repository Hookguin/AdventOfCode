using AdventOfCode.Runner;
using AdventOfCode.Runner.Attributes;

namespace AdventOfCode.Problems.AOC2024.Day6
{
    [ProblemInfo(2024, 06, "Guard Gallivant")]
    internal class GuardGallivant : Problem<int, int>
    {
        public string[] Inputs = Array.Empty<string>();

        public override void CalculatePart1()
        {
            var index = -1;
            var start = -1;
            for (int i = 0; i < Inputs.Length; i++)
            {
                var input = Inputs[i];
                var indexOf = input.IndexOf('^');

                if (indexOf == -1)
                    continue;

                index = indexOf;
                start = i - 1;
            }

            var keepGoing = true;
            var direction = Direction.up;

            do
            {
                switch(direction)
                {
                    case Direction.up:
                        keepGoing = GoingUp(index, start, out var up);
                        if (keepGoing)
                        {
                            direction = Direction.right;
                            index = up.column;
                            start = up.row;
                        }
                        break;

                    case Direction.right:
                        keepGoing = GoingRight(index, start, out var right);
                        if (keepGoing)
                        {
                            direction = Direction.down;
                            index = right.column;
                            start = right.row;
                        }
                        break;

                    case Direction.down:
                        keepGoing = GoingDown(index, start, out var down);
                        if (keepGoing)
                        {
                            direction = Direction.left;
                            index = down.column;
                            start = down.row;
                        }
                        break;

                    case Direction.left:
                        keepGoing = GoingLeft(index, start, out var left);
                        if (keepGoing)
                        {
                            direction = Direction.up;
                            index = left.column;
                            start = left.row;
                        }
                        break;
                }
            }
            while (keepGoing);

            Part1 = CountX();
        }

        public override void CalculatePart2()
        {
            throw new NotImplementedException();
        }

        public override void LoadInput()
        {
            Inputs = ReadInputLines();
            //Inputs = ReadInputLines("example.txt");
        }

        public int CountX()
        {
            var result = 0;
            for (int i = 0; i < Inputs.Length; i++) 
            {
                var input = Inputs[i];
                var xCount = input.Count(c => c == 'x');
                result += xCount;
            }
            // to include the starting point
            result += 1;
            return result;
        }

        public bool GoingUp(int index, int start, out (int row, int column) result)
        {
            var isStopped = false;
            result.row = start;
            result.column = index;

            for (int i = start; i >= 0; i--)
            {
                if (Inputs[i][index] == '#')
                {
                    isStopped = true;
                    result.row = i + 1;
                    break;
                }
                var newString = Inputs[i].ToCharArray();
                newString[index] = 'x';
                Inputs[i] = new string(newString);
            }
            return isStopped;
        }

        public bool GoingRight(int index, int start, out (int row, int column) result)
        {
            var isStopped = false;
            result.row = start;
            result.column = index;

            for (int i = index; i < Inputs[start].Length; i++)
            {
                if (Inputs[start][i] == '#')
                {
                    isStopped = true;
                    result.column = i - 1;
                    break;
                }
                var newString = Inputs[start].ToCharArray();
                newString[i] = 'x';
                Inputs[start] = new string(newString);
            }
            return isStopped;
        }

        public bool GoingDown(int index, int start, out (int row, int column) result)
        {
            var isStopped = false;
            result.row = start;
            result.column = index;

            for (int i = start; i < Inputs.Length; i++)
            {
                if (Inputs[i][index] == '#')
                {
                    isStopped = true;
                    result.row = i - 1;
                    break;
                }
                var newString = Inputs[i].ToCharArray();
                newString[index] = 'x';
                Inputs[i] = new string(newString);
            }
            return isStopped;
        }

        public bool GoingLeft(int index, int start, out (int row, int column) result)
        {
            var isStopped = false;
            result.row = start;
            result.column = index;

            for (int i = index; i >= 0; i--)
            {
                if (Inputs[start][i] == '#')
                {
                    isStopped = true;
                    result.column = i + 1;
                    break;
                }
                var newString = Inputs[start].ToCharArray();
                newString[i] = 'x';
                Inputs[start] = new string(newString);
            }
            return isStopped;
        }

        private enum Direction
        {
            up,
            down,
            left,
            right
        }
    }
}
