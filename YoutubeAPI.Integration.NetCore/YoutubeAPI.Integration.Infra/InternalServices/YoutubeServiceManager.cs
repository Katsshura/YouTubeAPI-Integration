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
        private readonly IYoutubeRepository youtubeRepository;

        public YoutubeServiceManager(IYoutubeRepository youtubeRepository)
        {
            this.youtubeRepository = youtubeRepository;
        }

        public async Task<List<VideoEntity>> GetPlaylistVideos(string oauthToken, PlaylistType playlist)
        {
            List<VideoEntity> videos = new List<VideoEntity>();
            var rawVideos = await youtubeRepository.GetPlaylistVideos(oauthToken, playlist);

            if (rawVideos != null)
            {
                rawVideos.ForEach(item => videos.Add(this.Map(item, oauthToken)));
            }

            return videos;
        }

        public async Task<ChannelEntity> GetChannel(string oauthToken)
        {
            var rawChannels = await this.youtubeRepository.GetChannel(oauthToken);
            return this.Map(rawChannels.FirstOrDefault());
        }

        private VideoEntity Map(PlaylistItem item, string oauthToken)
        {
            var videoId = item?.ContentDetails?.VideoId;
            var video = this.youtubeRepository.GetVideo(oauthToken, videoId).FirstOrDefault();
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
                Link = string.Format("https://www.youtube.com/watch?v={0}", videoId)
            };
        }

        private ChannelEntity Map(Channel item)
        {
            return new ChannelEntity() {
                Name = item.Snippet.Title,
                Subscribers = item.Statistics.SubscriberCount,
                Videos = item.Statistics.VideoCount,
                Views = item.Statistics.ViewCount,
                Thumbnails = item.Snippet.Thumbnails
            };
        }

        private TimeSpan GetTimeSpanFromISO8601(string ISO)
        {
            try
            {
                return XmlConvert.ToTimeSpan(ISO);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
