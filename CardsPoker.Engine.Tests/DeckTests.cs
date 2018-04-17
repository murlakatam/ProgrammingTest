using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CardsPoker.Engine.Entities;
using CardsPoker.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardsPoker.Engine.Tests
{
    [TestClass]
    public class DeckTests
    {
        /*
             #### Feature: Shuffle Deck

            **As** The Dealer 

            **I want to** Shuffle the Deck

            **So that** the card sequence is different for each round

            **Scenario:** Shuffle Deck X Times

            Given it is the start if a new round

            And the game is not over

            And a deck of 52 cards

            When I shuffle the deck X time (s)

            Then the deck is in a different order each time


         */

        [TestMethod]
        public void Deck_Shuffle()
        {
            var context = new Deck();
            context.Reset(); //adds to queue. 

            Assert.IsTrue(context.Count == 52);
            Assert.IsTrue(context._cards.GroupBy(x => x.ToString(), x => x).All(x => x.Count() == 1)); //grouped by card value should have no repeats

            for (int i = 0; i <= 6; i++) //six rounds
            {
                var beforeShuffle = new ReadOnlyCollection<Card>(context._cards.ToList());
                context.Shuffle();
                var afterShuffle = new ReadOnlyCollection<Card>(context._cards.ToList());

                Assert.IsFalse(beforeShuffle.SequenceEqual(afterShuffle));
            }

        }


        [TestMethod]
        public void SuccedesOn_Deck_GetTop()
        {
            var context = new Deck();
            context._cards = new Queue<Card>(Deck.DefaultCards.OrderBy(x => x.CardValue)); //ordered cards 

            Assert.IsTrue(context.Count == 52);
            var twoFromTop = context.TakeTop(2);
            Assert.IsTrue(twoFromTop[0].Equals(new Card(CardRank.Ace, Suit.Spades)));
            Assert.IsTrue(twoFromTop[1].Equals(new Card(CardRank.King, Suit.Spades)));
        }


    }
}
