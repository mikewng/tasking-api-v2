using Microsoft.EntityFrameworkCore;
using tasking_api.Infrastructure.Context;
using tasking_api.Infrastructure.Data;
using tasking_api.Main.Data.Contracts;
using tasking_api.Main.Models;

namespace tasking_api.Main.Data
{
    public class BoardRepository : Repository<Board>, IBoardRepository
    {
        public BoardRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(Board board, CancellationToken ct)
        {
            await DbSet.AddAsync(board, ct);
        }

        public Task<bool> ExistsAsync(Guid ownerId, Guid boardId, CancellationToken ct) =>
            DbSet.AnyAsync(b => b.Id == boardId && b.OwnerId == ownerId, ct);

        public Task<Board?> GetAsync(Guid id, CancellationToken ct) =>
            DbSet.Include(b => b.Tasks).FirstOrDefaultAsync(b => b.Id == id, ct);

        public async Task<IReadOnlyList<Board>> ListOwnedAsync(Guid ownerId, CancellationToken ct) =>
            await DbSet.Where(b => b.OwnerId == ownerId)
                .OrderBy(b => b.Name)
                .AsNoTracking()
                .ToListAsync(ct);

        public async Task<bool> UpdateAsync(Board board, CancellationToken ct)
        {
            if (!await DbSet.AnyAsync(b => b.Id == board.Id, ct))
                return false;

            DbSet.Update(board);
            return true;
        }

        public async Task<bool> RemoveAsync(Board board, CancellationToken ct)
        {
            if (!await DbSet.AnyAsync(b => b.Id == board.Id, ct))
                return false;

            DbSet.Remove(board);
            return true;
        }
    }
}
