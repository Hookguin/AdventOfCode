using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Runner;
using AdventOfCode.Runner.Attributes;

namespace AdventOfCode.Problems.AOC2024.Day6
{
    [ProblemInfo(2024, 06, "Guard Gallivant")]
    internal class GuardGallivant : Problem<int, int>
    {
        public string[] inputs = Array.Empty<string>();

        public override void CalculatePart1()
        {
            var index = -1;
            var start = -1;
            for (int i = 0; i < inputs.Length; i++)
            {
                var input = inputs[i];
                index = input.IndexOf("^");

                if (index == -1)
                    continue;

                start = i - 1;
            }

            var keepGoing = true;
            var direction = Direction.up;

            do
            {
                switch()
            }
            while (keepGoing);

            throw new NotImplementedException();
        }

        public override void CalculatePart2()
        {
            throw new NotImplementedException();
        }

        public override void LoadInput()
        {
            inputs = ReadInputLines();
        }

        public bool GoingUp(int index, int start, out (int row, int column) result)
        {
            var isStopped = false;
            result.row = start;
            result.column = index;

            for (int i = start; i >= 0; i--)
            {
                var curC = inputs[i][index];
                
                if (curC == '#')
                {
                    isStopped = true;
                    result.row = i + 1;
                    break;
                }
                curC = 'x';
            }
            return isStopped;
        }

        public bool GoingRight(int index, int start, out (int row, int column) result)
        {
            var isStopped = false;
            result.row = start;
            result.column = index;

            for (int i = index; i < inputs[start].Length; i++)
            {
                var curC = inputs[start][index];

                if (curC == '#')
                {
                    isStopped = true;
                    result.column = i - 1;
                    break;
                }
                curC = 'x';
            }
            return isStopped;
        }

        public bool GoingDown(int index, int start, out (int row, int column) result)
        {
            var isStopped = false;
            result.row = start;
            result.column = index;

            for (int i = start; i < inputs.Length; i++)
            {
                var curC = inputs[i][index];

                if (curC == '#')
                {
                    isStopped = true;
                    result.row = i - 1;
                    break;
                }
                curC = 'x';
            }
            return isStopped;
        }

        public bool GoingLeft(int index, int start, out (int row, int column) result)
        {
            var isStopped = false;
            result.row = start;
            result.column = index;

            for (int i = start; i >= 0; i--)
            {
                var curC = inputs[start][index];
            
                if (curC == '#')
                {
                    isStopped = true;
                    result.column = i - 1;
                    break;
                }
                curC = 'x';
            }
            return isStopped;
        }

        private enum Direction
        {
            up,
            down,
            left,
            right
        }
    }
}
