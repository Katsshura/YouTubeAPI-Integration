using Google;
using Microsoft.AspNetCore.Mvc;
using System;
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
            if (oauthToken == null) { return BadRequest(); }
            try
            {
                var result = await this.homeService.GetPlaylistVideos(oauthToken, playlistType);
                return Ok(result);
            }
            catch (GoogleApiException ex)
            {
                if (ex.Error.Code == 401)
                {
                    return Unauthorized();
                }

                return StatusCode(500, ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("channel")]
        public async Task<ActionResult<ChannelEntity>> GetChannel([FromHeader] string oauthToken)
        {
            if (oauthToken == null) { return BadRequest(); }
            try
            {
                var result = await this.homeService.GetChannel(oauthToken);
                return Ok(result);
            }
            catch (GoogleApiException ex)
            {
                if (ex.Error.Code == 401)
                {
                    return Unauthorized();
                }

                return StatusCode(500, ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}