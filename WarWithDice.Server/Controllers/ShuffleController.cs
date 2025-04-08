//using Microsoft.AspNetCore.Mvc;
//using WarWithDice.Models;
//using WarWithDice.Controllers;
//using System.Reflection.Metadata.Ecma335;



////I want to return the 2 decks post shuffle using my local method DealCards in the DeckToShuffle model

//namespace WarWithDice.Controllers
//{
   
//    [ApiController]
//    [Route("[controller]")]
//    public class ShuffleController : Controller
//    {
//        private readonly CurrentGame currentGame;

//        public ShuffleController(CurrentGame currentGame)
//        {
//            this.currentGame = currentGame; 
//        }



//        private static Random random = new Random();
        
//        public static IList<Card> Shuffle(IList<Card> items)
//        {
//            for (int i = 0; i < items.Count - 1; i++)
//            {
//                int pos = random.Next(i, items.Count);
//                Card temp = items[i];
//                items[i] = items[pos];
//                items[pos] = temp;
//            }
//            DeckToShuffle.AddCards(items);
//        }
//    }
//}



   