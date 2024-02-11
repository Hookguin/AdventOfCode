using AdventOfCode.Runner;
using AdventOfCode.Runner.Attributes;

namespace AdventOfCode.Problems.AOC2023.Day1
{
	[ProblemInfo(2023, 01, "trebuchet")]
	internal class Trebuchet : Problem<int, int>
	{
		public string[] sadge = Array.Empty<string>();
		public override void LoadInput()
		{
			sadge = ReadInputLines();
			//ReadInputText();
		}
		
		public override void CalculatePart1()
		{
			var listOfNumber = new List<int>();
			for (int i = 0; i < sadge.Length; i++) 
			{
				var input = sadge[i];
				var numberInInput = new List<int>();
				for (int j = 0; j < input.Length; j++) 
				{
					var currentChar = input[j];
					var deadge = currentChar - '0';
					if (deadge < 10)
						numberInInput.Add(deadge);
				}
				var first = numberInInput.First();
				var last = numberInInput.Last();
				var math = first * 10 + last;
				listOfNumber.Add(math);
			}
			Part1 = listOfNumber.Sum();
		}

		public override void CalculatePart2()
		{
			var listOfNumber = new List<int>();
			for (int i = 0; i < sadge.Length; i++)
			{
				var input = sadge[i];
				var numberInInput = new List<int>();
				for (int j = 0; j < input.Length; j++)
				{
					var currentChar = input[j];
					var currentInput = input.Substring(j, input.Length - j);
					var numbers = new[]
					{
						"zero",
						"one",
						"two",
						"three",
						"four",
						"five",
						"six",
						"seven",
						"eight",
						"nine"
					};
					for (int k = 0; k < numbers.Length; k++)
					{
						var number = numbers[k];
						if (currentInput.StartsWith(number))
						{
							numberInInput.Add(k);
						}
					}
					var deadge = currentChar - '0';
					if (deadge < 10)
						numberInInput.Add(deadge);
				}
				var first = numberInInput.First();
				var last = numberInInput.Last();
				var math = first * 10 + last;
				listOfNumber.Add(math);
			}
			Part2 = listOfNumber.Sum();
		}
	}
}