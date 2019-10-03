namespace YoutubeAPI.Integration.Infra.ExternalServices.GoogleAPI.Interfaces
{
    public interface IGoogleService<TService>
    {
        TService GetService();
    }
}
