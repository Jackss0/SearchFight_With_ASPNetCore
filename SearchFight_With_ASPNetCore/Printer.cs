using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFight_With_ASPNetCore
{
    public class Printer
    {
        public static void Print(string print)
        {
            Console.Write(print);
            Console.ResetColor();
        }
        public static void PrintGray(string print)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(print);
            Console.ResetColor();
        }

        public static void PrintBlue(string print)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write(print);
            Console.ResetColor();
        }

        public static void PrintYellow(string print)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(print);
            Console.ResetColor();
        }
    }
}
