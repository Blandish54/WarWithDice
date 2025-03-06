using System.Runtime.Serialization;

namespace WarWithDice.Models
{

    /// I keep wanting to return these values in other models
    ///
    /// Probably wrong but let's discuss
    public class CurrentGame
    {
        [DataMember]
        public List<Card> playerOneDeck = new List<Card>();
        
        
        [DataMember]
        public List <Card> playerTwoDeck = new List<Card>();


    }
}
