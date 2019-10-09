namespace YoutubeAPI.Integration.Infra.ExternalServices.GoogleAPI.Interfaces
{
    public interface IGoogleService<out TService>
    {
        TService GetService();
    }
}
