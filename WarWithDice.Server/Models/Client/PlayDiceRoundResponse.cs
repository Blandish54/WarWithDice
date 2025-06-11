using WarWithDice.Server.Models.Database;

namespace WarWithDice.Server.Models.Client

{
    public class PlayDiceRoundResponse
    {
        List<DieProperties> playerOneDiceProperties { get; set; }

        List<DieProperties> playerTwoDiceProperties { get; set; }

        public PlayDiceRoundResponse()
        {
            playerOneDiceProperties = new List<DieProperties>();
            playerTwoDiceProperties = new List<DieProperties>();
        }

        public string WhoWon { get; set; }

        public string RoundOutcome { get; set; }

        public int PlayerOneDiceTotal { get; set; }

        public int PlayerTwoDiceTotal { get; set; }



        public bool GameOver => PlayerOneDiceTotal == 0 || PlayerTwoDiceTotal == 0;
    }
}
