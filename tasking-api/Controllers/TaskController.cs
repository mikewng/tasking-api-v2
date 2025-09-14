using Microsoft.AspNetCore.Mvc;
using tasking_api.Main.Models;
using tasking_api.Main.Models.DTO.Request;
using tasking_api.Main.Service.Contracts;

namespace tasking_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;
        private readonly IBoardTaskService _taskService;
        public TaskController(ILogger<BoardController> logger, IBoardTaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        [HttpPost("CreateTask", Name = "CreateTask")]
        public async Task<ActionResult<Result>> Create([FromBody] BoardTaskRequest taskRequest)
        {
            var res = await _taskService.CreateTask(taskRequest);
            if (!res.Success || res.Value == null)
            {
                return Result.Fail("Could not create this new task.");
            }

            return Result.Ok();
        }

        [HttpGet("GetTask/{id:guid}", Name = "GetTask")]
        public async Task<ActionResult<Result<BoardTask>>> Get(Guid id)
        {
            var task = await _taskService.GetTask(id);
            if (!task.Success || task.Value == null)
            {
                return Result<BoardTask>.Fail("Could not find task by given id.");
            }

            return Result<BoardTask>.Ok(task.Value);
        }

        [HttpPatch("UpdateTask")]
        public async Task<ActionResult<Result>> Update([FromBody] BoardTaskRequest taskRequest)
        {
            if (taskRequest.Id == null)
            {
                return Result.Fail("Could not find specified task with following ID.");
            }

            var res = await _taskService.UpdateTask(taskRequest);
            if (!res.Success || res.Value == null)
            {
                return Result.Fail("Could not update specified task.");
            }

            return Result.Ok();
        }

        [HttpDelete]
        public async Task<ActionResult<Result<BoardTask>>> Delete(Guid id)
        {
            var res = await _taskService.DeleteTask(id);
            if (!res.Success || res.Value == null)
            {
                return Result<BoardTask>.Fail("Could not delete specified task.");
            }

            return Result<BoardTask>.Ok(res.Value);
        }
    }
}
