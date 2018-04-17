using System.Collections.Generic;
using System.Linq;
using CardsPoker.Engine.Entities;
using CardsPoker.Engine.Extensions;

namespace CardsPoker.Engine.Ranks
{
    public class StraightRankingChecker : SequentialRankingChecker
    {
        protected override PokerRuleRank GetPokerRuleRank()
        {
            return PokerRuleRank.Straight;
        }

        public override bool IsValidOrderedSequence(Card currentCard, Card nextCard)
        {
            var nextRank = nextCard.Rank.GetValue() - 1;
            var currentRank = currentCard.Rank.GetValue();
            return nextRank == currentRank;
        }

        public override bool AdditionalCheck(List<Card> cards)
        {
            var firstCardSuit = cards.First().Suit;
            return cards.Any(x => x.Suit != firstCardSuit);
        }

        public override List<Card> SelectWinningCards(List<Card> cards)
        {
            return cards;
        }
    }
}
