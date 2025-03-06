using System.Runtime.CompilerServices;

namespace WarWithDice.Models
{
    public class Card
    {
        //public int CardId { get; set; }

        public string FaceValue { get; set; }

        public string CardSuit { get; set; }

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

        //public List<Card> Cards { get; set; }

        //public static Random Random = new Random();

        //public static void Shuffle<Card>(this List<Card> cards)
        //{
        //    int n = cards.Count;
        //    while (n > 1)
        //    {
        //        n--;
        //        int k = Random.Next(n + 1);
        //        Card value = cards[k];
        //        cards[k] = cards[n];
        //        cards[n] = value;
        //    }
        //}

    }
}
