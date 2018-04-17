using System;
using CardsPocker.Common;

namespace CardsPoker.Console.Menu
{
    public class IntegerMenuOption : MenuOption
    {
        private readonly Func<string, string> _handler;

        public IntegerMenuOption(string title, string inputRequest, Func<string, string> handler, IConsole console) : base(title, inputRequest, console)
        {
            _handler = handler;
        }

        public override void RenderWithChildren(dynamic optionNumber)
        {
            RenderOptionOnly(optionNumber);
        }

        public override void HandleInput(string input)
        {
            string errorMessage = _handler(input);
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                Console.WriteLine(errorMessage);
                Console.WriteLine(Constants.PressAnyKeyToRetry);
                Console.Write(Constants.InputPrompt);
                Console.ReadKey();
                base.PromptForOptionValue(this);
            }
            else
            {
                base.HandleInput(input);
            }
        }
    }
}
