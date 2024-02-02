namespace ValidateService;

public class ValidateService
{
    public bool ValidateEmail(string email)
    {
        return email.Contains("@");
    }
}
