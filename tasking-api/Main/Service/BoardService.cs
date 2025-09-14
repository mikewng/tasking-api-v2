using tasking_api.Main.Data.Contracts;
using tasking_api.Main.Models;
using tasking_api.Main.Models.DTO.Request;
using tasking_api.Main.Service.Contracts;

namespace tasking_api.Main.Service
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepo;

        public BoardService(IBoardRepository boardRepository)
        {
            _boardRepo = boardRepository;
        }

        public async Task<Result<Board>> CreateBoard(BoardRequest boardRequest)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(boardRequest.Name))
                {
                    return Result<Board>.Fail("Board name is required");
                }

                var board = new Board
                {
                    Id = Guid.NewGuid(),
                    Name = boardRequest.Name.Trim(),
                    Description = boardRequest.Description?.Trim(),
                    OwnerId = Guid.Parse("fc8e8156-8f3b-11f0-9d7c-9c6b00830113")
                };

                await _boardRepo.AddAsync(board, CancellationToken.None);
                return Result<Board>.Ok(board);
            }
            catch (Exception ex)
            {
                return Result<Board>.Fail($"Failed to create board: {ex.Message}");
            }
        }

        public async Task<Result<Board>> DeleteBoard(Guid id)
        {
            try
            {
                var board = await _boardRepo.GetAsync(id, CancellationToken.None);
                if (board == null)
                {
                    return Result<Board>.Fail("Board not found");
                }

                await _boardRepo.RemoveAsync(board, CancellationToken.None);
                return Result<Board>.Ok(board);
            }
            catch (Exception ex)
            {
                return Result<Board>.Fail($"Failed to delete board: {ex.Message}");
            }
        }

        public async Task<Result<Board>> GetBoard(Guid id)
        {
            try
            {
                var board = await _boardRepo.GetAsync(id, CancellationToken.None);
                if (board == null)
                {
                    return Result<Board>.Fail("Board not found");
                }

                return Result<Board>.Ok(board);
            }
            catch (Exception ex)
            {
                return Result<Board>.Fail($"Failed to retrieve board: {ex.Message}");
            }
        }

        public async Task<Result<Board>> UpdateBoardInfo(BoardRequest boardRequest)
        {
            try
            {
                if (!boardRequest.Id.HasValue)
                {
                    return Result<Board>.Fail("Board ID is required for update");
                }

                if (string.IsNullOrWhiteSpace(boardRequest.Name))
                {
                    return Result<Board>.Fail("Board name is required");
                }

                var existingBoard = await _boardRepo.GetAsync(boardRequest.Id.Value, CancellationToken.None);
                if (existingBoard == null)
                {
                    return Result<Board>.Fail("Board not found");
                }

                existingBoard.Name = boardRequest.Name.Trim();
                existingBoard.Description = boardRequest.Description?.Trim();
                
                return Result<Board>.Ok(existingBoard);
            }
            catch (Exception ex)
            {
                return Result<Board>.Fail($"Failed to update board: {ex.Message}");
            }
        }
    }
}
