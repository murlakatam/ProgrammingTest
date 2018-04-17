using System;
using System.Collections.Generic;
using System.Linq;
using CardsPoker.Engine.Entities;

namespace CardsPoker.Engine.Ranks
{
    public class PairRankingChecker : RankingChecker
    {
        protected override PokerRuleRank GetPokerRuleRank()
        {
            return PokerRuleRank.OnePair;
        }

        public override bool CardsMatchPokerRankingRule(List<Card> cards)
        {
            if (cards.Count == 0)
                return false;

            var grouped = cards.GroupBy(x => x.Rank);
            return grouped.Any(x => x.Count() == 2);
        }

        public override List<Card> SelectWinningCards(List<Card> cards)
        {
            return cards.GroupBy(x => x.Rank).Where(x => x.Count() == 2).OrderBy(x => x.Key).FirstOrDefault().ToList();
        }
    }
}
