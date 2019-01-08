using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HalloSarah
{
    class Program
    {
        static void Main(string[] args)
        {
            Enumerable.Range(1, 100)
                .Select(Fizz)
                .Select(Buzz)
                .ToList()
                .ForEach(i => Console.WriteLine($"{i.Zahl,+4:X2} => {i.Text}"));

            Console.ReadKey();

            (int Zahl, string Text) Fizz(int i) => (i, i % 3 == 0 ? "Fizz" : string.Empty);
            (int Zahl, string Text) Buzz((int i, string text) i) => (i.i, i.i % 3 == 0 ? i.text + "Buzz" : i.text);
        }
    }
}
