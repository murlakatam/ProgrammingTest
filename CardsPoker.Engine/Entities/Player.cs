using System;
using System.Collections.Generic;
using System.Linq;
using CardsPoker.Engine.Extensions;

namespace CardsPoker.Engine.Entities
{
    public class Player
    {
        public Player(int id, string name)
        {
            Id = id;
            Name = name;
        }

        private List<Card> cards = new List<Card>();

        internal List<Card> Cards => cards;

        public int Id { get; set; }

        public string Name { get; set; }
        public int Score { get; set; }

        public void ChangeCards(List<Card> dealerCards)
        {
            this.cards = dealerCards;
        }

        public Card GetHighestCard()
        {
            return this.cards.GetHighestCard(); //the smaller the card value = the higher the overall card rank
        }

        public void LogCards(Action<string> logger)
        {
            logger(string.Empty);
            logger($"{Name}: {String.Join(", ", Cards.OrderByDescending(x => x.Rank))}");
        }
    }
}
