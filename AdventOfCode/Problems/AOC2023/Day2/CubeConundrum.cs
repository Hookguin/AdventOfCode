using AdventOfCode.Runner;
using AdventOfCode.Runner.Attributes;

namespace AdventOfCode.Problems.AOC2023.Day2
{
	[ProblemInfo(2023, 02, "Cube Conundrum")]
	internal class CubeConundrum : Problem<int, int>
	{
		public string[] inputs = Array.Empty<string>();
		public int redCube = 12;
		public int greenCube = 13;
		public int blueCube = 14;

		public override void LoadInput()
		{
			inputs = ReadInputLines();
		}
		
		public override void CalculatePart1()
		{
			var listOfPossibleRound = new List<int>();
			for (int i = 0; i < inputs.Length; i++)
			{
				var input = inputs[i];
				var isPossible = false;
				var index = input.IndexOf(":");

				var normalized = input.Remove(0, index).Replace(" ", "").ToLower();
				var cubeScores = normalized.Split(";").ToArray();
				foreach (var currentCubeScore in cubeScores)
				{
					isPossible = CheakForCubePossibility(currentCubeScore);
					if (!isPossible)
						break;
				}
				if (isPossible)
						listOfPossibleRound.Add(i + 1);
			}
			if (listOfPossibleRound.Any())
				Part1 = listOfPossibleRound.Sum();
			else
				Part1 = 0;
		}

		public override void CalculatePart2()
		{
			var listOfScores = new List<int>();
			for (int i = 0; i < inputs.Length; i++)
			{
				var input = inputs[i];
				var index = input.IndexOf(":");
				var normalized = input.Remove(0, index).Replace(" ", "").ToLower();
				var cubeScores = normalized.Split(";").ToArray();
				listOfScores.Add(CalculateScore(cubeScores));
			}
			if (listOfScores.Any())
				Part2 =listOfScores.Sum();
			else
				Part2 = 0;
		}

		public bool CheakForCubePossibility(string currentCubeScore)
		{
			var isPossible = true;
			var groupedCubes = currentCubeScore.Split(",");
			foreach (var currentColor in groupedCubes)
			{
				Int32.TryParse(string.Concat(currentColor.Where(Char.IsDigit)), out var score);

				if (currentColor.Contains("red") && score <= redCube)
					continue;
				if (currentColor.Contains("green") && score <= greenCube)
					continue;
				if (currentColor.Contains("blue") && score <= blueCube)
					continue;
				isPossible = false;
			}
			return isPossible;
		}

		public int CalculateScore(string[] cubeScores)
		{
			int mostRed = 0;
			int mostGreen = 0;
			int mostBlue = 0;
			
			foreach (var currentCubeScore in cubeScores)
			{
				var groupedCubes = currentCubeScore.Split(",");
				foreach (var currentColor in groupedCubes)
				{
					Int32.TryParse(string.Concat(currentColor.Where(Char.IsDigit)), out var score);
					if (currentColor.Contains("red"))
					{
						mostRed = (score > mostRed) ? score : mostRed;
						continue;
					}
					if (currentColor.Contains("green"))
					{
						mostGreen = (score > mostGreen) ? score : mostGreen;
						continue;
					}
					if (currentColor.Contains("blue"))
					{
						mostBlue = (score > mostBlue) ? score : mostBlue;
						continue;
					}
				}
			}
			return mostRed * mostGreen * mostBlue ;
		}
	}
}
