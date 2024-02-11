using AdventOfCode.Runner;
using AdventOfCode.Runner.Attributes;
using System.Diagnostics.Contracts;
using System.Linq;

namespace AdventOfCode.Problems.AOC2023.Day3
{
	[ProblemInfo(2023, 03, "Gear Ratios")]
	internal class GearRatios : Problem<int, int>
	{
		public string[] inputs = Array.Empty<string>();
		public string[] symbols = new[] { "*", "$", "@", "%", "^", "&", "+", "#", "!", "/", "=", "-" };
		public char[] allSymbols = new[] { '*', '$', '@', '%', '^', '&', '.', '+', '#', '!', '/', '=', '-', '\n' };
		public int[] numbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		public List<Location> symbolLocations = new List<Location>();

		public override void LoadInput()
		{
			inputs = ReadInputLines();
			symbolLocations = GetSymbolLocation();
		}
		
		public override void CalculatePart1()
		{
			var engineNumbers = new List<int>();
			for (int i = 0; i < inputs.Length; i++)
			{
				var input = inputs[i];
				for (int j = 0; j < input.Length; j++)
				{
					var currentChar = input[j];
					if (currentChar < '0' || currentChar > '9')
						continue;

					var stopPoint = input.IndexOfAny(allSymbols, j);
					if (stopPoint == -1)
						stopPoint = input.Length;
					var number = Int32.Parse(input[j..stopPoint]);
					var engineNumber = new EngineNumberLocation(number, i, j, stopPoint - 1);
                    j = stopPoint;

					if (CheckNeighbor(i, engineNumber))
						engineNumbers.Add(engineNumber.EngineNumber);
				}
			}
			Part1 = engineNumbers.Sum();
		}

		public override void CalculatePart2()
		{
			var numberLocations = GetEngineNumberLocation();
			var starLocations = symbolLocations.Where(s => s.Symbol == '*');

			foreach (var starLocation in starLocations)
			{
				var nearbyNumber = numberLocations.Where(number => number.Row >= starLocation.Row - 1 && number.Row <= starLocation.Row + 1);
				var gearNumbers = nearbyNumber.Where(number => IsBetween(starLocation.IndexOfSymbol, number.FirstIndex, number.LastIndex)).ToArray();
				if (gearNumbers.Length == 2)
				{
					var ratio = gearNumbers[0].EngineNumber * gearNumbers[1].EngineNumber;
					Part2 += ratio;
				}
			}
		}

		public List<Location> GetSymbolLocation()
		{
			var symbolLocation = new List<Location>();
			for (int i = 0; i < inputs.Length; i++)
			{
				var input = inputs[i];

				for (int j = 0; j < input.Length; j++)
				{
					var currentChar = input[j];
					if (symbols.Contains(currentChar.ToString()))
					{
						var location = new Location(currentChar, i, j);
						symbolLocation.Add(location);
					}
				}
			}
			return symbolLocation;
		}
		
		public List<EngineNumberLocation> GetEngineNumberLocation()
		{
			var numberLocations = new List<EngineNumberLocation>();
			for (int i = 0; i < inputs.Length; i++)
			{
				var input = inputs[i];
				for (int j = 0; j < input.Length; j++)
				{
					var currentChar = input[j];
					if (currentChar < '0' || currentChar > '9')
						continue;
					var stopPoint = input.IndexOfAny(allSymbols, j);
					if (stopPoint == -1)
						stopPoint = input.Length;
					var number = Int32.Parse(input[j..stopPoint]);
					numberLocations.Add(new EngineNumberLocation(number, i, j, stopPoint - 1));
					j = stopPoint;
				}
			}
			return numberLocations;
		}

		public bool CheckNeighbor(int numberRow, EngineNumberLocation engineNumberLocation)
		{
			var nearbySymbol = symbolLocations.Where(s => s.Row >= numberRow - 1 && s.Row <= numberRow + 1 );
			return nearbySymbol.Any(row => IsBetween(row.IndexOfSymbol, engineNumberLocation.FirstIndex, engineNumberLocation.LastIndex));
		}
		
		public static bool IsBetween(int input, int start, int end)
		{
			if (input >= start - 1 && input <= end + 1)
				return true;
			return false;
		}
	}
}

public class Location
{
	public char Symbol { get; set; }
	public int Row { get; set; }
	public int IndexOfSymbol { get; set; }

	public Location(char symbol, int inputRow, int indexOfSymbol)
	{
		Symbol = symbol;
		Row = inputRow;
		IndexOfSymbol = indexOfSymbol;
	}
}

public class EngineNumberLocation
{
	public int EngineNumber { get; set; } 
	public int Row { get; set; }
	public int FirstIndex { get; set; }
	public int LastIndex { get; set; }

	public EngineNumberLocation(int engineNumber, int row, int firstIndex, int lastIndex)
	{
		EngineNumber = engineNumber;
		Row = row;
		FirstIndex = firstIndex;
		LastIndex = lastIndex;
	}
}
