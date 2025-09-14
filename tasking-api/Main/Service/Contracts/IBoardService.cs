using tasking_api.Main.Models;
using tasking_api.Main.Models.DTO.Request;

namespace tasking_api.Main.Service.Contracts
{
    public interface IBoardService
    {
        public Task<Result<Board>> CreateBoard(BoardRequest boardRequest);
        public Task<Result<Board>> GetBoard(Guid id);
        public Task<Result<Board>> UpdateBoardInfo(BoardRequest boardRequest);
        public Task<Result<Board>> DeleteBoard(Guid id);
    }
}
