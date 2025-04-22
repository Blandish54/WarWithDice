using System.Runtime.CompilerServices;

namespace WarWithDice.Server.Models.Database
{
    /// Gonna need to be split into seperate classes at some point
    
    public class Card
    {
        public int CardId { get; set; }

        public int CardValue { get; set; }

        public string FaceValue { get; set; } = "";

        public string CardSuit { get; set; } = "";

        public int CardRank { get; set; }

        public string DisplayName => $"{FaceValue} of {CardSuit}";

        public Card()
        {

        }

        public Card(int CardId, string CardRank, string CardSuit, int CardValue)
        {
            CardId = CardId;

            CardRank = CardRank;

            CardSuit = CardSuit;

            CardValue = CardValue;
        }

        public Card(string faceValue, string cardSuit)
        {
            FaceValue = faceValue;
            CardSuit = cardSuit;
        }

    }
}
