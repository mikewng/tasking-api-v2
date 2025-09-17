using Microsoft.EntityFrameworkCore.Storage;
using tasking_api.Infrastructure.Context;
using tasking_api.Main.Data;
using tasking_api.Main.Data.Contracts;

namespace tasking_api.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;
        
        private IBoardRepository? _boards;
        private IBoardTaskRepository? _boardTasks;
        private IUserRepository? _users;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IBoardRepository Boards => 
            _boards ??= new BoardRepository(_context);

        public IBoardTaskRepository BoardTasks => 
            _boardTasks ??= new BoardTaskRepository(_context);

        public IUserRepository Users => 
            _users ??= new UserRepository(_context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync(cancellationToken);
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(cancellationToken);
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}