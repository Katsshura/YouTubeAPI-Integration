using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeAPI.Integration.Domain.Entities.YouTube;
using YoutubeAPI.Integration.Domain.Enum;

namespace YoutubeAPI.Integration.Domain.Interfaces
{
    public interface IYoutubeServiceManager
    {
        Task<KeyValuePair<string, List<VideoEntity>>> GetPlaylistVideos(string oauthToken, PlaylistType playlist, string pageToken, int prefetch);
        Task<ChannelEntity> GetChannel(string oauthToken);
    }
}
