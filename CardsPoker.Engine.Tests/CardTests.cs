using System;
using CardsPoker.Engine.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardsPoker.Engine.Tests
{
    [TestClass]
    public class CardTests
    {
        /*
         *   * Individual cards are ranked A (highest), K, Q, J, 10, 9, 8, 7, 6, 5, 4, 3, 2 (lowest).
             * Suit order (strongest to weakest): Spades, Clubs, Hearts, Diamonds
         *     Since there were no priorities for above criteria I am assuming suits are stronger than ranks. So 2 Spades is higher than Ace Diamonds
         */

        [TestMethod]
        public void Card_Compare()
        {
            Assert.IsTrue(new Card(CardRank.Ace, Suit.Clubs).Equals(new Card(CardRank.Ace, Suit.Clubs)));
            Assert.IsTrue(new Card(CardRank.Ace, Suit.Diamonds).CompareTo(new Card(CardRank.Ace, Suit.Clubs)) == -1);
            Assert.IsTrue(new Card(CardRank.Ace, Suit.Clubs).CompareTo(new Card(CardRank.Ace, Suit.Diamonds)) == 1);
        }

        [TestMethod]
        public void Card_ToString()
        {
            Assert.IsTrue(new Card(CardRank.Ace, Suit.Clubs).ToString() == "Ace Clubs");
        }
    }
}
