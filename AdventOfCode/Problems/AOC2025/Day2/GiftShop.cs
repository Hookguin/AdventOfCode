using System.Text.RegularExpressions;
using AdventOfCode.Runner;
using AdventOfCode.Runner.Attributes;

namespace AdventOfCode.Problems.AOC2025.Day2
{
    [ProblemInfo(2025, 02, "Secret Shop")]
    internal class GiftShop : Problem<long, long>
    {
        public string[] inputs = Array.Empty<string>();

        public override void LoadInput()
        {
            //inputs = ReadInputLines();
            inputs = ReadInputLines("example1.txt");
        }

        public override void CalculatePart1()
        {
            long ans = 0;

            foreach (var input in inputs)
            {
                var ranges = input.Split(",");

                foreach (var range in ranges)
                {
                    if (range == "")
                        continue;

                    var ids = range.Split('-');

                    var start = long.Parse(ids[0]);
                    var end = long.Parse(ids[1]);

                    ans += GetTotalFromInvalidIds(start, end);
                }
            }

            Part1 = ans;
        }

        public override void CalculatePart2()
        {
            long ans = 0;

            foreach (var input in inputs)
            {
                var ranges = input.Split(",");

                foreach (var range in ranges)
                {
                    if (range == "")
                        continue;

                    var ids = range.Split('-');

                    var start = long.Parse(ids[0]);
                    var end = long.Parse(ids[1]);

                    ans += GetTotalFromInvalidIdsV2(start, end);
                }
            }

            Part2 = ans;
        }

        public long GetTotalFromInvalidIds(long start, long end)
        {
            long res = 0;
            for (var i = start; i <= end; i++)
            {
                var id = i.ToString();

                if (id.Length % 2 != 0)
                    continue;
                
                var halfSize = id.Length / 2;
                var firstHalf = id.Substring(0, halfSize);
                var secondHalf = id.Substring(halfSize);
                
                if (firstHalf == secondHalf)
                    res += i;
            }
            return res;
        }

        public long GetTotalFromInvalidIdsV2(long start, long end)
        {
            long res = 0;
            for (var i = start; i <= end; i++)
            {
                var id = i.ToString();
                var pattern = "^\\d*(?<repeat>\\d+)\\k<repeat>+\\d*$";

                if (Regex.IsMatch(id, pattern))
                {
                    Console.WriteLine(id);
                    res += i;
                }
            }
            return res;
        }
    }
}
