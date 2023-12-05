using Shared;
using System.Text.RegularExpressions;

namespace Day4
{
    public class Part1
    {
        /*
         * As far as the Elf has been able to figure out, you have to figure out which of the numbers you have appear in the list of winning numbers. The first match makes the card worth one point and each match after the first doubles the point value of that card.

            For example:

            Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
            Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
            Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
            Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
            Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
            Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
            In the above example, card 1 has five winning numbers (41, 48, 83, 86, and 17) and eight numbers you have (83, 86, 6, 31, 17, 9, 48, and 53). Of the numbers you have, four of them (48, 83, 17, and 86) are winning numbers! That means card 1 is worth 8 points (1 for the first match, then doubled three times for each of the three matches after the first).

            Card 2 has two winning numbers (32 and 61), so it is worth 2 points.
            Card 3 has two winning numbers (1 and 21), so it is worth 2 points.
            Card 4 has one winning number (84), so it is worth 1 point.
            Card 5 has no winning numbers, so it is worth no points.
            Card 6 has no winning numbers, so it is worth no points.
            So, in this example, the Elf's pile of scratchcards is worth 13 points.

            Take a seat in the large pile of colorful cards. How many points are they worth in total?
         */
        public void Run()
        {
            Console.WriteLine("Starting reading from file");

            var fileReader = new FileReader();

            var lines = fileReader.ReadFile("input.txt");

            var games = new List<double>();

            var winningNumbers = new List<int>();
            var regularNumbers = new List<int>();
            foreach (var line in lines)
            {
                var numbers = Regex.Matches(line, "[0-9]+").Cast<Match>().Select(n => Int32.Parse(n.Value)).ToList<int>();

                winningNumbers = numbers.GetRange(1, 10);//numbers.Where(n => numbers.IndexOf(n) >= 1 && numbers.IndexOf(n) <= 10).ToList();
                regularNumbers = numbers.GetRange(11, 25);//Where(n => numbers.IndexOf(n) >= 11).ToList();

                var winPoints = winningNumbers.Where(w => regularNumbers.Contains(w)).Count();//.Select(n => Math.Pow(2, n-1));
                
                if(winPoints >= 1)
                {
                    games.Add(Math.Pow(2, winPoints - 1));
                }
            }

            var result = games.Sum();

            Console.WriteLine($"Result: {result}");
        }
    }
}
