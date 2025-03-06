namespace WarWithDice.Models
{
    public class RoundStats
    {
        public int RoundId { get; set; }

        public int GameId { get; set; }

        public int RoundNumber { get; set; }

        public int PlayerRoll { get; set; }

        public int ComputerRoll { get; set; }

        public string RoundResult { get; set; }

        public bool IsWar {  get; set; }

    }
}
