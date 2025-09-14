using Microsoft.AspNetCore.Mvc;
using tasking_api.Main.Models;
using tasking_api.Main.Models.DTO.Request;
using tasking_api.Main.Service.Contracts;

namespace tasking_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;
        private readonly IBoardService _boardService;

        public BoardController(ILogger<BoardController> logger, IBoardService boardService)
        {
            _logger = logger;
            _boardService = boardService;
        }

        [HttpPost("CreateBoard")]
        public async Task<ActionResult<Result>> Create([FromBody] BoardRequest boardRequest)
        {
            var res = await _boardService.CreateBoard(boardRequest);
            if (!res.Success || res.Value == null)
            {
                return Result.Fail("Could not create this new board");
            }

            return Result.Ok();
        }

        [HttpGet("GetBoard/{id:guid}", Name = "GetBoard")]
        public async Task<ActionResult<Result<Board>>> Get(Guid id)
        {
            var board = await _boardService.GetBoard(id);
            if (!board.Success || board.Value == null)
            {
                return Result<Board>.Fail("Could not find board by given id.");
            }

            return Result<Board>.Ok(board.Value);
        }

        [HttpPatch("UpdateBoard")]
        public async Task<ActionResult<Result>> Update([FromBody] BoardRequest boardRequest)
        {
            if (boardRequest.Id == null)
            {
                return Result.Fail("Could not find specified board with following ID.");
            }

            var res = await _boardService.UpdateBoardInfo(boardRequest);
            if (!res.Success || res.Value == null)
            {
                return Result.Fail("Could not update specified board.");
            }

            return Result.Ok();
        }

        [HttpDelete("DeleteBoard/{id:guid}", Name = "DeleteBoard")]
        public async Task<ActionResult<Result<Board>>> Delete(Guid id)
        {
            var board = await _boardService.DeleteBoard(id);
            if (!board.Success || board.Value == null)
            {
                return Result<Board>.Fail("Could not delete the board of given id.");
            }

            return Result<Board>.Ok(board.Value);
        }
    }
}
