using Microsoft.AspNetCore.Mvc;
using Piwerko.Api.Helpers;

namespace Piwerko.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Contact")]
    public class ContactController : Controller
    {
        [HttpPost("sendmail")]
        public IActionResult ConfirmNewPwd([FromBody] ContactUs contactUs)
        {
            if (contactUs.sendEmail())
            {
                return Ok("Twoje zgłoszenie zostało przyjęte! Zaczekaj na wiadomość od administratora.");
            }
            else
            {
                return BadRequest("Ups! Coś poszło nie tak. Spróbuj ponownie później.");
            }
        }
    }
}