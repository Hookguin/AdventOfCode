using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;
using AdventOfCode.Runner;
using AdventOfCode.Runner.Attributes;
using static System.Net.Mime.MediaTypeNames;

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
            var skip = false;
            var isSafe = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                var input = inputs[i];
                var levels = input.Split(" ").Select(s => int.Parse(s)).ToList();

                skip = ShouldSkip(levels);
                if (!skip)
                    isSafe++;
            }
            Part2 = isSafe;
        }

        public override void LoadInput()
        {
            inputs = ReadInputLines();
            //inputs = ReadInputLines("example.txt");
        }

        public bool ShouldSkip(List<int> input, bool failedBefore = false) 
        {
            var newList = new List<int>();
            for (int i = 0; i < input.Count - 1; i++)
            {
                var curLv = input[i];
                var nextLv = input[i + 1];
                newList.Add(curLv - nextLv);
            }

            var failCount = 0;
            if (newList.Any(n => n < 0))
            {
                var neg = newList.Where(n => n < 0).Count();
                
                if (newList.Count - neg == 1)
                {
                    var n = newList.IndexOf(newList.Where(n => n >= 0).First());
                    newList.RemoveAt(n);
                    input.RemoveAt(n + 1);
                }
                
                if (neg != newList.Count)
                    return true;
            }

            for (int i = 0; i < newList.Count; i++) 
            {
                newList[i] = Math.Abs(newList[i]);
            }

            failCount = newList.Where(n => Math.Abs(n) == 0 || Math.Abs(n) > 3).Count();

            if (failCount > 1)
                return true;
            if (failCount == 1)
            {
                if (failedBefore)
                    return true;

                var failedIndex = newList.IndexOf(newList.Where(n => n > 3 || n == 0).First());

                var atList = new List<int>(input);
                atList.RemoveAt(failedIndex);
                var at = ShouldSkip(atList, true);

                bool be = true;
                if (failedIndex > 0)
                {
                    var beList = new List<int>(input);
                    beList.RemoveAt(failedIndex - 1);
                    be = ShouldSkip(beList, true);
                }

                bool af = true;
                if (failedIndex < input.Count - 1)
                {
                    var afList = new List<int>(input);
                    afList.RemoveAt(failedIndex + 1);
                    af = ShouldSkip(afList, true);
                }

                if (at == false || be == false || af == false)
                    return false;
                else
                    return true;
            }
            return false;
        }

        public enum Type
        {
            incr,
            decr,
            same
        }
    }
}
