namespace YoutubeAPI.Integration.Domain.Entities.YouTube
{
    public class ChannelEntity
    {
        private string Name { get; }

        public ulong? Subscribers { get; set; }

        public ulong? Views { get; set; }

        public ulong? Videos { get; set; }

        public dynamic Thumbnails { get; set; }

        public ChannelEntity(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if(!(obj is ChannelEntity compareObj)) { return false; }
            return this.Name.Equals(compareObj.Name);
        } 

        public override string ToString()
        {
            return this.Name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
