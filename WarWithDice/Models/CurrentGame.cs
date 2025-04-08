using System.Runtime.Serialization;

namespace WarWithDice.Server.Models
{
    public class CurrentGame
    {
        public List<Card> playerOneDeck = new List<Card>();
        
        
        public List <Card> playerTwoDeck = new List<Card>();
    }
}
