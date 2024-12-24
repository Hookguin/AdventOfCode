using AdventOfCode.Runner;
using AdventOfCode.Runner.Attributes;

namespace AdventOfCode.Problems.AOC2024.Day4
{
    [ProblemInfo(2024, 04, "Ceres Search")]
    internal class CeresSearch : Problem<int, int>
    {
        public string[] inputs = Array.Empty<string>();

        public override void CalculatePart1()
        {
            var score = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                var curLine = inputs[i];

                for (int j = 0; j < curLine.Length; j++)
                {
                    var curChar = curLine[j];

                    if (curChar == 'X')
                    {
                        var keyword = "MAS";

                        if (FindEast(keyword, i , j))
                            score++;

                        if (FindSouthWest(keyword, i, j))
                            score++;

                        if (FindSouth(keyword, i, j))
                            score++;

                        if (FindSouthEast(keyword, i, j))
                            score++;
                    }

                    if (curChar == 'S')
                    {
                        var keyword = "AMX";

                        if (FindEast(keyword, i, j))
                            score++;

                        if (FindSouthWest(keyword, i, j))
                            score++;

                        if (FindSouth(keyword, i, j))
                            score++;

                        if (FindSouthEast(keyword, i, j))
                            score++;
                    }
                }
            }
            Part1 = score;
        }

        public override void CalculatePart2()
        {
            throw new NotImplementedException();
        }

        public override void LoadInput()
        {
            inputs = ReadInputLines();
            //inputs = ReadInputLines("example.txt");
        }

        public bool FindEast (string sample, int curLine, int index)
        {
            if ((inputs[curLine].Length - index) < 4)
                return false;

            for (int i = 0; i < sample.Length; i++)
            {
                var c = sample[i];

                index++;

                var d = inputs[curLine][index];

                if (d != c)
                    return false;
            }
            return true;
        }

        public bool FindSouthWest (string sample, int curLine, int index)
        {
            if (index < 3 || (inputs.Length - curLine) < 4)
                return false;

            for (int i = 0; i < sample.Length; i++)
            {
                var c = sample[i];

                curLine++;
                index--;

                var d = inputs[curLine][index];

                if (d != c)
                    return false;
            }
            return true;
        }

        public bool FindSouth (string sample, int curLine, int index)
        {
            if ((inputs.Length - curLine) < 4)
                return false;

            for (int i = 0; i < sample.Length; i++)
            {
                var c = sample[i];

                curLine++;

                var d = inputs[curLine][index];

                if (d != c)
                    return false;
            }
            return true;
        }

        public bool FindSouthEast (string sample, int curLine, int index) 
        {
            if ((inputs.Length - curLine) < 4 || (inputs[curLine].Length - index) < 4)
                return false;

            for (int i = 0; i < sample.Length; i++)
            {
                var c = sample[i];

                curLine++;
                index++;

                var d = inputs[curLine][index];

                if (d != c)
                    return false;
            }
            return true;
        }
    }
}
