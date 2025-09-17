using tasking_api.Main.Data.Contracts;

namespace tasking_api.Main.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IBoardRepository Boards { get; }
        IBoardTaskRepository BoardTasks { get; }
        IUserRepository Users { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}