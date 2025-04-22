using System.Runtime.Serialization;
using WarWithDice.Server.Models.Database;

namespace WarWithDice.Server.Models
{
    public class CurrentGame
    {
        public List<Card> playerOneDeck = new List<Card>();
        
        
        public List <Card> playerTwoDeck = new List<Card>();
    }
}
