using AdventOfCode.Runner;
using AdventOfCode.Runner.Attributes;

namespace AdventOfCode.Problems.AOC2024.Day1
{
    [ProblemInfo(2024, 01, "Historian Hysteria")]
    internal class HistorianHysteria : Problem<int, int>
    {
        public string[] inputs = Array.Empty<string>();
        public List<int> leftList = new List<int>();
        public List<int> rightList = new List<int>();

        public override void CalculatePart1()
        {
            var totalDistant = 0;

            for (int i = 0; i < inputs.Length; i++) 
            {
                var input = inputs[i];
                var values = input.Split(' ');
                values = values.Where(v => !string.IsNullOrEmpty(v)).ToArray();
                
                leftList.Add(int.Parse(values[0]));
                rightList.Add(int.Parse(values[1]));
            }
            leftList.Sort();
            rightList.Sort();

            for (int i = 0; i < leftList.Count; i++)
            {
                var left = leftList[i];
                var right = rightList[i];

                totalDistant += Math.Abs(left - right);
            }
            Part1 = totalDistant;
        }

        public override void CalculatePart2()
        {
            var totalScore = 0;
            for (int i = 0; i < leftList.Count; i++)
            {
                var value = leftList[i];
                var count = rightList.Where(v => v == value).Count();
                totalScore += (value * count);
            }
            Part2 = totalScore;
        }

        public override void LoadInput()
        {
            inputs = ReadInputLines();
        }
    }
}
