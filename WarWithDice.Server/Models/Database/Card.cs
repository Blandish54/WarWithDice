using System.Runtime.CompilerServices;

namespace WarWithDice.Server.Models.Database
{
    /// Gonna need to be split into seperate classes at some point
    
    public class Card
    {
        public int CardId;

        public string FaceValue { get; set; } = "";

        public string CardSuit { get; set; } = "";

        public int CardRank { get; set; }

        public string DisplayName => $"{FaceValue} of {CardSuit}";

        public Card()
        {

        }

        public Card( int cardRank, string cardSuit, string faceValue)
        {
           
            CardRank = cardRank;

            CardSuit = cardSuit;
            
            FaceValue = faceValue;
        }
    }
}
