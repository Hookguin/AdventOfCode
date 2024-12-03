using System.Net.Http.Headers;
using AdventOfCode.Runner;
using AdventOfCode.Runner.Attributes;

namespace AdventOfCode.Problems.AOC2024.Day2
{
    [ProblemInfo(2024, 02, "Red-Nosed Reports")]
    internal class RedNosedReports : Problem<int, int>
    {
        public string[] inputs = Array.Empty<string>();
        public int IsSafe = 0;

        public override void CalculatePart1()
        {
            for (int i = 0; i < inputs.Length; i++) 
            {
                var curReport = inputs[i];
                
                var levelList = curReport.Split(" ").Select(n => int.Parse(n)).ToArray();
                var type = Type.same;
                var skip = false;

                for (int j = 0; j < levelList.Length - 1; j++)
                {
                    var cur = levelList[j];
                    var next = levelList[j + 1];

                    if (Math.Abs(cur - next) > 3)
                    {
                        skip = true;
                        break;
                    }

                    // decrease
                    if (cur > next)
                    {
                        if (type == Type.incr)
                        {
                            skip = true;
                            break;
                        }
                        type = Type.decr;
                    }
                    if (cur < next)
                    {
                        if (type == Type.decr)
                        {
                            skip = true;
                            break;
                        }
                        type = Type.incr;
                    }
                    if (cur == next)
                    {
                        skip = true;
                        break;
                    }
                }
                if (skip == true)
                    continue;

                IsSafe++;
            }
            Part1 = IsSafe;
        }

        public override void CalculatePart2()
        {
            // try again later
            throw new NotImplementedException();
        }

        public override void LoadInput()
        {
            inputs = ReadInputLines();
        }

        public enum Type
        {
            incr,
            decr,
            same
        }
    }
}
