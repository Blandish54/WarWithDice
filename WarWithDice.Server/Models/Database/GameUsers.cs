using System.Numerics;

namespace WarWithDice.Server.Models.Database
{
    public class GameUsers
    {
        public int UserId { get; set; }

        public required string UserName { get; set; }

        public int GamesWon { get; set; }

        public int GamesLost { get; set; }

        public int TotalDiceRolls { get; set; }

        public int WarsWon { get; set; }

        public int WarsLost { get; set; }

        public int RoundsWon { get; set; }

        public int RoundsLost { get; set; }

        public int TotalGames => GamesWon + GamesLost;

        public double WinPercentage => TotalGames == 0 ? 0 : GamesWon / (double)TotalGames * 100;

        public int TotalWars => WarsWon + WarsLost;

        public double WarWinPercentage => TotalWars == 0 ? 0 : WarsWon / (double)TotalWars * 100;

        public int TotalRounds => RoundsWon + RoundsLost;

        public double RoundWinPercentage => TotalRounds == 0 ? 0 : RoundsWon / (double)TotalRounds * 100;

    }
}
