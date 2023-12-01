using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day1
{
    public class Part2
    {
        public Part2() { }


        /*
         * Your calculation isn't quite right. It looks like some of the digits are actually spelled out with letters: one, two, three, four, five, six, seven, eight, and nine also count as valid "digits".

            Equipped with this new information, you now need to find the real first and last digit on each line. For example:

            two1nine
            eightwothree
            abcone2threexyz
            xtwone3four
            4nineeightseven2
            zoneight234
            7pqrstsixteen

            In this example, the calibration values are 29, 83, 13, 24, 42, 14, and 76. Adding these together produces 281.

            What is the sum of all of the calibration values?
         */

        public void Run()
        {
            Console.WriteLine("Starting reading from file");

            var fileReader = new FileReader();

            var lines = fileReader.ReadFile("input.txt");

            var dictionary = new Dictionary<string, int> {
                { "1", 1 },
                { "2", 2 },
                { "3", 3 },
                { "4", 4 },
                { "5", 5 },
                { "6", 6 },
                { "7", 7 },
                { "8", 8 },
                { "9", 9 },
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 }
            };

            var numbers = new List<int>();

            foreach (var line in lines)
            {
                var positions = dictionary.Select(i => Regex.Matches(line, i.Key).Cast<Match>().Select(m => m.Index).ToList<int>()).ToList();

                int currentMin = 100000;
                var currentMinIndex = 0;
                foreach(var position in positions)
                {
                    foreach(var item in position)
                    {
                        if( item <= currentMin)
                        {
                            currentMin = item;
                            currentMinIndex = positions.IndexOf(position);
                        }
                    }
                }


                var firstDigit = dictionary.ElementAt(currentMinIndex).Value;


                //var maxValue = positivePositions.Max();
                //var lastDigit = dictionary.ElementAt(positivePositions.IndexOf(maxValue)).Value;

                var currentMax = -1;
                var currentMaxIndex = 0;
                foreach (var position in positions)
                {
                    foreach (var item in position)
                    {
                        if (item >= currentMax)
                        {
                            currentMax = item;
                            currentMaxIndex = positions.IndexOf(position);
                        }
                    }
                }

                var lastDigit = dictionary.ElementAt(currentMaxIndex).Value;

                var number = firstDigit * 10 + lastDigit;
                numbers.Add(number);
                
            }

            var sum = numbers.Sum();

            Console.WriteLine($"Result: {sum}");
        }
    }
}
