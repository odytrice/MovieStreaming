using System;

namespace MovieStreaming.Common
{
    public static class ColorConsole
    {
        public static void WriteColor(string message, ConsoleColor color)
        {
            lock (new{ })
            {
                var beforeColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ForegroundColor = beforeColor;
            }
        }

        public static void WriteLineGray(string message)
        {
            WriteColor(message, ConsoleColor.Gray);
        }

        public static void WriteLineCyan(string message)
        {
            WriteColor(message, ConsoleColor.Cyan);
        }

        public static void WriteLineGreen(string message)
        {
            WriteColor(message, ConsoleColor.Green);
        }

        public static void WriteMagenta(string message)
        {
            WriteColor(message, ConsoleColor.Magenta);
        }

        public static void WriteLineRed(string message)
        {
            WriteColor(message, ConsoleColor.Red);
        }
        public static void WriteLineYellow(string message)
        {
            WriteColor(message, ConsoleColor.Yellow);
        }

        public static void WriteLineWhite(string message)
        {
            WriteColor(message, ConsoleColor.White);
        }
    }
}
