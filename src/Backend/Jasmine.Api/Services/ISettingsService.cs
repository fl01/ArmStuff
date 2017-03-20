namespace Jasmine.Api.Services
{
    public interface ISettingsService
    {
        string GetConnectionString();

        string GetMovementsDbName();
    }
}