using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WarWithDice.Server.Models.Database
{
    public class WarResolution
    {
        public int WarId { get; set; }

        public int GameId { get; set; }

        public int RoundId { get; set; }

        public List<int> PlayerCardsPlayed { get; set; } = new List<int>();

        public List<int> ComputerCardsPlayed { get; set; } = new List<int>();

        public required string WarWinner { get; set; }
    }
}
