namespace YoutubeAPI.Integration.Domain.Entities.YouTube
{
    public class ChannelEntity
    {
        public string Name { get; set; }
        public int Subscribers { get; set; }
        public int Views { get; set; }
        public int Videos { get; set; }

        public override bool Equals(object obj)
        {
            ChannelEntity compareObj = obj as ChannelEntity;
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
