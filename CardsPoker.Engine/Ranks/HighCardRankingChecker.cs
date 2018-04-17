using System.Collections.Generic;
using System.Linq;
using CardsPoker.Engine.Entities;
using CardsPoker.Engine.Extensions;

namespace CardsPoker.Engine.Ranks
{
    public class HighCardRankingChecker : SequentialRankingChecker
    {
        protected override PokerRuleRank GetPokerRuleRank()
        {
            return PokerRuleRank.HighCard;
        }

        public override bool IsValidOrderedSequence(Card currentCard, Card nextCard)
        {
            //everything is valid from high hand perspective :)
            return true;
        }

        public override List<Card> SelectWinningCards(List<Card> cards)
        {
            return new List<Card> { cards.OrderBy(x => x.CardValue).FirstOrDefault() };
        }


        public override bool AdditionalCheck(List<Card> cards)
        {
            if (cards.Count == 2)
            {
                //in case we have only 2 cards we need, we need to check if we have different rank and suit and not in sequence
                var firstCard = cards[0];
                var secondCard = cards[1];
                return firstCard.Rank != secondCard.Rank 
                        && firstCard.Suit != secondCard.Suit 
                        && firstCard.Rank.GetValue() + 1 != secondCard.Rank.GetValue();
            }

            //we don't need to check this for more cards in play

            return true;
        }
    }
}
