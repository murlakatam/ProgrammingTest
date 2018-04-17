using System;
using System.Collections.Generic;
using System.Linq;
using CardsPoker.Engine.Entities;
using CardsPoker.Engine.Extensions;

namespace CardsPoker.Entities
{
    public class Deck
    {
        public static List<Card> DefaultCards;
        private static Random _rand = new Random(Guid.NewGuid().GetHashCode());
        public Queue<Card> _cards = new Queue<Card>();

        static Deck()
        {
            DefaultCards = new List<Card>();
            foreach (Suit suit in EntitiesExtensions.GetAllSuites())
            {
                foreach (CardRank type in EntitiesExtensions.GetAllCardTypes())
                {
                    DefaultCards.Add(new Card(type, suit));
                }
            }
        }

        public int Count => _cards.Count;

        public void Reset()
        {
            _cards = new Queue<Card>(DefaultCards);
        }

        public List<Card> TakeTop(int number)
        {
            if (number <= 0)
            {
                return new List<Card>();
            }

            if (number < _cards.Count)
            {
                return GetTopCards(number).ToList();
            }

            var result = _cards.ToList();
            _cards = new Queue<Card>();

            return result;
        }

        private IEnumerable<Card> GetTopCards(int number)
        {
            for (int i = 0; i < number && _cards.Count > 0; i++)
            {
                yield return _cards.Dequeue();
            }
        }

        public void Shuffle()
        {
            if (_cards.Count == 0)
            {
                Reset();
            }

            var tempList = _cards.ToList();

            for (int n = _cards.Count - 1; n > 0; --n)
            {
                int randomIndex = _rand.Next(n + 1);
                var temp = tempList[n];
                tempList[n] = tempList[randomIndex];
                tempList[randomIndex] = temp;
            }

            _cards = new Queue<Card>(tempList);
        }

    }
}
