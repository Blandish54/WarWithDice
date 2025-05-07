using System.Runtime.Serialization;
using WarWithDice.Server.Models.Database;

namespace WarWithDice.Server.Models
{
    public class CurrentGame
    {
        public List<Die> playerOneDiceDeck = new List<Die>();

        public List<Die> playerTwoDiceDeck = new List<Die>();

        public List<Card> playerOneDeck = new List<Card>();
        
        public List <Card> playerTwoDeck = new List<Card>();
    }
}
