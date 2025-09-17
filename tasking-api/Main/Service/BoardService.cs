using tasking_api.Main.Data.Contracts;
using tasking_api.Main.Models;
using tasking_api.Main.Models.DTO.Request;
using tasking_api.Main.Service.Contracts;

namespace tasking_api.Main.Service
{
    public class BoardService : IBoardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BoardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

                await _unitOfWork.Boards.AddAsync(board, CancellationToken.None);
                await _unitOfWork.SaveChangesAsync(CancellationToken.None);
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
                var board = await _unitOfWork.Boards.GetAsync(id, CancellationToken.None);
                if (board == null)
                {
                    return Result<Board>.Fail("Board not found");
                }

                var removeSuccess = await _unitOfWork.Boards.RemoveAsync(board, CancellationToken.None);
                if (!removeSuccess)
                {
                    return Result<Board>.Fail("Failed to remove board");
                }

                await _unitOfWork.SaveChangesAsync(CancellationToken.None);
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
                var board = await _unitOfWork.Boards.GetAsync(id, CancellationToken.None);
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

                var existingBoard = await _unitOfWork.Boards.GetAsync(boardRequest.Id.Value, CancellationToken.None);
                if (existingBoard == null)
                {
                    return Result<Board>.Fail("Board not found");
                }

                existingBoard.Name = boardRequest.Name.Trim();
                existingBoard.Description = boardRequest.Description?.Trim();
                
                var updateSuccess = await _unitOfWork.Boards.UpdateAsync(existingBoard, CancellationToken.None);
                if (!updateSuccess)
                {
                    return Result<Board>.Fail("Failed to update board");
                }

                await _unitOfWork.SaveChangesAsync(CancellationToken.None);
                return Result<Board>.Ok(existingBoard);
            }
            catch (Exception ex)
            {
                return Result<Board>.Fail($"Failed to update board: {ex.Message}");
            }
        }
    }
}
