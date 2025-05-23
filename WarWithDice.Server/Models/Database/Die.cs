using WarWithDice.Server.Controllers;

namespace WarWithDice.Server.Models.Database

//Create the dice roll method 

//Begin to make the comparison in game play use dice rolls

{
    public class Die
    {
        public int DieID;

        public int NumberOfSides { get; set; }

        public int LastNumberRolled { get; set; }

        public string DieName { get; set; }

        public Die(string dieName, int numberOfSides)
        {
            DieName = dieName;

            NumberOfSides = numberOfSides;
        }

        public int RollDice()
        {
            LastNumberRolled = new Random().Next(1, NumberOfSides + 1);

            return LastNumberRolled;
        }

    }
}
