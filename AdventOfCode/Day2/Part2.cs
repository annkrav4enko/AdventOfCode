using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using System.Text.RegularExpressions;

namespace Day2
{
    // For each game, find the minimum set of cubes that must have been present.What is the sum of the power of these sets?
    public class Part2
    {
        public void Run()
        {
            Console.WriteLine("Starting reading from file");

            var fileReader = new FileReader();

            var lines = fileReader.ReadFile("input.txt");

            var games = new List<int>();

            foreach (var line in lines)
            {
                var id = Int32.Parse(Regex.Match(line, "(?<=Game\\s)[0-9]+").Value);

                var reds = Regex.Matches(line, "[0-9]+(?= red)").Cast<Match>().Select(m => Int32.Parse(m.Value)).ToArray();
                var greens = Regex.Matches(line, "[0-9]+(?= green)").Cast<Match>().Select(m => Int32.Parse(m.Value)).ToArray();
                var blues = Regex.Matches(line, "[0-9]+(?= blue)").Cast<Match>().Select(m => Int32.Parse(m.Value)).ToArray();

                var biggestReds = reds.Max();
                var biggestGreens = greens.Max();
                var biggestBlues = blues.Max();

                games.Add(biggestReds*biggestBlues*biggestGreens);
            }

            var result = games.Sum();

            Console.WriteLine($"Result: {result}");
        }
    }
}
