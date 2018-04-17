using System;
using System.Collections.Generic;
using System.Linq;
using CardsPoker.Engine.Entities;

namespace CardsPoker.Engine.Ranks
{
    public abstract class SequentialRankingChecker : RankingChecker
    {
        protected override PokerRuleRank GetPokerRuleRank()
        {
            return PokerRuleRank.None;
        }

        public override bool CardsMatchPokerRankingRule(List<Card> unorderedCards)
        {
            if (unorderedCards.Count == 0)
                return false;

            var cards = unorderedCards.OrderBy(x => (int)x.Rank).ToList();
            

            for (int i = 0; i < cards.Count - 1; i++)
            {
                var card = cards[i];
                var nextCard = cards.ElementAtOrDefault(i + 1);
                if (!IsValidOrderedSequence(card, nextCard))
                {
                    return false;
                }
            }

            if (!AdditionalCheck(cards))
            {
                return false;
            }


            return true;
        }

        public abstract bool IsValidOrderedSequence(Card currentCard, Card nextCard);

        public virtual bool AdditionalCheck(List<Card> cards)
        {
            return true;
        }
    }
}
