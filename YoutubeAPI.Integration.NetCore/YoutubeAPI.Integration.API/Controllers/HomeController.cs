using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using YoutubeAPI.Integration.Infra.ExternalServices.GoogleAPI.Services;

namespace YoutubeAPI.Integration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IGoogleService<YouTubeService> youtubeConnection;
        private readonly string token = "teste";

        public HomeController(IGoogleService<YouTubeService> youtubeConnection)
        {
            this.youtubeConnection = youtubeConnection;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Hello = "Hello", World = "World" });
        }
    }
}