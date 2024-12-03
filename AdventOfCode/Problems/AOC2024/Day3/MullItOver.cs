using System.Text.RegularExpressions;

using AdventOfCode.Runner;
using AdventOfCode.Runner.Attributes;

namespace AdventOfCode.Problems.AOC2024.Day3
{
    [ProblemInfo(2024, 03, "Mull It Over")]
    internal class MullItOver : Problem<int, int>
    {
        public string[] inputs = Array.Empty<string>();

        public override void CalculatePart1()
        {
            var result = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                var input = inputs[i];

                var c = Regex.Matches(input, @"mul\(\d+\,\d+\)");

                result += Calculate(c);
            }
            Part1 = result;
        }

        public override void CalculatePart2()
        {
            throw new NotImplementedException();
        }

        public override void LoadInput()
        {
            inputs = ReadInputLines();
        }

        public int Calculate(MatchCollection collection)
        {
            var result = 0;

            for (int i = 0; i < collection.Count; i++) 
            {
                var cur = collection[i];

                var numCollection = Regex.Matches(cur.Value, @"\d+");
                var v1 = int.Parse(numCollection[0].Value);
                var v2 = int.Parse(numCollection[1].Value);

                result += (v1 * v2);
            }
            return result;
        }
    }
}
