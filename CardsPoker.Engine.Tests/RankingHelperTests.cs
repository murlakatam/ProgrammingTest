using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsPoker.Engine.Entities;
using CardsPoker.Engine.Extensions;
using CardsPoker.Engine.Ranks;
using CardsPoker.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardsPoker.Engine.Tests
{
    [TestClass]
    public class RankingHelperTests
    {
        private RankingHelper context = new RankingHelper(new List<RankingChecker>
        {
            new FlushRankingChecker(),
            new PairRankingChecker(),
            new StraightFlushRankingChecker(),
            new StraightRankingChecker(),
            new HighCardRankingChecker()
        });

        [TestMethod]
        public void SucceedOn_DetermineRankType_StraightFlush()
        {
            /*Straight Flush(2 cards of sequential rank, same suit)*/
            var straightFlushCards = new List<Card>
            {
                new Card(CardRank.King, Suit.Diamonds),
                new Card(CardRank.Ace, Suit.Diamonds)
            };

            Assert.IsTrue(context.DefineRankType(straightFlushCards.OrderBy(x => x.CardValue).ToList()) ==
                          PokerRuleRank.StraightFlush);
            Assert.IsTrue(context.DefineRankType(straightFlushCards.OrderBy(x => x.CardValue).ToList()) ==
                          context.DefineRankType(straightFlushCards.OrderByDescending(x => x.CardValue).ToList()));
        }

        [TestMethod]
        public void SucceedOn_DetermineRankType_Flush()
        {
            /* Flush(2 cards , same suit)*/
            var straightFlushCards = new List<Card>
            {
                new Card(CardRank.Two, Suit.Diamonds),
                new Card(CardRank.Ace, Suit.Diamonds)
            };

            Assert.IsTrue(context.DefineRankType(straightFlushCards.OrderBy(x => x.CardValue).ToList()) ==
                          PokerRuleRank.Flush);
            Assert.IsTrue(context.DefineRankType(straightFlushCards.OrderBy(x => x.CardValue).ToList()) ==
                          context.DefineRankType(straightFlushCards.OrderByDescending(x => x.CardValue).ToList()));
        }

        [TestMethod]
        public void SucceedOn_DetermineRankType_Straight()
        {
            /* Straight(2 cards of sequential rank, different suit)*/
            var straightFlushCards = new List<Card>
            {
                new Card(CardRank.King, Suit.Clubs),
                new Card(CardRank.Ace, Suit.Diamonds)
            };

            Assert.IsTrue(context.DefineRankType(straightFlushCards.OrderBy(x => x.CardValue).ToList()) ==
                          PokerRuleRank.Straight);
            Assert.IsTrue(context.DefineRankType(straightFlushCards.OrderBy(x => x.CardValue).ToList()) ==
                          context.DefineRankType(straightFlushCards.OrderByDescending(x => x.CardValue).ToList()));
        }

        [TestMethod]
        public void SucceedOn_DetermineRankType_Straight2()
        {
            /* Straight(2 cards of sequential rank, different suit)*/
            var straightFlushCards = new List<Card>
            {
                new Card(CardRank.Six, Suit.Diamonds),
                new Card(CardRank.Five, Suit.Spades)
            };

            var test = context.DefineRankType(straightFlushCards.OrderBy(x => x.CardValue).ToList());
            Assert.IsTrue(test == PokerRuleRank.Straight);
            Assert.IsTrue(context.DefineRankType(straightFlushCards.OrderBy(x => x.CardValue).ToList()) ==
                          context.DefineRankType(straightFlushCards.OrderByDescending(x => x.CardValue).ToList()));
        }

        [TestMethod]
        public void SucceedOn_DetermineRankType_Pair()
        {
            /* Straight(2 cards of sequential rank, different suit)*/
            var paircards1 = new List<Card>
            {
                new Card(CardRank.King, Suit.Clubs),
                new Card(CardRank.King, Suit.Diamonds)
            };

            var paircards2 = new List<Card>
            {
                new Card(CardRank.Queen, Suit.Hearts),
                new Card(CardRank.Queen, Suit.Spades)
            };

            Assert.IsTrue(context.DefineRankType(paircards1) == PokerRuleRank.OnePair);
            Assert.IsTrue(context.DefineRankType(paircards1) == context.DefineRankType(paircards2));
        }

        [TestMethod]
        public void SucceedOn_DetermineRankType_HighCard()
        {
            /*
            *   * Individual cards are ranked A (highest), K, Q, J, 10, 9, 8, 7, 6, 5, 4, 3, 2 (lowest).
            * Suit order (strongest to weakest): Spades, Clubs, Hearts, Diamonds
            *     Since there were no priorities for above criteria I am assuming suits are stronger than ranks. So 2 Spades is higher than Ace Diamonds
            */
            var highCards1 = new List<Card>
            {
                new Card(CardRank.Two, Suit.Spades),
                new Card(CardRank.Ace, Suit.Diamonds)
            };

            var highCards2 = new List<Card>
            {
                new Card(CardRank.Two, Suit.Hearts),
                new Card(CardRank.Four, Suit.Diamonds)
            };

            Assert.IsTrue(context.DefineRankType(highCards1) == PokerRuleRank.HighCard);
            Assert.IsTrue(context.DefineRankType(highCards2) == PokerRuleRank.HighCard);
            Assert.IsTrue(highCards1.GetHighestCard().Equals(new Card(CardRank.Two, Suit.Spades)));
            Assert.IsTrue(highCards2.GetHighestCard().Equals(new Card(CardRank.Two, Suit.Hearts)));
        }


        [TestMethod]
        public void SucceedOn_GetExtraRankValueBasedOnPokerRankingRule_StraightFlush()
        {
            /*Straight Flush(2 cards of sequential rank, same suit)*/
            var straightFlushCards = new List<Card>
            {
                new Card(CardRank.King, Suit.Diamonds),
                new Card(CardRank.Ace, Suit.Diamonds)
            };

            var straightFlushCards2 = new List<Card>
            {
                new Card(CardRank.Queen, Suit.Diamonds),
                new Card(CardRank.King, Suit.Diamonds)
            };

            Assert.IsTrue(context.DefineRankType(straightFlushCards) == PokerRuleRank.StraightFlush);
            Assert.IsTrue(context.DefineRankType(straightFlushCards2) == PokerRuleRank.StraightFlush);
            //so we have a situation here when both ranks are straight flush, but actually the second set is weaker. 
            //i have introduced reversed logic so that the smaller value produced by the following method - the higher the rank
            Assert.IsTrue(context.GetExtraRankValueBasedOnPokerRankingRule(straightFlushCards, PokerRuleRank.StraightFlush) <
                          context.GetExtraRankValueBasedOnPokerRankingRule(straightFlushCards2, PokerRuleRank.StraightFlush));
        }

        [TestMethod]
        public void SucceedOn_GetExtraRankValueBasedOnPokerRankingRule_Flush()
        {
            /* Flush(2 cards , same suit)*/
            var flushCards = new List<Card>
            {
                new Card(CardRank.King, Suit.Diamonds),
                new Card(CardRank.Two, Suit.Diamonds)
            };

            var flushCards2 = new List<Card>
            {
                new Card(CardRank.Two, Suit.Diamonds),
                new Card(CardRank.Seven, Suit.Diamonds)
            };

            Assert.IsTrue(context.DefineRankType(flushCards) == PokerRuleRank.Flush);
            Assert.IsTrue(context.DefineRankType(flushCards2) == PokerRuleRank.Flush);
            //so we have a situation here when both ranks are straight flush, but actually the second set is weaker. 
            //i have introduced reversed logic so that the smaller value produced by the following method - the higher the rank
            Assert.IsTrue(context.GetExtraRankValueBasedOnPokerRankingRule(flushCards, PokerRuleRank.Flush) <
                          context.GetExtraRankValueBasedOnPokerRankingRule(flushCards2, PokerRuleRank.Flush));
        }

        [TestMethod]
        public void SucceedOn_GetExtraRankValueBasedOnPokerRankingRule_Straight()
        {
            /* Straight(2 cards of sequential rank, different suit)*/
            var straightCards = new List<Card>
            {
                new Card(CardRank.King, Suit.Clubs),
                new Card(CardRank.Ace, Suit.Diamonds)
            };

            var straightCards2 = new List<Card>
            {
                new Card(CardRank.Queen, Suit.Hearts),
                new Card(CardRank.King, Suit.Diamonds)
            };

            Assert.IsTrue(context.DefineRankType(straightCards) == PokerRuleRank.Straight);
            Assert.IsTrue(context.DefineRankType(straightCards2) == PokerRuleRank.Straight);
            //so we have a situation here when both ranks are straight flush, but actually the second set is weaker. 
            //i have introduced reversed logic so that the smaller value produced by the following method - the higher the rank
            Assert.IsTrue(context.GetExtraRankValueBasedOnPokerRankingRule(straightCards, PokerRuleRank.StraightFlush) <
                          context.GetExtraRankValueBasedOnPokerRankingRule(straightCards2, PokerRuleRank.StraightFlush));
        }

        [TestMethod]
        public void SucceedOn_GetExtraRankValueBasedOnPokerRankingRule_HighHand()
        {
            /*
            *   * Individual cards are ranked A (highest), K, Q, J, 10, 9, 8, 7, 6, 5, 4, 3, 2 (lowest).
            * Suit order (strongest to weakest): Spades, Clubs, Hearts, Diamonds
            *     Since there were no priorities for above criteria I am assuming suits are stronger than ranks. So 2 Spades is higher than Ace Diamonds
            */
            var highHandCards = new List<Card>
            {
                new Card(CardRank.Two, Suit.Hearts),
                new Card(CardRank.Ace, Suit.Diamonds)
            };

            var highHandCards2 = new List<Card>
            {
                new Card(CardRank.Jack, Suit.Clubs),
                new Card(CardRank.King, Suit.Diamonds)
            };

            Assert.IsTrue(context.DefineRankType(highHandCards) == PokerRuleRank.HighCard);
            Assert.IsTrue(context.DefineRankType(highHandCards2) == PokerRuleRank.HighCard);
            //so we have a situation here when both ranks are straight flush, but actually the second set is stronger (King of Clubs). 
            //i have introduced reversed logic so that the smaller value produced by the following method - the higher the rank
            Assert.IsTrue(context.GetExtraRankValueBasedOnPokerRankingRule(highHandCards, PokerRuleRank.HighCard) >
                          context.GetExtraRankValueBasedOnPokerRankingRule(highHandCards2, PokerRuleRank.HighCard));
        }
    }
}
