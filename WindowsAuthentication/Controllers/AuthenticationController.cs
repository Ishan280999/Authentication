using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WindowsAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        [Authorize]
        [HttpGet("protected")]
        public IActionResult Index()
        {
            var user = HttpContext.User.Identity.Name;
            return Ok($"{user} This endpoint is protected by windows authentication!");
        }
    }
}
