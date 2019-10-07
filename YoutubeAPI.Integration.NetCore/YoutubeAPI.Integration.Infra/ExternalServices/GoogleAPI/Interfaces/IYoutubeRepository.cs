using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YoutubeAPI.Integration.Domain.Enum;

namespace YoutubeAPI.Integration.Infra.ExternalServices.GoogleAPI.Interfaces
{
    public interface IYoutubeRepository
    {
        Task<KeyValuePair<string, List<PlaylistItem>>> GetPlaylistVideos(string oauthToken, PlaylistType playlist, string pageToken, int prefetch);
        Task<List<Channel>> GetChannel(string oauthToken);
        List<Video> GetVideo(string oauthToken, string id);
    }
}
