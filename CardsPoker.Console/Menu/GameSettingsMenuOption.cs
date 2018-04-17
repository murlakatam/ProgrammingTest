using System.Collections.Generic;
using CardsPocker.Common;

namespace CardsPoker.Console.Menu
{
    public class GameSettingsMenuOption : MenuOption
    {
        public GameSettingsMenuOption(string title, List<IMenuOption> options, IConsole console) : base(title, console)
        {
            foreach (var option in options)
            {
                AddChildOption(option);
            }
        }


        public override void RenderWithChildren(dynamic data)
        {
            Console.Clear();
            Console.WriteLine(Constants.StartOfSettingsMenu);
            Console.WriteCommandDescriptionLine();
            Console.WriteLine();
            RenderChildOptions();
            Console.WriteLine();
            Console.WriteLine(Constants.EndOfSettingsMenu);
            SelectOptionByUserInput();
        }

        
    }
}
