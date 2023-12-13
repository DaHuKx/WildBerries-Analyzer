using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildBerriesAnalyzer.Domain.Interfaces;

namespace WildBerriesAnalyzer.ConsoleTest
{
    public class ConsoleNotifier : INotifier
    {
        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("Error: ");
            Console.ResetColor();
            Console.WriteLine(message);
        }

        public void Ok(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Ok: ");
            Console.ResetColor();
            Console.WriteLine(message);
        }

        public void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Warning: ");
            Console.ResetColor();
            Console.WriteLine(message);
        }
    }
}
