using System.Text.RegularExpressions;

using AdventOfCode.Runner;
using AdventOfCode.Runner.Attributes;

namespace AdventOfCode.Problems.AOC2025.Day1
{
    [ProblemInfo(2025, 01, "Secret Entrance")]
    internal class SecretEntrance : Problem<int, int>
    {
        public string[] inputs = Array.Empty<string>();
        public int password1 = 0;
        public int password2 = 0;

        public override void LoadInput()
        {
            inputs = ReadInputLines();
            //inputs = ReadInputLines("example1.txt");
        }

        public override void CalculatePart1()
        {
            var curPos = 50;
            foreach (var input in inputs)
            {
                var match = Regex.Match(input, "\\d+");
                if (match.Value == "")
                    continue;

                var turningCount = int.Parse(match.Value);

                if (input.StartsWith("L"))
                    curPos = TurnLeft(curPos, turningCount);
                else
                    curPos = TurnRight(curPos, turningCount);
            }

            Part1 = password1;
        }

        public override void CalculatePart2() 
        {
            var curPos = 50;
            foreach (var input in inputs)
            {
                var match = Regex.Match(input, "\\d+");
                if (match.Value == "")
                    continue;

                var turningCount = int.Parse(match.Value);

                if (input.StartsWith("L"))
                    curPos = TurnLeftV2(curPos, turningCount);
                else
                    curPos = TurnRightV2(curPos, turningCount);
            }

            Part2 = password2;
        }

        private int TurnLeft(int curPos, int turningCount)
        {
            var newPos = curPos - turningCount;

            if (newPos < 0)
            {
                newPos = newPos % 100;
                if (newPos != 0)
                    newPos += 100;
            }

            if (newPos == 0)
                password1++;

            return Math.Abs(newPos);
        }

        private int TurnRight(int curPos, int turningCount)
        {
            var newPos = curPos + turningCount;
             
            if (newPos > 99)
                newPos = newPos % 100;

            if (newPos == 0)
                password1++;
            
            return newPos;
        }

        private int TurnLeftV2(int curPos, int turning)
        {
            for (int i = 0; i < turning; i++)
            {
                curPos--;
                if (curPos == 0)
                {
                    password2++;
                }
                if (curPos == -1)
                    curPos = 99;
            }

            return curPos;
        }

        private int TurnRightV2(int curPos, int turning)
        {
            for (int i = 0; i < turning; i++)
            {
                curPos++;
                if (curPos == 100)
                {
                    password2++;
                    curPos = 0;
                }
            }
            
            return curPos;
        }
    }
}
