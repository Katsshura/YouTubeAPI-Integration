namespace YoutubeAPI.Integration.Infra.ExternalServices.GoogleAPI.Services
{
    public interface IGoogleService<TService>
    {
        TService GetService();
    }
}
