
namespace CardsPoker.Engine.Ranks
{
    public enum PokerRuleRank
    {
        None = 2000,
        StraightFlush = 1, 
        Flush = 415, 
        Straight = 829, //suit is not counted
        OnePair = 843, //suit is not counted
        HighCard = 857
    }
}
