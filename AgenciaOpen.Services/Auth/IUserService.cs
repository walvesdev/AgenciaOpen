namespace AgenciaOpen.Services.Auth
{
    public interface IUserService
    {
        bool ValidateCredentials(string username, string password);
    }
}