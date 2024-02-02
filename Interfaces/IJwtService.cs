using System.Security.Claims;

public interface IJwtService
{
    string GenerateToken(string userId);
    bool ValidateToken(string token);
}