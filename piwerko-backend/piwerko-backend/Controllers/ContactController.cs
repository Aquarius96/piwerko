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
            return Ok(contactUs.sendEmail());
        }
    }
}