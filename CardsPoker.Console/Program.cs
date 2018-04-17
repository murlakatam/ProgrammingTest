using System;
using System.Collections.Generic;
using CardsPocker.Common;
using CardsPoker.Console.Helpers;
using CardsPoker.Console.Menu;
using CardsPoker.Engine;

namespace CardsPoker.Console
{
    class Program
    {
        private static IConsole _console;
        private static IEnvironment _environment;
        private static GameSettings _gameSettings;
        private static GameEngine _gameEngine;

        static void Main(string[] args)
        {
            _environment = new Environment();
            _console = new Console();
            _gameSettings = new GameSettings();
            _gameEngine = new GameEngine(_gameSettings, _console.WriteLine);
            _gameEngine.SetConsoleKeyPromptForPause(() => _gameEngine.PromptForAKeyStrokeToContinue(_console));
            var gameSettingsChildOptions = new List<IMenuOption>
            {
                new IntegerMenuOption(Constants.MenuOptionsTitles.Players, $"Please enter amount of players ( between {_gameSettings.MinAmountOfPlayers} and {_gameSettings.MaxAmountOfPlayers} )",
                    input =>
                    {
                        return _gameSettings.ValidateInputAndAssignValueFor(g => g.PlayersAmount, input, _gameSettings.MinAmountOfPlayers, _gameSettings.MaxAmountOfPlayers);
                    }, _console),

                new IntegerMenuOption(Constants.MenuOptionsTitles.Rounds, $"Please enter amount of rounds ( between {_gameSettings.MinAmountOfRounds} and {_gameSettings.MaxAmountOfRounds} )",
                    input =>
                    {
                        return _gameSettings.ValidateInputAndAssignValueFor(g => g.RoundsAmount, input, _gameSettings.MinAmountOfRounds, _gameSettings.MaxAmountOfRounds);
                    }, _console),

                new ExitToMainMenuOption(Constants.MenuOptionsTitles.ExitToMainMenu, _console)

            };

            var mainMenuOptions = new List<IMenuOption>
            {
                new GameSettingsMenuOption(Constants.MenuOptionsTitles.GameSettings, gameSettingsChildOptions, _console),
                new GameMenuOption(Constants.MenuOptionsTitles.GameMenu, _gameSettings, _gameEngine, _console),
                new ExitMenuOption(Constants.MenuOptionsTitles.Exit, _console, _environment)
            };

            var mainTitleFormat = "{0} Card Poker Challenge";
            var mainMenuDetails =
                "This programming challenge is designed to see how you code, \r\nit should be able to an hour and include input validation and unit tests to prove your code will work as expected." +
                System.Environment.NewLine;

            var menu = new MainMenuOption(mainTitleFormat, mainMenuDetails, mainMenuOptions, _gameSettings, _gameEngine, _console);
            menu.RenderWithChildren(args);
        }

        
    }
}
