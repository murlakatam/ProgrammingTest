using System.Collections.Generic;
using System.Linq;
using CardsPoker.Engine.Entities;
using CardsPoker.Engine.Extensions;

namespace CardsPoker.Engine.Ranks
{
    public class StraightFlushRankingChecker : SequentialRankingChecker
    {
        protected override PokerRuleRank GetPokerRuleRank()
        {
            return PokerRuleRank.StraightFlush;
        }

        public override bool IsValidOrderedSequence(Card currentCard, Card nextCard)
        {
            var nextRank = nextCard.Rank.GetValue() - 1;
            var currentRank = currentCard.Rank.GetValue();
            return nextRank == currentRank;
        }

        public override bool AdditionalCheck(List<Card> cards)
        {
            var suit = cards.First().Suit;
            return cards.All(x => x.Suit == suit);
        }

        public override List<Card> SelectWinningCards(List<Card> cards)
        {
            return cards;
        }
    }
}
