using tasking_api.Main.Models;
using tasking_api.Main.Models.DTO.Request;

namespace tasking_api.Main.Service.Contracts
{
    public interface IBoardTaskService
    {
        public Task<Result<BoardTask>> CreateTask(BoardTaskRequest taskRequest);
        public Task<Result<BoardTask>> GetTask(Guid id);
        public Task<Result<BoardTask>> UpdateTask(BoardTaskRequest taskRequest);
        public Task<Result<BoardTask>> DeleteTask(Guid id);
    }
}
