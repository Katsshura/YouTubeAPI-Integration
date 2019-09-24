namespace YoutubeAPI.Integration.Infra.ExternalServices.GoogleAPI.Services
{
    public interface IGoogleServiceManager<TService>
    {
        TService GetService();
    }
}
