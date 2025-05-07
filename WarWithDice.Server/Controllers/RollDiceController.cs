using Microsoft.AspNetCore.Mvc;

namespace WarWithDice.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RollDiceController : Controller
    {
        [HttpGet(Name = "RollDice")]
        public int Get()
        {
            int Test = 5;

            return Test;
        }
    }
}
