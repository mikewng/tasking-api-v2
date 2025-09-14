using tasking_api.Main.Secure.Contracts;

namespace tasking_api.Main.Secure
{
    public class UserSecure : IUserSecure
    {
        public string GenerateSecureToken()
        {
            throw new NotImplementedException();
        }

        public string HashPassword(string password)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            throw new NotImplementedException();
        }
    }
}
