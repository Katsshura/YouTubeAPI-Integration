using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;

namespace YoutubeAPI.Integration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly string token = "teste";

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Hello = "Hello", World = "World" });
        }
    }
}