using System.Drawing;

namespace WarWithDice.Server.Models.ClientAPIs
{
    public class GameUserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }        

        public int LuckyNumber { get; set; }

        public string ColorSelection { get; set; }

        public string ColorCode { get; set; }
    }
}
