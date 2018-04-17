using System;
using System.Collections.Generic;
using System.Linq;
using CardsPoker.Engine.Entities;
using CardsPoker.Engine.Ranks;
using CardsPoker.Entities;

namespace CardsPoker.Engine
{
    public class GameEngine
    {
        public GameEngine()
        {
        }

        private readonly GameSettings _settings;
        private readonly Action<string> _logger;
        private Action consoleKeyPrompt;
        private List<(int rank, Player player)> _currentRoundRanks = new List<(int rank, Player player)>();

        private static Random _rand = new Random(Guid.NewGuid().GetHashCode());

        private static RankingHelper rankingHelper = new RankingHelper(new List<RankingChecker>
        {
            new FlushRankingChecker(),
            new PairRankingChecker(),
            new StraightFlushRankingChecker(),
            new StraightRankingChecker(), 
            new HighCardRankingChecker()
        });

        public List<Player> Players { get; private set; }
        public Deck Deck { get; }
        public virtual int CurrentRound { get; private set; }
        public virtual int RoundsToBePlayed { get; private set; }

        public GameEngine(GameSettings settings, Action<string> logger)
        {
            _settings = settings;
            _logger = logger;
            Deck = new Deck();
        }

        public void SetConsoleKeyPromptForPause(Action consoleKeyPrompt)
        {
            this.consoleKeyPrompt = consoleKeyPrompt;
        }

        private void SetupPlayers()
        {
            Players = new List<Player>();

            RandomizePlayersIfNeeded();
            for (int i = 0; i < _settings.PlayersAmount; i++)
            {
                Players.Add(new Player(i, $"{nameof(Player)} {i + 1}"));
            }
        }

        private void RandomizePlayersIfNeeded()
        {
            if (_settings.PlayersAmount == 0)
            {
                _settings.PlayersAmount = (uint) _rand.Next(_settings.MinAmountOfPlayers, _settings.MaxAmountOfPlayers);
            }
        }

        private void RandomizeRoundsIfNeeded()
        {
            if (_settings.RoundsAmount == 0)
            {
                _settings.RoundsAmount = (uint)_rand.Next(_settings.MinAmountOfRounds, _settings.MaxAmountOfRounds);
            }
        }

        public void SetupTheGame()
        {
            SetupPlayers();
            SetupRounds();
            CurrentRound = 0;
        }

        public bool CanPlay()
        {
            return CurrentRound < RoundsToBePlayed;
        }

        public void PlayRound()
        {
            ClearRoundState();

            CurrentRound++;

            _logger($"Playing Round #{CurrentRound} or {RoundsToBePlayed}");

            _logger(string.Empty);

            _logger("Shuffling the deck");
            Deck.Shuffle();
            _logger($"Deck has { Deck.Count } cards");
            _logger("Dealing cards to each player");

            foreach (var player in Players)
            {
                player.ChangeCards(Deck.TakeTop(_settings.PokerCards));
                player.LogCards(_logger);
                var rank = rankingHelper.DefineRankType(player);

                _logger("Defined hand rank: " + rank + (rank == PokerRuleRank.HighCard ? $" ({ player.GetHighestCard() } )" : string.Empty));
                _currentRoundRanks.Add((rank: (int)rank + rankingHelper.GetExtraRankValueBasedOnPokerRankingRule(player, rank), player: player));
            }

            

            var groupedByRankPlayersToDefineScore = _currentRoundRanks.GroupBy(x => x.rank, x => x.player).OrderByDescending(x => x.Key).ToList(); 
            //weakest players will have the highest rank value

            for (int i = 0; i < groupedByRankPlayersToDefineScore.Count; i++)
            {
                foreach (var player in groupedByRankPlayersToDefineScore[i])
                {
                    /*At the end of each round, each player is assigned a score(0 – weakest to
                    strongest x - 1(where x = number of players)).*/
                    player.Score += i;
                }
            }

            LogStats();

            consoleKeyPrompt();
        }

        private void LogStats()
        {
            _logger(string.Empty);
            _logger("Current Stats: ");
            var stats = Players.OrderByDescending(x => x.Score);
            _logger(string.Join("; ", stats.Select(x => $"{x.Name}: {x.Score} scores")));
            _logger(string.Empty);
        }

        private void ClearRoundState()
        {
            _logger(String.Empty);
            _currentRoundRanks.Clear();
        }

        private void SetupRounds()
        {
            RandomizeRoundsIfNeeded();
            RoundsToBePlayed = (int)_settings.RoundsAmount;
        }
    }
}
