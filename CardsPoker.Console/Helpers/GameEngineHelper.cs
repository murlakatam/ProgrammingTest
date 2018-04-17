using System;
using CardsPocker.Common;
using CardsPoker.Engine;

namespace CardsPoker.Console.Helpers
{
    public static class GameEngineHelper
    {
        public static void PromptForAKeyStrokeToContinue(this GameEngine gameEngine, IConsole console)
        {
            if (console != null && gameEngine != null)
            {
                console.WriteLine();
                console.WriteLine(gameEngine.CurrentRound != gameEngine.RoundsToBePlayed
                    ? Constants.PressAnyKeyToContinue
                    : Constants.PressAnyKeyToEnd);
                console.Write(Constants.InputPrompt);
                console.ReadKey();
            }
        }
    }
}
