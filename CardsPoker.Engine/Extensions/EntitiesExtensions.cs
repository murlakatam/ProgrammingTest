using System;
using System.Collections.Generic;
using System.Linq;
using CardsPoker.Engine.Entities;

namespace CardsPoker.Engine.Extensions
{
    public static class EntitiesExtensions
    {
        public static uint GetValue(this Suit suit)
        {
            return (uint)suit * 100; //this allows to compare
        }

        public static uint GetValue(this CardRank cardRank)
        {
            return (uint)cardRank;
        }

        public static IEnumerable<Suit> GetAllSuites()
        {
            return Enum.GetValues(typeof(Suit)).Cast<Suit>();
        }

        public static IEnumerable<CardRank> GetAllCardTypes()
        {
            return Enum.GetValues(typeof(CardRank)).Cast<CardRank>();
        }

        public static int MaxDeckSize()
        {
            return GetAllCardTypes().Count() * GetAllSuites().Count();
        }

        public static Card GetHighestCard(this List<Card> cards)
        {
            return cards.OrderBy(x => x.CardValue).FirstOrDefault();
        }
    }
}
