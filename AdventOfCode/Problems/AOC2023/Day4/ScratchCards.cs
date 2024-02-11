using AdventOfCode.Runner;
using AdventOfCode.Runner.Attributes;
using System.ComponentModel.Design;

namespace AdventOfCode.Problems.AOC2023.Day4
{
	[ProblemInfo(2023, 04, "Scratch Cards")]
	internal class ScratchCards : Problem<int, int>
	{
		public string[] inputs = Array.Empty<string>();
		public override void LoadInput()
		{
			inputs = ReadInputLines();
		}
		
		public override void CalculatePart1()
		{
			for (int i = 0; i < inputs.Length; i++)
			{
				var input = inputs[i];
				var index = input.IndexOf(":");
				var splitedInput = input.Remove(0, index).Split("|");
				var list1 = splitedInput[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
				var list2 = splitedInput[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
				var intersection = list1.Intersect(list2).Count(); ;
				if (intersection > 0)
				{
					var winning = Math.Pow(2, intersection - 1);
					Part1 += (int)winning;
				}
			}
		}

		public override void CalculatePart2()
		{
			var scratchCards = GetScratchCardsInfo();

			for (int i = 0; i < scratchCards.Count; i++)
			{
				var scratch = scratchCards[i];
				for (int j = 0; j < scratch.Copies; j++)
				{
					scratch.PileCount += 1;
					for (int k = 0; k < scratch.WinningCount; k++)
					{
						scratchCards[i + k + 1].Copies += 1;
					}
				}
				Part2 += scratchCards[i].PileCount;
			}
		}

		public Dictionary<int, ScratchCardsInfo> GetScratchCardsInfo()
		{
			var scratchCards = new Dictionary<int, ScratchCardsInfo>();

			for (int i = 0; i < inputs.Length; i++)
			{
				var input = inputs[i];
				var index = input.IndexOf(":");
				var splitedInput = input.Remove(0, index).Split("|");

				var list1 = splitedInput[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
				var list2 = splitedInput[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
				var intersection = list1.Intersect(list2).Count();
				if (intersection > 0)
					scratchCards.Add(i, new ScratchCardsInfo(intersection));
				else
					scratchCards.Add(i, new ScratchCardsInfo(intersection));
			}
			return scratchCards;
		}
	}
}

public class ScratchCardsInfo
{
	public int WinningCount { get; set; }
	public int PileCount { get; set; }
	public int Copies { get; set; }

	public ScratchCardsInfo(int winningCount)
	{
		WinningCount = winningCount;
		PileCount = 0;
		Copies = 1;
	}
}
