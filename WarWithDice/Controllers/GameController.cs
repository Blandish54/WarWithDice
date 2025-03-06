using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using WarWithDice.Models;

namespace WarWithDice.Controllers
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


        [Route("CreateDeck")]
        [HttpGet]
        public IActionResult CreateDeck()
        {
            currentGame.playerOneDeck.Clear();
            currentGame.playerTwoDeck.Clear();

            List<Card> deck = new List<Card>();

            string[] faceValues = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
            string[] cardSuits = { "Hearts", "Diamonds", "Clubs", "Spades" };

            foreach (string faceValue in faceValues)
            {
                foreach (var cardSuit in cardSuits)
                {
                    var card = new Card();

                    card.FaceValue = faceValue;
                    card.CardRank = Array.IndexOf(faceValues, faceValue);
                    card.CardSuit = cardSuit;

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

        //2. Text stash your list of things
        //3. Write the code to play a round
        //4. Return the winner and each players card as a message to the user


        //1. Write a list of things to do in the order to do them
        [Route("PlayRound")]
        [HttpGet]
        public IActionResult PlayRound()
        {
            //Both players play the card from the top of their deck and the cardRank
            Card playerOneCard = currentGame.playerOneDeck[0];

            var playerOneCardRank = currentGame.playerOneDeck[0].CardRank;
            var playerOneCardFace = currentGame.playerOneDeck[0].FaceValue;
            var playerOneCardSuit = currentGame.playerOneDeck[0].CardSuit;
            var playerOneCardDisplayName = currentGame.playerOneDeck[0].DisplayName;

            Card playerTwoCard = currentGame.playerTwoDeck[0];

            var playerTwoCardRank = currentGame.playerTwoDeck[0].CardRank;
            var playerCarTwodFace = currentGame.playerTwoDeck[0].FaceValue;
            var playerTwoCardSuit = currentGame.playerTwoDeck[0].CardSuit;
            var playerTwoCardDisplayName = currentGame.playerTwoDeck[0].DisplayName;

            //Compare the two values of the cards and determine the result of the round (winner/loser or War)
            bool isWar = false;

            string winnerMessage = ("");

            if (playerOneCardRank > playerTwoCardRank)
            {
                winnerMessage = ($"Player One won the hand a {playerOneCardDisplayName} vs. a {playerTwoCardDisplayName}");

                currentGame.playerOneDeck.Add(currentGame.playerOneDeck[0]);
                currentGame.playerOneDeck.Add(currentGame.playerTwoDeck[0]);
                currentGame.playerTwoDeck.RemoveAt(0);

            }
            
            if (playerTwoCardRank > playerOneCardRank)
            {
                winnerMessage = ($"Player Two won the hand a {playerTwoCardDisplayName} vs. a {playerOneCardDisplayName}");

                currentGame.playerTwoDeck.Add(currentGame.playerTwoDeck[0]);
                currentGame.playerTwoDeck.Add(currentGame.playerOneDeck[0]);
                currentGame.playerOneDeck.RemoveAt(0);
            }
            
            else
            {
                isWar = true;
            }
            
            List<Card> warDeckPlayerOne = new List<Card>();
            List<Card> warDeckPlayerTwo = new List<Card>();
            
            while (isWar)
            {
                int warCounter = 1;

                while (warCounter <= 5)
                {
                    playerOneCard = currentGame.playerOneDeck[warCounter];

                    playerOneCardRank = currentGame.playerOneDeck[warCounter].CardRank;
                    playerOneCardFace = currentGame.playerOneDeck[warCounter].FaceValue;
                    playerOneCardSuit = currentGame.playerOneDeck[warCounter].CardSuit;
                    playerOneCardDisplayName = currentGame.playerOneDeck[warCounter].DisplayName;

                    warDeckPlayerOne.Add(playerOneCard);

                    playerTwoCard = currentGame.playerTwoDeck[warCounter];

                    playerTwoCardRank = currentGame.playerTwoDeck[warCounter].CardRank;
                    playerCarTwodFace = currentGame.playerTwoDeck[warCounter].FaceValue;
                    playerTwoCardSuit = currentGame.playerTwoDeck[warCounter].CardSuit;
                    playerTwoCardDisplayName = currentGame.playerTwoDeck[warCounter].DisplayName;

                    warDeckPlayerTwo.Add(playerTwoCard);

                    warCounter++;
                }
                
                
                if (playerOneCardRank > playerTwoCardRank)
                {
                    foreach (Card card in warDeckPlayerOne)
                    {
                        currentGame.playerOneDeck.Add(card);
                    }
                    foreach (Card card in warDeckPlayerTwo)
                    {
                        currentGame.playerOneDeck.Add(card);
                    }

                    winnerMessage = ($"Player One won the hand a {playerOneCardDisplayName} vs. a {playerTwoCardDisplayName}");

                    isWar = false;
                }
                
                
                if (playerTwoCardRank > playerOneCardRank)
                {
                    foreach (Card card in warDeckPlayerTwo)
                    {
                        currentGame.playerTwoDeck.Add(card);
                    }
                    foreach (Card card in warDeckPlayerOne)
                    {
                        currentGame.playerTwoDeck.Add(card);
                    }

                    winnerMessage = ($"Player Two won the hand a {playerTwoCardDisplayName} vs. a {playerOneCardDisplayName}");

                    isWar = false;
                }
                else 
                {
                    isWar = true;
                }
            }

            return Ok(winnerMessage);//Return your result here
        }
    }
}
