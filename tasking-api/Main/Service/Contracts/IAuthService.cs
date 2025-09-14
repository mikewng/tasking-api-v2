using tasking_api.Main.Models;
using tasking_api.Main.Models.DTO.Request;

namespace tasking_api.Main.Service.Contracts
{
    public interface IAuthService
    {
        public Task<Result> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken);
        public Task<Result> RegisterAsync(RegisterRequest registerRequest, CancellationToken cancellationToken);
    }
}
