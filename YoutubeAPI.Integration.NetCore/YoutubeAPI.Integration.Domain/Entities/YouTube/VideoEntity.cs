using System;
using System.Collections.Generic;

namespace YoutubeAPI.Integration.Domain.Entities.YouTube
{
    public class VideoEntity
    {
        public string Title { get; set; }
        public string ChannelName { get; set; }
        public TimeSpan Duration { get; set; }
        public dynamic Thumbnails { get; set; }
        public string Link { get; set; }
        public ulong? Likes { get; set; }
        public ulong? Dislikes { get; set; }
        public ulong? Comments { get; set; }
        public ulong? Views { get; set; }
        public ulong? Favorites { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is VideoEntity compareObj)) { return false; }

            return Title.Equals(compareObj.Title)
                && ChannelName.Equals(compareObj.ChannelName);
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
