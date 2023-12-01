using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    public class Part1
    {
        public Part1() { }

        /*
            each line originally contained a specific calibration value that the Elves now need to recover. 
            On each line, the calibration value can be found by combining the first digit and the last digit (in that order) to form a single two-digit number.

            For example:

            1abc2
            pqr3stu8vwx
            a1b2c3d4e5f
            treb7uchet

            In this example, the calibration values of these four lines are 12, 38, 15, and 77. Adding these together produces 142.

            What is the sum of all of the calibration values?
         */

        public void Run()
        {
            Console.WriteLine("Starting reading from file");

            var fileReader = new FileReader();

            var lines = fileReader.ReadFile("input.txt");

            var numbers = new List<int>();

            foreach (var line in lines)
            {
                var item = new String(line.Where(Char.IsDigit).ToArray());
                if (item.Length == 1)
                {
                    item += item;
                }

                if (item.Length > 1)
                {
                    item = string.Concat(item.First(), item.Last());
                }

                var number = Int32.Parse(item);

                numbers.Add(number);
            }

            var sum = numbers.Sum();

            Console.WriteLine($"Result: {sum}");
        }
    }
}
