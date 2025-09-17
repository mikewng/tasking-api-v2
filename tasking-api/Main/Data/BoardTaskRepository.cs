using Microsoft.EntityFrameworkCore;
using tasking_api.Infrastructure.Context;
using tasking_api.Infrastructure.Data;
using tasking_api.Main.Data.Contracts;
using tasking_api.Main.Models;
using tasking_api.Main.Models.DTO;

namespace tasking_api.Main.Data
{
    public class BoardTaskRepository : Repository<BoardTask>, IBoardTaskRepository
    {
        public BoardTaskRepository(AppDbContext context) : base(context) { }

        public async Task<BoardTask?> GetAsync(Guid id, CancellationToken ct)
        {
            return await DbSet.FirstOrDefaultAsync(t => t.Id == id, ct);
        }

        public async Task<IReadOnlyList<BoardTaskDto>> ListByBoardAsync(
            Guid boardId, CancellationToken ct, BoardTaskStatus? status = null, int skip = 0, int take = 50)
        {
            var query = DbSet.Where(t => t.BoardId == boardId);

            if (status.HasValue)
            {
                query = query.Where(t => t.TaskStatus == status.Value);
            }

            return await query
                .Skip(skip)
                .Take(take)
                .OrderBy(t => t.CreatedAt)
                .Select(t => new BoardTaskDto
                {
                    Id = t.Id,
                    BoardId = t.BoardId,
                    Name = t.Name,
                    Description = t.Description,
                    Deadline = t.Deadline,
                    Status = t.TaskStatus,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt
                })
                .ToListAsync(ct);
        }

        public async Task AddAsync(BoardTask task, CancellationToken ct)
        {
            await DbSet.AddAsync(task, ct);
        }

        public async Task<bool> UpdateAsync(BoardTask task, CancellationToken ct)
        {
            if (!await DbSet.AnyAsync(t => t.Id == task.Id, ct))
                return false;

            DbSet.Update(task);
            return true;
        }

        public async Task<bool> RemoveAsync(BoardTask task, CancellationToken ct)
        {
            if (!await DbSet.AnyAsync(t => t.Id == task.Id, ct))
                return false;

            DbSet.Remove(task);
            return true;
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct) =>
            DbSet.AnyAsync(t => t.Id == id, ct);
    }
}
