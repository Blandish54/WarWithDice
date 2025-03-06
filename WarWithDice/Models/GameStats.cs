namespace WarWithDice.Models
{
    public class GameStats
    {
        public int GameId { get; set; }

        public DateTime StartTime { get; set; } = DateTime.UtcNow;

        public DateTime? EndTime { get; set; }

        public int TotalRounds { get; set; }

        public string Winner {  get; set; }
    }
}
