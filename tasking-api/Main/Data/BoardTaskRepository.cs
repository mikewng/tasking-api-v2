using Microsoft.EntityFrameworkCore;
using tasking_api.Infrastructure.Context;
using tasking_api.Main.Data.Contracts;
using tasking_api.Main.Models;
using tasking_api.Main.Models.DTO;

namespace tasking_api.Main.Data
{
    public class BoardTaskRepository : IBoardTaskRepository
    {
        private readonly AppDbContext _db;
        
        public BoardTaskRepository(AppDbContext db) => _db = db;

        public async Task<BoardTask?> GetAsync(Guid id, CancellationToken ct)
        {
            return await _db.task
                .FirstOrDefaultAsync(t => t.Id == id, ct);
        }

        public async Task<IReadOnlyList<BoardTaskDto>> ListByBoardAsync(
            Guid boardId, CancellationToken ct, BoardTaskStatus? status = null, int skip = 0, int take = 50)
        {
            var query = _db.task.Where(t => t.BoardId == boardId);

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
            task.CreatedAt = DateTime.UtcNow;
            await _db.task.AddAsync(task, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task RemoveAsync(BoardTask task, CancellationToken ct)
        {
            _db.task.Remove(task);
            await _db.SaveChangesAsync(ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct) =>
            _db.board.AnyAsync(b => b.Id == id, ct);
    }
}
