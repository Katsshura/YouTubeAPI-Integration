using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeAPI.Integration.Domain.Entities.YouTube;
using YoutubeAPI.Integration.Domain.Enum;
using YoutubeAPI.Integration.Domain.Interfaces;

namespace YoutubeAPI.Integration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeApplicationService homeService;

        public HomeController(IHomeApplicationService homeService)
        {
            this.homeService = homeService;
        }

        [HttpGet("playlist")]
        public async Task<ActionResult<IEnumerable<VideoEntity>>> Get([FromQuery] PlaylistType playlistType, [FromHeader] string oauthToken)
        {
            if(oauthToken == null) { return BadRequest(); }

            var result = await this.homeService.GetPlaylistVideos(oauthToken, playlistType);
            return Ok(result);
        }

        [HttpGet("channel")]
        public async Task<ActionResult<ChannelEntity>> GetChannel([FromHeader] string oauthToken)
        {
            if (oauthToken == null) { return BadRequest(); }

            var result = await this.homeService.GetChannel(oauthToken);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult teste()
        {
            return Ok(new { A = "A" });
        }
    }
}