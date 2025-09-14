namespace tasking_api.Main.Secure.Contracts
{
    public interface IUserSecure
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
        string GenerateSecureToken();
    }
}
