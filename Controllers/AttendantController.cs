using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Controllers
{
   [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    
    public class AttendantController : ControllerBase
    {
       private  IConfiguration _configuration;
        public AttendantController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Login modal)
        {
            var attendantRepository = new AttendantRepository(_configuration);
            var attendant = attendantRepository.GetAttendantLogin(modal);
            return Ok(attendant);
        }

        [HttpPost]
        public IActionResult SignUp([FromBody]SignUp modal){
            var attendantRepository = new AttendantRepository(_configuration);
            var attendant = attendantRepository.CreateAttendant(modal);
            return Ok(attendant);
        }
    }
}