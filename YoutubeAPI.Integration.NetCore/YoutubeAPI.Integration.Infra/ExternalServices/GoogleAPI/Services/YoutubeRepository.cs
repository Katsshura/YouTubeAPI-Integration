using YoutubeAPI.Integration.Infra.ExternalServices.GoogleAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using YoutubeAPI.Integration.Domain.Enum;
using System.Threading.Tasks;

namespace YoutubeAPI.Integration.Infra.ExternalServices.GoogleAPI.Services
{
    public class YoutubeRepository : IYoutubeRepository
    {
        private static YouTubeService Instance { get; set; }

        public YoutubeRepository(IGoogleService<YouTubeService> googleService)
        {
            Instance = googleService.GetService();
        }

        public async Task<List<PlaylistItem>> GetPlaylistVideos(string oauthToken, PlaylistType playlist)
        {
            List<PlaylistItem> videos = new List<PlaylistItem>();

            var channelResponse = await this.GetChannelListRequest(oauthToken, "contentDetails").ExecuteAsync();
            var channel = channelResponse.Items.FirstOrDefault();
            var nextPageToken = "";

            while (nextPageToken != null)
            {
                var playListRequest = this.GetPlaylistRequest(oauthToken, "contentDetails", channel, playlist);
                playListRequest.PageToken = nextPageToken;
                try
                {
                    var response = playListRequest.Execute();
                    videos.AddRange(response.Items);
                    nextPageToken = response.NextPageToken;
                }
                catch(Exception)
                {
                    return null;
                }
            }
            return videos;
        }

        public async Task<List<Channel>> GetChannel(string oauthToken)
        {
            List<Channel> channels = new List<Channel>();
            var channelResponse = await this.GetChannelListRequest(oauthToken, "contentDetails,statistics,snippet").ExecuteAsync();
            var channelItems = channelResponse.Items;
            channels.AddRange(channelItems);
            return channels;
        }

        public List<Video> GetVideo(string oauthToken, string id)
        {
            var videoResponse = this.GetVideoRequest(oauthToken, "contentDetails,statistics,snippet", id).Execute();
            return videoResponse.Items.ToList();
        }

        private ChannelsResource.ListRequest GetChannelListRequest(string oauthToken, string part)
        {
            var channelListRequest = Instance.Channels.List(part);
            channelListRequest.OauthToken = oauthToken;
            channelListRequest.Mine = true;
            return channelListRequest;
        }

        private PlaylistItemsResource.ListRequest GetPlaylistRequest(string oauthToken, string part, Channel channel, PlaylistType playlist)
        {
            var playlistRequest = Instance.PlaylistItems.List(part);
            playlistRequest.PlaylistId = this.GetPlaylistID(channel, playlist);
            playlistRequest.OauthToken = oauthToken;
            return playlistRequest;
        }

        private VideosResource.ListRequest GetVideoRequest(string oauthToken, string part, string id)
        {
            var videoRequest = Instance.Videos.List(part);
            videoRequest.Id = id;
            videoRequest.OauthToken = oauthToken;
            return videoRequest;
        }

        private string GetPlaylistID(Channel channel, PlaylistType playlist)
        {
            switch (playlist)
            {
                case PlaylistType.Upload:
                    return channel.ContentDetails.RelatedPlaylists.Uploads;
                case PlaylistType.Like:
                    return channel.ContentDetails.RelatedPlaylists.Likes;
                case PlaylistType.Favorite:
                    return channel.ContentDetails.RelatedPlaylists.Favorites;
                case PlaylistType.WatchHistory:
                    return channel.ContentDetails.RelatedPlaylists.WatchHistory;
                case PlaylistType.WatchLater:
                    return channel.ContentDetails.RelatedPlaylists.WatchLater;
                default:
                    throw new Exception();
            }
        }
    }
}
