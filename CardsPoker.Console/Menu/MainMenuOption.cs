using System.Collections.Generic;
using CardsPocker.Common;
using CardsPoker.Engine;

namespace CardsPoker.Console.Menu
{
    public class MainMenuOption : MenuOption
    {
        internal readonly string _details;
        private readonly GameSettings _gameSettings;

        public MainMenuOption(string title, string details, List<IMenuOption> options, GameSettings gameSettings, GameEngine gameEngine, IConsole console) : base(title, console)
        {
            Console.SetupConsole();

            _details = details;
            _gameSettings = gameSettings;

            foreach (var option in options)
            {
                AddChildOption(option);
            }
        }

        public virtual void ShowHeaders()
        {
            Console.WriteLine(string.Format(this.Title, _gameSettings.PokerCards));
            if (!string.IsNullOrWhiteSpace(this._details))
            {
                Console.WriteLine(this._details);
            }
        }

        public override void RenderWithChildren(dynamic data)
        {
            Console.Clear();
            ShowHeaders();

            Console.WriteLine(Constants.StartOfMenu);
            Console.WriteCommandDescriptionLine();

            Console.WriteLine();
            base.RenderChildOptions();
            Console.WriteLine();
            Console.WriteLine(Constants.EndOfMenu);
            base.SelectOptionByUserInput();
            
        }

    }
}
