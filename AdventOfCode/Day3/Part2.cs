using Shared;
using System.Text.RegularExpressions;

namespace Day3
{
    public class Part2
    {
        private List<string> lines;

        /*
         * Consider the same engine schematic again:

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
            In this schematic, there are two gears. The first is in the top left; it has part numbers 467 and 35, so its gear ratio is 16345. The second gear is in the lower right; its gear ratio is 451490. (The * adjacent to 617 is not a gear because it is only adjacent to one part number.) Adding up all of the gear ratios produces 467835.

            What is the sum of all of the gear ratios in your engine schematic?
         */
        public void Run()
        {
            Console.WriteLine("Starting reading from file");

            var fileReader = new FileReader();

            lines = fileReader.ReadFile("input.txt");

            var adjacentNumbers = new List<int>();

            foreach (var line in lines)
            {
                //Console.WriteLine($"Line {lines.IndexOf(line)}");
                var numbers = Regex.Matches(line, "[0-9]+").Cast<Match>();
                var stars = Regex.Matches(line, "[*]").Cast<Match>();
                // If line does not contain stars
                if(stars.Count() == 0)
                {
                    continue;
                }

                foreach (var star in stars)
                {

                    var numbersToMultiply = new List<int>();
                    // Number Above

                    // If start is not on first line
                    if (lines.IndexOf(line) != 0)
                    {
                        var prevNumbers = Regex.Matches(lines[lines.IndexOf(line) - 1], "[0-9]+").Cast<Match>();
                        var numbersAbove = prevNumbers.Where(n =>
                            n.Index + n.Length == star.Index
                            || (n.Index <= star.Index && star.Index <= n.Index + n.Length - 1)
                            || star.Index + 1 == n.Index);

                        if (numbersAbove != null)
                        {
                            foreach(var number in numbersAbove)
                            {
                                numbersToMultiply.Add(Int32.Parse(number.Value));
                            }
                        }
                    }

                    var leftNumber = numbers.Where(n => n.Index + n.Length == star.Index).FirstOrDefault();
                    if(leftNumber != null)
                    {
                        numbersToMultiply.Add(Int32.Parse(leftNumber.Value));
                    }

                    var rightNumber = numbers.Where(n => n.Index == star.Index + 1).FirstOrDefault();
                    if(rightNumber != null)
                    {
                        numbersToMultiply.Add(Int32.Parse(rightNumber.Value));
                    }

                    // Number Below

                    // If star is not on last line
                    if (lines.IndexOf(line) != lines.Count -1)
                    {
                        var nextNumbers = Regex.Matches(lines[lines.IndexOf(line) + 1], "[0-9]+").Cast<Match>();
                        var numbersBelow = nextNumbers.Where(n =>
                            n.Index + n.Length == star.Index
                            || (n.Index <= star.Index && star.Index <= n.Index + n.Length - 1)
                            || star.Index + 1 == n.Index);

                        if (numbersBelow != null)
                        {
                            foreach(var number in numbersBelow)
                            {
                                numbersToMultiply.Add(Int32.Parse(number.Value));
                            }
                        }
                    }

                    if (numbersToMultiply.Count == 2)
                    {
                        Console.WriteLine($"Numbers: {numbersToMultiply[0]} and {numbersToMultiply[1]}");
                        var mult = numbersToMultiply[0] * numbersToMultiply[1];
                        adjacentNumbers.Add(mult);
                    }
                }   
            }

            var result = adjacentNumbers.Sum();

            Console.WriteLine($"Result: {result}");
        }
    }
}
