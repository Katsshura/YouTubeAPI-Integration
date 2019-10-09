using YoutubeAPI.Integration.Domain.Interfaces;
using System.Linq;
using System.Collections.Generic;
using YoutubeAPI.Integration.Infra.ExternalServices.GoogleAPI.Interfaces;
using YoutubeAPI.Integration.Domain.Entities.YouTube;
using System.Threading.Tasks;
using YoutubeAPI.Integration.Domain.Enum;
using Google.Apis.YouTube.v3.Data;
using System.Xml;
using System;

namespace YoutubeAPI.Integration.Infra.InternalServices
{
    public class YoutubeServiceManager : IYoutubeServiceManager
    {
        private readonly IYoutubeRepository _youtubeRepository;

        public YoutubeServiceManager(IYoutubeRepository youtubeRepository)
        {
            this._youtubeRepository = youtubeRepository;
        }

        public async Task<KeyValuePair<string, List<VideoEntity>>> GetPlaylistVideos(string oauthToken, PlaylistType playlist, string pageToken, int prefetch)
        {
            List<VideoEntity> videos = new List<VideoEntity>();
            var rawVideos = await _youtubeRepository.GetPlaylistVideos(oauthToken, playlist, pageToken, prefetch);

            rawVideos.Value?.ForEach(item => videos.Add(this.Map(item, oauthToken)));

            return new KeyValuePair<string, List<VideoEntity>>(rawVideos.Key, videos);
        }

        public async Task<ChannelEntity> GetChannel(string oauthToken)
        {
            var rawChannels = await this._youtubeRepository.GetChannel(oauthToken);
            return this.Map(rawChannels.FirstOrDefault());
        }

        private VideoEntity Map(PlaylistItem item, string oauthToken)
        {
            var videoId = item?.ContentDetails?.VideoId;
            var video = this._youtubeRepository.GetVideo(oauthToken, videoId).FirstOrDefault();
            return new VideoEntity() {
                ChannelName = video?.Snippet?.ChannelTitle,
                Title = video?.Snippet?.Title,
                Duration = this.GetTimeSpanFromISO8601(video?.ContentDetails?.Duration),
                Comments = video?.Statistics.CommentCount,
                Likes = video?.Statistics.LikeCount,
                Dislikes = video?.Statistics.DislikeCount,
                Favorites = video?.Statistics.FavoriteCount,
                Views = video?.Statistics.ViewCount,
                Thumbnails = video?.Snippet?.Thumbnails,
                Link = $"https://www.youtube.com/watch?v={videoId}"
            };
        }

        private ChannelEntity Map(Channel item)
        {
            return new ChannelEntity(item.Snippet.Title) {
                Subscribers = item.Statistics.SubscriberCount,
                Videos = item.Statistics.VideoCount,
                Views = item.Statistics.ViewCount,
                Thumbnails = item.Snippet.Thumbnails
            };
        }

        private TimeSpan GetTimeSpanFromISO8601(string iso)
        {
            try
            {
                return XmlConvert.ToTimeSpan(iso);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
