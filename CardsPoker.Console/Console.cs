using System;
using CardsPocker.Common;

namespace CardsPoker.Console
{
    /// <summary>
    /// Wrapper to allow testing
    /// </summary>
    public class Console : IConsole
    {
        private string _spaceBetweenTitleAndDesc = " ";

        public string SpaceBetweenTitleAndDesc => _spaceBetweenTitleAndDesc;

        public ConsoleColor BackgroundColor
        {
            get { return System.Console.BackgroundColor; }
            set { System.Console.BackgroundColor = value; }
        }

        public ConsoleColor ForegroundColor {
            get { return System.Console.ForegroundColor; }
            set { System.Console.ForegroundColor = value; }
        }

        public void WriteLine(string message = null)
        {
            System.Console.WriteLine(message);
        }

        public void Clear()
        {
            System.Console.Clear();
        }

        public void Write<T>(T message)
        {
            System.Console.Write(message);
        }

        public string ReadLine()
        {
            return System.Console.ReadLine();
        }

        public ConsoleKeyInfo ReadKey()
        {
            return System.Console.ReadKey();
        }

        public void WriteCommandDescriptionLine()
        {
            System.Console.Write(Constants.CommandTitle + SpaceBetweenTitleAndDesc + Constants.DescriptionTitle + System.Environment.NewLine);
        }

        public void SetupConsole()
        {
            BackgroundColor = ConsoleColor.Green;
            ForegroundColor = ConsoleColor.DarkMagenta;
            _spaceBetweenTitleAndDesc = new string(Constants.Space, 18);
        }
    }
}
