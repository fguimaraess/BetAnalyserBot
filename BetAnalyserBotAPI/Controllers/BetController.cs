using Domain;
using Microsoft.AspNetCore.Mvc;

namespace BetAnalyserBotAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BetController : ControllerBase
    {
        private readonly IBetLogic _betLogic;

        public BetController(IBetLogic betLogic)
        {
            _betLogic = betLogic;
        }

        [HttpGet("get-bets-today")]
        public IActionResult GetBetsToday()
        {
            return Ok(_betLogic.GetBetsToday());
        }
    }
}
