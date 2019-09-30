namespace YoutubeAPI.Integration.Domain.Entities.YouTube
{
    public class VideoEntity
    {
        public string Title { get; set; }
        public string ChannelName { get; set; }
        public string Duration { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Comments { get; set; }
        public int Views { get; set; }
        public int Favorites { get; set; }

        public override bool Equals(object obj)
        {
            VideoEntity compareObj = obj as VideoEntity;
            return Title.Equals(compareObj.Title)
                && ChannelName.Equals(compareObj.ChannelName);
        }

        public override string ToString()
        {
            return this.Title.ToString();
        }
    }
}
