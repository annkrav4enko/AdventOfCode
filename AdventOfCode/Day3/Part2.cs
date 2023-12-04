using Shared;
using System.Text.RegularExpressions;

namespace Day3
{
    public class Part2
    {
        private List<string> lines;
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
