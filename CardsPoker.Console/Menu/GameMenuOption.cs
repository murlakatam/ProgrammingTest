using CardsPocker.Common;
using CardsPoker.Engine;

namespace CardsPoker.Console.Menu
{
    public class GameMenuOption : MenuOption
    {
        private readonly GameSettings _gameSettings;
        private readonly GameEngine _gameEngine;

        public GameMenuOption(string title, GameSettings gameSettings, GameEngine gameEngine, IConsole console) : base(title, console)
        {
            HasNoInput = true;
            _gameSettings = gameSettings;
            _gameEngine = gameEngine;
        }

        public override void RenderWithChildren(dynamic data)
        {
            Console.Clear();
            _gameEngine.SetupTheGame();
            Console.WriteLine($"Starting game of { _gameSettings.RoundsAmount } rounds for { _gameSettings.PlayersAmount } players");
            while (_gameEngine.CanPlay())
            {
                _gameEngine.PlayRound();
                Console.Clear();
            }

            Parent.RenderWithChildren(string.Empty);
        }
    }
}
