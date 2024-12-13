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
            for (int i = 0; i < inputs.Length; i++)
            {
                var input = inputs[i];
                index = input.IndexOf("^");

                if (index == -1)
                    continue;
            }



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

        public bool GoingUp(int index, int start, out int stoppedRow)
        {
            var isStopped = false;
            stoppedRow = -1;

            for (int i = start; i >= 0; i--)
            {
                var curC = inputs[i][index];
                
                if (curC == '#')
                {
                    isStopped = true;
                    stoppedRow = i + 1;
                    break;
                }
                curC = 'x';
            }
            return isStopped;
        }

        public bool GoingRight(int index, int start, out int stoppedColumn)
        {
            var isStopped = false;
            stoppedColumn = -1;

            for (int i = index; i < inputs[start].Length; i++)
            {
                var curC = inputs[start][index];

                if (curC == '#')
                {
                    isStopped = true;
                    stoppedColumn = i - 1;
                    break;
                }
                curC = 'x';
            }
            return isStopped;
        }

        public bool GoingDown(int index, int start, out int stoppedRow)
        {
            var isStopped = false;
            stoppedRow = -1;

            for (int i = start; i < inputs.Length; i++)
            {
                var curC = inputs[i][index];

                if (curC == '#')
                {
                    isStopped = true;
                    stoppedRow = i - 1;
                    break;
                }
                curC = 'x';
            }
            return isStopped;
        }

        public bool GoingLeft(int index, int start, out int stoppedColumn)
        {
            var isStopped = false;
            stoppedColumn = -1;

            for (int i = start; i >= 0; i--)
            {
                var curC = inputs[start][index];
            
                if (curC == '#')
                {
                    isStopped = true;
                    stoppedColumn = i - 1;
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
