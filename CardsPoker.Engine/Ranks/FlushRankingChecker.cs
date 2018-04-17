using System;
using System.Collections.Generic;
using System.Linq;
using CardsPoker.Engine.Entities;

namespace CardsPoker.Engine.Ranks
{
    public class FlushRankingChecker : RankingChecker
    {
        protected override PokerRuleRank GetPokerRuleRank()
        {
            return PokerRuleRank.Flush;
        }

        public override bool CardsMatchPokerRankingRule(List<Card> cards)
        {
            if (cards.Count == 0)
                return false;

            var firstCardSuit = cards.First().Suit;

            return cards.All(x => x.Suit == firstCardSuit);
        }

        public override List<Card> SelectWinningCards(List<Card> cards)
        {
            return cards;
        }
    }
}
