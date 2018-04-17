using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using CardsPoker.Engine.Extensions;

namespace CardsPoker.Engine
{
    public class GameSettings
    {
        public GameSettings()
        {
        }

        [DisplayName("Amount of players")]
        public virtual uint PlayersAmount { get; set; }
        [DisplayName("Amount of rounds")]
        public virtual uint RoundsAmount { get; set; }

        public int MinAmountOfPlayers => GetFromAppSettings(nameof(MinAmountOfPlayers), 2);

        public int MaxAmountOfPlayers => GetFromAppSettings(nameof(MaxAmountOfPlayers), 6);

        public int MinAmountOfRounds => GetFromAppSettings(nameof(MinAmountOfRounds), 2);

        public virtual int PokerCards => GetFromAppSettings(nameof(PokerCards), 2);

        public int MaxAmountOfRounds => GetFromAppSettings(nameof(MaxAmountOfRounds), 5);

        private int GetFromAppSettings(string configName, int defaultValue)
        {
            uint val;
            var value = ConfigurationManager.AppSettings[configName];
            if (string.IsNullOrWhiteSpace(value) || !uint.TryParse(value, out val))
            {
                return defaultValue;
            }
            return (int)val;
        }

        public (string message, bool validationResult) Validate()
        {
            if (MaxAmountOfRounds * MaxAmountOfPlayers * PokerCards > EntitiesExtensions.MaxDeckSize())
            {
                return (message: "Current game setup is wrong since amount of players multiplied by rounds, multiplied by cards in hand is bigger than standard deck size", validationResult: false);
            }

            if (MinAmountOfRounds > MaxAmountOfRounds)
            {
                return (message: "Min amount of rounds shoulbe be less than max amount of rounds", validationResult: false);
            }

            if (MinAmountOfPlayers > MaxAmountOfPlayers)
            {
                return (message: "Min amount of players shoulbe be less than max amount of players", validationResult: false);
            }

            if (PlayersAmount < MinAmountOfPlayers)
            {
                return (message: "Players amount can't be less than " + MinAmountOfPlayers, validationResult: false);
            }

            if (PlayersAmount > MaxAmountOfPlayers)
            {
                return (message: "Players amount can't be more than " + MaxAmountOfPlayers, validationResult: false);
            }

            if (RoundsAmount < MinAmountOfRounds)
            {
                return (message: "Players amount can't be less than " + MinAmountOfRounds, validationResult: false);
            }

            if (RoundsAmount > MaxAmountOfRounds)
            {
                return (message: "Players amount can't be more than " + MaxAmountOfRounds, validationResult: false);
            }

            return (message: String.Empty, validationResult: true);
        }
    }
}
