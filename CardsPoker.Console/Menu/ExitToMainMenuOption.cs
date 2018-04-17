using CardsPocker.Common;

namespace CardsPoker.Console.Menu
{
    public class ExitToMainMenuOption : MenuOption
    {
        public ExitToMainMenuOption(string title, IConsole console) : base(title, console)
        {
            HasNoInput = true;
        }

        public override void RenderWithChildren(dynamic data)
        {
            IMenuOption cmd = this;
            MainMenuOption mainMenu = cmd.Parent as MainMenuOption;
            while (mainMenu == null)
            {
                mainMenu = cmd.Parent as MainMenuOption;
                cmd = cmd.Parent;
            }

            mainMenu.RenderWithChildren(string.Empty);
        }
    }
}
