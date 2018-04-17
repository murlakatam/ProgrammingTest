using System;
using CardsPoker.Engine.Extensions;

namespace CardsPoker.Engine.Entities
{
    public class Card : IComparable<Card>
    {
        public Card(CardRank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public CardRank Rank { get; set; }
        public Suit Suit { get; set; }

        public uint CardValue => this.Suit.GetValue() + (uint)this.Rank;

        public int CompareTo(Card other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            if (this.CardValue == other.CardValue) return 0;
            if (this.CardValue < other.CardValue) return 1; //the smallest car value = the highest the rank
            if (this.CardValue > other.CardValue) return -1;
            return 0;
        }

        public override string ToString()
        {
            return $"{Rank} {Suit}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (obj is Card)
            {
                var card = (Card) obj;
                return card.Suit == this.Suit && card.Rank == this.Rank;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return CardValue.GetHashCode();
        }

    }
}
