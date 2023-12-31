﻿using Shared;
using System.Text.RegularExpressions;

namespace Day3
{
    public class Part1
    {
        private List<string> lines;

        /*
         * Here is an example engine schematic:

            467..114..
            ...*......
            ..35..633.
            ......#...
            617*......
            .....+.58.
            ..592.....
            ......755.
            ...$.*....
            .664.598..
            In this schematic, two numbers are not part numbers because they are not adjacent to a symbol: 114 (top right) and 58 (middle right). Every other number is adjacent to a symbol and so is a part number; their sum is 4361.

            Of course, the actual engine schematic is much larger. What is the sum of all of the part numbers in the engine schematic?
         */
        public void Run()
        {
            Console.WriteLine("Starting reading from file");

            var fileReader = new FileReader();

            lines = fileReader.ReadFile("input.txt");

            var adjacentNumbers = new List<int>();

            var symbolsAbove = false;
            var symbosInSame = false;
            var symbolsBelow = false;
            foreach(var line in lines)
            {
                var numbers = Regex.Matches(line, "[0-9]+").Cast<Match>();

                foreach (var number in numbers)
                {
                    symbolsAbove = lines.IndexOf(line) >= 1
                        ? IsSymbolThere(line, number, lines.IndexOf(line) - 1)
                        : false;

                    var symbolLeft = number.Index == 0 ? false : line[number.Index - 1] != char.Parse(".");
                    var symbolRight = number.Index + number.Length == line.Length ? false : line[number.Index + number.Length] != char.Parse(".");

                    symbosInSame = symbolLeft || symbolRight;

                    symbolsBelow = lines.IndexOf(line) != lines.Count -1
                        ? IsSymbolThere(line, number, lines.IndexOf(line) + 1)
                        : false;

                    if (symbolsAbove || symbosInSame || symbolsBelow)
                    {
                        adjacentNumbers.Add(Int32.Parse(number.Value));
                    }
                }
            }

            var result = adjacentNumbers.Sum();

            Console.WriteLine($"Result: {result}");
        }

        private bool IsSymbolThere(string line, Match number, int lineIndex)
        {
            // Process if number is in the start of string
            if (number.Index == 0)
            {
                return Regex.Matches(lines[lineIndex].Substring(number.Index, number.Length + 1), "[^.0-9]").Count > 0;
            }
            // Process if number is in the end of string
            else if (number.Index + number.Length == line.Length)
            {
                return Regex.Matches(lines[lineIndex].Substring(number.Index - 1, number.Length + 1), "[^.0-9]").Count > 0;
            }
            else
            {
                return Regex.Matches(lines[lineIndex].Substring(number.Index - 1, number.Length + 2), "[^.0-9]").Count > 0;
            }
        }
    }
}
