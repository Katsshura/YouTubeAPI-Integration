using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google;
using Microsoft.AspNetCore.Mvc;
using YoutubeAPI.Integration.Domain.Entities.YouTube;
using YoutubeAPI.Integration.Domain.Enum;
using YoutubeAPI.Integration.Domain.Interfaces;

namespace YoutubeAPI.Integration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeApplicationService _homeService;

        public HomeController(IHomeApplicationService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet("playlist")]
        public async Task<ActionResult<IEnumerable<VideoEntity>>> Get([FromQuery] PlaylistType playlistType,
            [FromHeader] string oauthToken, [FromHeader] string pageToken, [FromHeader] int prefetch)
        {
            if (HasNullOrDefaultArgs(oauthToken, pageToken) || prefetch <= 0) { return BadRequest(); }
            try
            {
                var result = await _homeService.GetPlaylistVideos(oauthToken, playlistType, pageToken, prefetch);
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
            if (HasNullOrDefaultArgs(oauthToken)) { return BadRequest(); }
            try
            {
                var result = await _homeService.GetChannel(oauthToken);
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

        private bool HasNullOrDefaultArgs(params object[] args)
        {
            return args.Any(arg => arg == null);
        }
    }
}