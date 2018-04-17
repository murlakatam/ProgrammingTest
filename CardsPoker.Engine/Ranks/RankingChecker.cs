
using System.Collections.Generic;
using CardsPoker.Engine.Entities;

namespace CardsPoker.Engine.Ranks
{
    public abstract class RankingChecker
    {
        public PokerRuleRank PokerRuleRank => GetPokerRuleRank();

        protected abstract PokerRuleRank GetPokerRuleRank();
        public abstract bool CardsMatchPokerRankingRule(List<Card> cards);

        public abstract List<Card> SelectWinningCards(List<Card> cards);
    }
}
