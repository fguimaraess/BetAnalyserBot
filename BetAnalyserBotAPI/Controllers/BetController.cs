using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetBetsToday()
        {
            return Ok(await _betLogic.GetBetsToday());
        }
    }
}
