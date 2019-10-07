﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YoutubeAPI.Integration.Domain.Entities.YouTube;
using YoutubeAPI.Integration.Domain.Enum;
using YoutubeAPI.Integration.Domain.Interfaces;

namespace YoutubeAPI.Integration.Application.Services
{
    public class HomeApplicationService : IHomeApplicationService
    {
        private readonly IYoutubeServiceManager youtubeServiceManager;

        public HomeApplicationService(IYoutubeServiceManager youtubeServiceManager)
        {
            this.youtubeServiceManager = youtubeServiceManager;
        }

        public Task<KeyValuePair<string, List<VideoEntity>>> GetPlaylistVideos(string oauthToken, PlaylistType playlist, string pageToken, int prefetch)
        {
            return this.youtubeServiceManager.GetPlaylistVideos(oauthToken, playlist, pageToken, prefetch);
        }

        public Task<ChannelEntity> GetChannel(string oauthToken)
        {
            return this.youtubeServiceManager.GetChannel(oauthToken);
        }
    }
}
