using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using VSoccerDemo.Models;
using VSoccerDemo.Models.Dto;

namespace VSoccerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly BusinessLogic _bl;
        public EventController(IMemoryCache memoryCache)
        {
            _bl = new BusinessLogic(memoryCache);
        }

        /// <summary>
        /// Generise match sa kvotama
        /// </summary>
        /// <returns> Trenutni match sa kvotama za opkladu </returns>
        [HttpGet("currentMatch")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            Match result = _bl.GetCurrentMatch();
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Uplata tiketa na match
        /// </summary>
        /// <param name="value">Tiket sa opkladama i korisnikom koji salje request</param>
        /// <returns>Da li je tiket uspjesno obradjen</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] TicketDto value)
        {
            bool result = _bl.InsertTicket(value);
            if(result == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Generise rezultat matcha i obradjuje uplacene tikete
        /// </summary>
        /// <param name="payInOperator">ID usera koji salje request</param>
        /// <returns>Rezulat matcha sa dobitkom</returns>
        [HttpGet("result")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetResult(string payInOperator)
        {
            ResultDto? result = _bl.CalculateResult(payInOperator);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
