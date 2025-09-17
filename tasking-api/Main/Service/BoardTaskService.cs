using tasking_api.Main.Data.Contracts;
using tasking_api.Main.Models;
using tasking_api.Main.Models.DTO.Request;
using tasking_api.Main.Service.Contracts;

namespace tasking_api.Main.Service
{
    public class BoardTaskService : IBoardTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BoardTaskService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<BoardTask>> CreateTask(BoardTaskRequest taskRequest)
        {
            if (string.IsNullOrWhiteSpace(taskRequest.Name))
            {
                return Result<BoardTask>.Fail("Task name is required.");
            }

            if (taskRequest.BoardId == Guid.Empty)
            {
                return Result<BoardTask>.Fail("Board ID is required.");
            }

            try
            {
                var boardTask = new BoardTask
                {
                    Id = Guid.NewGuid(),
                    BoardId = taskRequest.BoardId,
                    Name = taskRequest.Name.Trim(),
                    Description = taskRequest.Description?.Trim() ?? string.Empty,
                    Deadline = taskRequest.Deadline,
                    TaskStatus = BoardTaskStatus.NotStarted,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.BoardTasks.AddAsync(boardTask, CancellationToken.None);
                await _unitOfWork.SaveChangesAsync(CancellationToken.None);

                return Result<BoardTask>.Ok(boardTask);
            }
            catch (Exception ex)
            {
                return Result<BoardTask>.Fail($"Failed to create task: {ex.Message}");
            }
        }
        public async Task<Result<BoardTask>> GetTask(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Result<BoardTask>.Fail("Task ID is required.");
            }

            var task = await _unitOfWork.BoardTasks.GetAsync(id, CancellationToken.None);
            if (task == null)
            {
                return Result<BoardTask>.Fail("Task not found.");
            }
            
            return Result<BoardTask>.Ok(task);
        }
        public async Task<Result<BoardTask>> DeleteTask(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Result<BoardTask>.Fail("Task ID is required.");
            }
            
            var existingTask = await _unitOfWork.BoardTasks.GetAsync(id, CancellationToken.None);
            if (existingTask == null)
            {
                return Result<BoardTask>.Fail("Task not found.");
            }
            
            var removeSuccess = await _unitOfWork.BoardTasks.RemoveAsync(existingTask, CancellationToken.None);
            if (!removeSuccess)
            {
                return Result<BoardTask>.Fail("Failed to remove task.");
            }

            await _unitOfWork.SaveChangesAsync(CancellationToken.None);
            return Result<BoardTask>.Ok(existingTask);
        }
        public async Task<Result<BoardTask>> UpdateTask(BoardTaskRequest taskRequest)
        {
            if (!taskRequest.Id.HasValue || taskRequest.Id.Value == Guid.Empty)
            {
                return Result<BoardTask>.Fail("Task ID is required for update.");
            }

            if (string.IsNullOrWhiteSpace(taskRequest.Name))
            {
                return Result<BoardTask>.Fail("Task name is required.");
            }

            if (taskRequest.BoardId == Guid.Empty)
            {
                return Result<BoardTask>.Fail("Board ID is required.");
            }

            var existingTask = await _unitOfWork.BoardTasks.GetAsync(taskRequest.Id.Value, CancellationToken.None);
                
            if (existingTask == null)
            {
                return Result<BoardTask>.Fail("Task not found.");
            }

            existingTask.Name = taskRequest.Name.Trim();
            existingTask.Description = taskRequest.Description?.Trim() ?? string.Empty;
            existingTask.Deadline = taskRequest.Deadline;
            existingTask.BoardId = taskRequest.BoardId;
            existingTask.UpdatedAt = DateTime.UtcNow;

            var updateSuccess = await _unitOfWork.BoardTasks.UpdateAsync(existingTask, CancellationToken.None);
            if (!updateSuccess)
            {
                return Result<BoardTask>.Fail("Failed to update task.");
            }

            await _unitOfWork.SaveChangesAsync(CancellationToken.None);
            return Result<BoardTask>.Ok(existingTask);

        }
    }
}

