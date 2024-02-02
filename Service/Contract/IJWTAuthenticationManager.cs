namespace ENTITYAPP.Service.Contract
{
    public interface IJWTAuthenticationManager
    {
        string Authenticate(string email, string password);
    }
}
