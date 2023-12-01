using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Day1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Part1();
            p1.Run();

            var p2 = new Part2();
            p2.Run();

            Console.ReadLine();
        }
    }
}