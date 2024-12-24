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
                var input = inputs[i].Split(" ").Select(s => int.Parse(s)).ToList();
                var hasFailed = HasFailedLevelsV2(input, out int location);
                if (!hasFailed)
                    IsSafe++;
            }
            Part1 = IsSafe;
        }

        public override void CalculatePart2()
        {
            var isSafe = 0;

            var successsList = new List<string>();
            var failList = new List<string>();
            for (int i = 0; i < inputs.Length; i++)
            {
                var input = inputs[i].Split(" ").Select(s => int.Parse(s)).ToList();
                var skip = ShouldSkipV2(input);

                if (!skip)
                {
                    isSafe++;
                    successsList.Add(inputs[i]);
                }
                else
                    failList.Add(inputs[i]);
            }
            Part2 = isSafe;
        }

        public override void LoadInput()
        {
            inputs = ReadInputLines();
            //inputs = ReadInputLines("example.txt");
            //inputs = ReadInputLines("example2.txt");
        }

        public bool ShouldSkipV2(List<int> input, bool hasFailed = false)
        {
            //var hasProblem = HasFailedLevels(report, out int location);
            var hasProblem = HasFailedLevelsV2(input, out int location);

            if (hasProblem) 
            {
                if (hasFailed)
                    return true;

                // check for failure at moment of failure
                var atList = new List<int>(input);
                atList.RemoveAt(location);
                var at = ShouldSkipV2(atList, true);

                // check for failure 1 level before
                var be = true;
                var beList = new List<int>(input);
                if (location > 0)
                {
                    beList.RemoveAt(location - 1);
                    be = ShouldSkipV2(beList, true);
                }

                // check for failure 1 level after
                var af = true;
                var afList = new List<int>(input);
                if (location < input.Count - 1)
                {
                    afList.RemoveAt(location + 1);
                    af = ShouldSkipV2(afList, true);
                }

                // check for failure 2 levels before
                var be2 = true;
                var be2List = new List<int>(input);
                if (location > 1)
                {
                    be2List.RemoveAt(location - 2);
                    be2 = ShouldSkipV2(be2List, true);
                }

                if (at == false || be == false || af == false || be2 == false)
                    return false;
                else
                    return true;
            }
            return false;
        }

        public bool HasFailedLevelsV2(List<int> input, out int failedLocation)
        {
            failedLocation = 0;
            var dir = Type.unset;

            for (int i = 0; i < input.Count - 1; i++)
            {
                var curLv = input[i];
                var nextLv = input[i + 1];
                var res = curLv - nextLv;

                if (Math.Abs(res) > 3 || res == 0)
                {
                    failedLocation = i + 1;
                    return true;
                }
                if (dir == Type.unset)
                    dir = res > 0 ? Type.decr : Type.incr;

                if (dir == Type.decr && res < 0)
                {
                    failedLocation = i + 1;
                    return true;
                }
                if (dir == Type.incr && res > 0)
                {
                    failedLocation = i + 1;
                    return true; 
                }
            }
            return false;
        }

        public enum Type
        {
            incr,
            decr,
            unset
        }
    }
}
