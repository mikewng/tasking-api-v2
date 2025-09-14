using tasking_api.Main.Models;

namespace tasking_api.Main.Data.Contracts
{
    public interface IBoardRepository
    {
        Task<Board?> GetAsync(Guid id, CancellationToken ct);
        Task<IReadOnlyList<Board>> ListOwnedAsync(Guid ownerId, CancellationToken ct);
        Task AddAsync(Board board, CancellationToken ct);
        Task RemoveAsync(Board board, CancellationToken ct);
        Task<bool> ExistsAsync(Guid ownerId, Guid boardId, CancellationToken ct);
    }
}
