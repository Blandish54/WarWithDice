namespace WarWithDice.Server.Models.Database
{
    public class GameStats
    {
        public int GameId { get; set; }

        public DateTime StartTime { get; set; } = DateTime.UtcNow;

        public DateTime? EndTime { get; set; }

        public int TotalRounds { get; set; }

        public required string Winner { get; set; }
    }
}
