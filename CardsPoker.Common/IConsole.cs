using System;

namespace CardsPocker.Common
{
    public interface IConsole
    {
        void WriteLine(string message = null);

        void Write<T>(T message);

        void WriteCommandDescriptionLine();

        void SetupConsole();

        string ReadLine();

        ConsoleKeyInfo ReadKey();

        ConsoleColor BackgroundColor { get; set; }
        ConsoleColor ForegroundColor { get; set; }

        string SpaceBetweenTitleAndDesc { get; }
        void Clear();
    }
}
