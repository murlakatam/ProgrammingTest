using System;
using System.Collections.Generic;
using System.Linq;
using CardsPoker.Engine.Entities;
using CardsPoker.Engine.Extensions;

namespace CardsPoker.Engine.Ranks
{
    public class RankingHelper
    {
        private readonly List<RankingChecker> _rankingCheckers;

        public RankingHelper(List<RankingChecker> rankingCheckers)
        {
            _rankingCheckers = rankingCheckers.OrderBy(x => x.PokerRuleRank).ToList(); //we need this collection to be ordered to select first match with higher rank
        }

        public PokerRuleRank DefineRankType(Player player)
        {
            return DefineRankType(player.Cards);
        }


        public PokerRuleRank DefineRankType(List<Card> cards)
        {
            return _rankingCheckers.FirstOrDefault(x => x.CardsMatchPokerRankingRule(cards))?.PokerRuleRank ?? PokerRuleRank.None;
        }

        public int GetExtraRankValueBasedOnPokerRankingRule(Player player, PokerRuleRank rank)
        {
            return GetExtraRankValueBasedOnPokerRankingRule(player.Cards, rank);
        }

        public int GetExtraRankValueBasedOnPokerRankingRule(List<Card> cards, PokerRuleRank rank)
        {
            switch (rank)
            {
                case PokerRuleRank.StraightFlush: //We need to add value based on cards types to define who won in case both players have king and ace, but first player got spades and the other clubs, 
                    //or when both players have Straight Flush, but one has kind and ace, and the other 2 and 3. 
                case PokerRuleRank.Flush: //I assume if we have 2 cards with same suit for one player and 2 cards with weaker suit for another we need to handle this
                case PokerRuleRank.OnePair: //same goes here pairs can be of different value if one has pair of aces and another pair of 2s
                case PokerRuleRank.HighCard: //this will only select one card.
                    var winningCards = _rankingCheckers.FirstOrDefault(x => x.PokerRuleRank == rank).SelectWinningCards(cards);
                    return (int)winningCards.GetHighestCard().CardValue; 

                case PokerRuleRank.Straight:
                    return (int)cards.GetHighestCard().Rank; //we don't care about suit when it's Straight (2 cards of sequential rank, different suit)
            }

            return 0;
        }
    }
}
