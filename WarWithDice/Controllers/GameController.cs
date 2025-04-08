﻿using System;
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

        //2. Change the message to user to show the dice values

        [Route("PlayRound")]
        [HttpGet]
        public IActionResult PlayRound()
        {
            Random random = new Random();

            var playerOneDiceRoll = random.Next(0, 7);

            var playerTwoDiceRoll = random.Next(0, 7);

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
                {playerOneCard.DisplayName} vs. {playerTwoCard.DisplayName}
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
                {playerTwoCard.DisplayName} vs. {playerOneCard.DisplayName}
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
