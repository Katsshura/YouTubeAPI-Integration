using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeAPI.Integration.Domain.Entities.YouTube;
using YoutubeAPI.Integration.Domain.Enum;
using YoutubeAPI.Integration.Domain.Interfaces;

namespace YoutubeAPI.Integration.Application.Services
{
    public class HomeApplicationService : IHomeApplicationService
    {
        private readonly IYoutubeServiceManager _youtubeServiceManager;

        public HomeApplicationService(IYoutubeServiceManager youtubeServiceManager)
        {
            this._youtubeServiceManager = youtubeServiceManager;
        }

        public Task<KeyValuePair<string, List<VideoEntity>>> GetPlaylistVideos(string oauthToken, PlaylistType playlist, string pageToken, int prefetch)
        {
            return this._youtubeServiceManager.GetPlaylistVideos(oauthToken, playlist, pageToken, prefetch);
        }

        public Task<ChannelEntity> GetChannel(string oauthToken)
        {
            return this._youtubeServiceManager.GetChannel(oauthToken);
        }
    }
}
