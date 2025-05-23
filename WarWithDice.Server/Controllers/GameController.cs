using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using WarWithDice.Server.Models;
using WarWithDice.Server.Models.Database;

namespace WarWithDice.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : Controller
    {
        CurrentGame currentGame;

        public GameController(CurrentGame currentGame)
        {
            this.currentGame = currentGame;
        }



        [Route("CreateDiceDeck")]
        [HttpGet]
        public IActionResult CreateDiceDeck()
        {
            currentGame.playerOneDiceDeck.Clear();
            currentGame.playerTwoDiceDeck.Clear();

            List<Die> mainDieBag = new List<Die>();

            int[] numberOfSides = { 4, 6, 8, 20 };
            string[] dieNames = { "d4", "d6", "d8", "d20" };

            var counter = 0;

            foreach (string dieName in dieNames)
            {
                for (int i = 0; i < 10; i++)
                {
                    var die = new Die(dieName, numberOfSides[counter]);

                    mainDieBag.Add(die);
                }
                counter++;
            }



            while (mainDieBag.Count > 0)
            {
                var topDie = mainDieBag[0];

                if (mainDieBag.Count % 2 == 0)
                {
                    mainDieBag.RemoveAt(0);
                    currentGame.playerOneDiceDeck.Add(topDie);
                }
                else
                {
                    mainDieBag.RemoveAt(0);
                    currentGame.playerTwoDiceDeck.Add(topDie);
                }
            }

            return Ok(currentGame);
        }


        [Route("PlayDiceRound")]
        [HttpGet]
        public IActionResult PlayDiceRound()
        {
            Random dieLocationOne = new Random();
            Random dieLocationTwo = new Random();

            bool isWar = false;

            var die1Location = dieLocationOne.Next(0, currentGame.playerOneDiceDeck.Count);
            var die2Location = dieLocationTwo.Next(0, currentGame.playerTwoDiceDeck.Count);

            Die playerOneDie = currentGame.playerOneDiceDeck[die1Location];
            Die playerTwoDie = currentGame.playerTwoDiceDeck[die2Location];

            playerOneDie.RollDice();
            playerTwoDie.RollDice();

            if (playerOneDie.LastNumberRolled > playerTwoDie.LastNumberRolled)
            {
                currentGame.playerTwoDiceDeck.RemoveAt(die2Location);
                currentGame.playerOneDiceDeck.Add(playerTwoDie);

                return Ok($"Player One Rolled a: {playerOneDie.DieName} for {playerOneDie.LastNumberRolled} and won the Round vs Player Two :{playerTwoDie.DieName} {playerTwoDie.LastNumberRolled}! Player One has {currentGame.playerOneDiceDeck.Count} dice. Player Two has {currentGame.playerTwoDiceDeck.Count} dice");
            }
            if (playerTwoDie.LastNumberRolled > playerOneDie.LastNumberRolled)
            {
                currentGame.playerOneDiceDeck.RemoveAt(die1Location);
                currentGame.playerTwoDiceDeck.Add(playerOneDie);

                return Ok($"Player Two Rolled a :{playerTwoDie.DieName} for {playerTwoDie.LastNumberRolled} and won the Round vs Player One Rolled a :{playerOneDie.DieName} for {playerOneDie.LastNumberRolled}! Player One has {currentGame.playerOneDiceDeck.Count} dice. Player Two has {currentGame.playerTwoDiceDeck.Count} dice");
            }
            else
            {
                isWar = true;
            }

            List<Die> playerOneWarBag = new List<Die>();
            List<Die> playerTwoWarBag = new List<Die>();

            List<Die> warPot = new List<Die>();
            
            while (isWar)
            {
                //build player one war deck
                while (currentGame.playerOneDiceDeck.Any() && playerOneWarBag.Count <= 4)
                {
                    var randomCardPosition = new Random().Next(0, currentGame.playerOneDiceDeck.Count);
                    var cardToAddToWarBag = currentGame.playerOneDiceDeck[randomCardPosition];
                    currentGame.playerOneDeck.RemoveAt(randomCardPosition);
                    playerOneWarBag.Add(cardToAddToWarBag);
                }

                //build player two war deck
                while (currentGame.playerTwoDiceDeck.Any() && playerTwoWarBag.Count <= 4)
                {
                    var randomCardPosition = new Random().Next(0, currentGame.playerTwoDiceDeck.Count);
                    var cardToAddToWarBag = currentGame.playerTwoDiceDeck[randomCardPosition];
                    currentGame.playerTwoDeck.RemoveAt(randomCardPosition);
                    playerTwoWarBag.Add(cardToAddToWarBag);
                }


                if (!playerOneWarBag.Any())
                {
                    return Ok();
                    //return game object
                    //Player one loses, game is over
                }
                
                else if (!playerTwoWarBag.Any())
                {
                    return Ok("Player One Wins!");
                    //return game object
                    //Player two loses, game is over
                }


                var playerOneWarDieValue = playerOneWarBag.First().RollDice();
                var playerTwoWarDieValue = playerTwoWarBag.First().RollDice();

                warPot.AddRange(playerOneWarBag);
                warPot.AddRange(playerTwoWarBag);

                playerOneWarBag.Clear();
                playerTwoWarBag.Clear();
                
                if (playerOneWarDieValue > playerTwoWarDieValue)
                {
                    currentGame.playerOneDiceDeck.AddRange(warPot);
                    warPot.Clear();
                    return Ok();
                    //return game object
                }
                else if(playerTwoWarDieValue > playerOneWarDieValue)
                {
                    return Ok();
                    currentGame.playerTwoDiceDeck = warPot;
                    warPot.Clear();
                    //return game object
                }
                else
                {
                    continue;
                    //start loop over
                }

                //check for war
                    //repeat loop if war
                //award dice and end turn

            }

            return NotFound("You should not have gotten to this point");
        }





        [Route("CreateDeck")]
        [HttpGet]
        public IActionResult CreateDeck()
        {
            currentGame.playerOneDeck.Clear();
            currentGame.playerTwoDeck.Clear();

            List<Card> deck = new List<Card>();

            string[] faceValues = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
            string[] cardSuits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            //When CardRank is set during the creation the 2s rank from 0 to 3 and so on for the rest



            foreach (string faceValue in faceValues)
            {
                foreach (var cardSuit in cardSuits)
                {
                    var cardRank = Array.IndexOf(faceValues, faceValue);

                    var card = new Card(cardRank, cardSuit, faceValue);

                    deck.Add(card);
                }
            }

            var random = new Random();
            int playerDealCounter = 1;

            while (deck.Count > 0)
            {
                int randomCardIndex = random.Next(deck.Count - 1);

                var randomCard = deck[randomCardIndex];

                if (playerDealCounter % 2 == 0)
                {
                    currentGame.playerTwoDeck.Add(randomCard);
                }
                else
                {
                    currentGame.playerOneDeck.Add(randomCard);
                }

                deck.RemoveAt(randomCardIndex);
                playerDealCounter++;
            }

            return Ok(currentGame);
        }

        //2. Change the message to user to show the dice values

        [Route("PlayRound")]
        [HttpGet]
        public IActionResult PlayRound()
        {
            Random random = new Random();

            var playerOneDiceRoll = random.Next(1, 7);

            var playerTwoDiceRoll = random.Next(1, 7);

            Card playerOneCard = currentGame.playerOneDeck.First();

            Card playerTwoCard = currentGame.playerTwoDeck.First();

            bool isWar = false;

            if (playerOneDiceRoll > playerTwoDiceRoll)
            {
                currentGame.playerTwoDeck.RemoveAt(0);

                currentGame.playerOneDeck.Add(playerTwoCard);
                currentGame.playerOneDeck.RemoveAt(0);
                currentGame.playerOneDeck.Add(playerOneCard);

                return Ok($@"Player One won the round, without War 
                {playerOneDiceRoll} vs. {playerTwoDiceRoll}
                Player One's deck has {currentGame.playerOneDeck.Count()} card(s)
                Player Two's deck has {currentGame.playerTwoDeck.Count()} card(s)");

            }

            if (playerTwoDiceRoll > playerOneDiceRoll)
            {
                currentGame.playerOneDeck.RemoveAt(0);

                currentGame.playerTwoDeck.Add(playerTwoCard);
                currentGame.playerTwoDeck.RemoveAt(0);
                currentGame.playerTwoDeck.Add(playerOneCard);


                return Ok($@"Player Two won the round, without War 
                {playerTwoDiceRoll} vs. {playerOneDiceRoll}
                Player One's deck has {currentGame.playerOneDeck.Count()} card(s)
                Player Two's deck has {currentGame.playerTwoDeck.Count()} card(s)");
            }
            else
            {
                isWar = true;
            }

            List<Card> warDeckPlayerOne = new List<Card>();
            List<Card> warDeckPlayerTwo = new List<Card>();

            currentGame.playerOneDeck.RemoveAt(0);
            warDeckPlayerOne.Add(playerOneCard);

            currentGame.playerTwoDeck.RemoveAt(0);
            warDeckPlayerTwo.Add(playerTwoCard);


            // If there is not a card at the index position it throws an error
            // check to see if the cards are there before trying to pull them

            //Look at React

            while (isWar)
            {
                int warCounter = 0;

                while (warCounter < 4)
                {
                    playerOneCard = currentGame.playerOneDeck[warCounter];
                    currentGame.playerOneDeck.RemoveAt(warCounter);
                    warDeckPlayerOne.Add(playerOneCard);
                    //Error when war counter reached 9, array said capacity 16
                    playerTwoCard = currentGame.playerTwoDeck[warCounter];
                    currentGame.playerTwoDeck.RemoveAt(warCounter);
                    warDeckPlayerTwo.Add(playerTwoCard);

                    warCounter++;
                }


                if (warDeckPlayerOne.Last().CardRank > warDeckPlayerTwo.Last().CardRank)
                {
                    currentGame.playerOneDeck.AddRange(warDeckPlayerOne);
                    currentGame.playerOneDeck.AddRange(warDeckPlayerTwo);

                    return Ok($@"Player One won the War with 
                    {warDeckPlayerTwo.Last().DisplayName} vs. {warDeckPlayerOne.Last().DisplayName}
                    Player One deck has {currentGame.playerOneDeck.Count} 
                    Player Two deck has {currentGame.playerTwoDeck.Count}");
                }

                if (playerTwoCard.CardRank > playerOneCard.CardRank)
                {
                    currentGame.playerTwoDeck.AddRange(warDeckPlayerOne);
                    currentGame.playerTwoDeck.AddRange(warDeckPlayerTwo);

                    return Ok($@"Player One won the War with 
                    {warDeckPlayerTwo.Last().DisplayName} vs. {warDeckPlayerOne.Last().DisplayName}
                    Player One deck has {currentGame.playerOneDeck.Count} 
                    Player Two deck has {currentGame.playerTwoDeck.Count}");
                }
                else
                {
                    isWar = true;
                }
            }

            return Ok();
        }
    }
}
