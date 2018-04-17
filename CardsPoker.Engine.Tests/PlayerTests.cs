using System;
using System.Collections.ObjectModel;
using System.Linq;
using CardsPoker.Engine.Entities;
using CardsPoker.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardsPoker.Engine.Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Player_GetHighestCard()
        {
            var deck = new Deck();
            deck.Reset();
            var context = new Player(0, "Test");
            context.ChangeCards(deck._cards.ToList());
            Assert.IsTrue(context.GetHighestCard().Equals(new Card(CardRank.Ace, Suit.Spades)));
        }

        
    }
}
