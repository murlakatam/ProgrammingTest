using CardsPocker.Common;

namespace CardsPoker.Console.Menu
{
    public class ExitMenuOption : MenuOption
    {
        private readonly IEnvironment _environment;

        public ExitMenuOption(string title, IConsole console, IEnvironment environment) : base(title, console)
        {
            _environment = environment;
            HasNoInput = true;
        }

        public override void RenderWithChildren(dynamic data)
        {
            Console.WriteLine(Constants.ByeBye);
            _environment.Exit();
        }
    }
}
