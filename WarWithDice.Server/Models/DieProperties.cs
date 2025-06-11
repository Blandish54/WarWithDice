using WarWithDice.Server.Models.Database;

namespace WarWithDice.Server.Models
{
    public class DieProperties
    {
        private string dieName { get; set; }

        private int dieRoll { get; set; }

        public string DieDescription => $"{dieName} - {dieRoll}";

        DieProperties(Die die) 
        {
            dieName = die.DieName;
            dieRoll = die.LastNumberRolled;    
        }

        DieProperties(string dieName, int dieRoll) 
        { 
            this.dieName = dieName;
            this.dieRoll = dieRoll; 

        }
    }
}
