namespace CardsPocker.Common
{
    public static class Constants
    {
        public const string InputPrompt = "$> ";
        public const string CommandTitle = "Command";
        public const string DescriptionTitle = "Description";
        public const char Space = ' ';
        public const string StartOfMenu = "*****************************Main menu********************************";
        public const string EndOfMenu = "****************************End of menu*******************************";
        public const string StartOfSettingsMenu = "***************************Settings Menu******************************";
        public const string EndOfSettingsMenu = "***********************End of Settings Menu***************************";
        public const string ByeBye = "Bye bye";
        public const string UnhandledError = "Bad property type. We shouldn't be here. OMG.";
        public const string PressAnyKeyToRetry = "Press any key to retry...";
        public const string PressAnyKeyToContinue = "Press any key to start the next round...";
        public const string PressAnyKeyToEnd = "Press any key to end the game...";
        public const string InvalidInputOption = "Invalid input option. Please try again";
        public const string TitleIsRequried = "Title is required";

        public static class MenuOptionsTitles
        {
            public const string GameSettings = "Game settings";
            public const string GameMenu = "Play game";
            public const string Exit = "Exit";
            public const string ExitToMainMenu = "Exit to main menu";
            public const string Players = "Set amount of players";
            public const string Rounds = "Set amount of rounds";
        }
        
    }
}
