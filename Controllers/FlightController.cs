using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using midterm_SE4458.Model;
using Repository;

namespace Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    
    public class FlightController : ControllerBase
    {

        private IConfiguration _configuration;
        public FlightController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetFlights([FromQuery] TicketQuery modal)
        {
            var fligthRepo = new FlightsRepository();
            var resp = fligthRepo.GetFlights(modal);
            return Ok(resp);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateFlight([FromBody] Flight modal)
        {
            var fligthRepo = new FlightsRepository();
            var resp = fligthRepo.CreateFlight(modal);
            return Ok(resp);
        }

        [HttpPost, Authorize]
        public IActionResult BuyTicket([FromBody] BuyTicket modal)
        {
            var fligthRepo = new FlightsRepository();
            var attendantRepository = new AttendantRepository(_configuration);

            var token = TokenManager.GetToken(Request);

            var attendant = attendantRepository.GetAttendantByToken(token);
            if (attendant != null)
            {
                var response = fligthRepo.BuyTicket(modal, attendant);
                return Ok(response);
            }
            else
            {
                return Ok("Passenger token is not valid");
            }
        }
    }
}