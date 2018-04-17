using System;
using System.Text;
using CardsPocker.Common;

namespace CardsPoker.Common.Tests
{
    public class TestConsole : IConsole
    {
        public StringBuilder Output { get; set; }

        public void SetupConsole()
        {
            Output = new StringBuilder();
        }

        public string SpaceBetweenTitleAndDesc { get; }

        public void WriteLine(string message = null)
        {
            Output.AppendLine(message);
        }

        public void Write<T>(T message)
        {
            Output.Append(message);
        }

        public void WriteCommandDescriptionLine()
        {
            Output.AppendLine("TEST");
        }

        public string ReadLine()
        {
            return "1";
        }

        public ConsoleKeyInfo ReadKey()
        {
            return new ConsoleKeyInfo();
        }

        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }

        public void Clear()
        {
            Output.Clear();
        }
    }
}
