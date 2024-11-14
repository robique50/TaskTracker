using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TasksAPI.Models;
using TasksAPI.Services;

namespace TasksAPI
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        

        ITaskCollectionService _taskCollectionService;

        public TaskController(ITaskCollectionService taskCollectionService)
        {
            _taskCollectionService = taskCollectionService ?? throw new ArgumentNullException(nameof(TaskCollectionService)); ;
        }

        /// <summary>
        /// Retrieves all tasks.
        /// </summary>
        /// <returns>A list of tasks.</returns>
        [HttpGet()]
        public async Task<IActionResult> GetTasks()
        {
            List<TaskModel> tasks = await _taskCollectionService.GetAll();
            return Ok(tasks);
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="task">Task model from the request body.</param>
        /// <returns>The created task.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskModel taskModel)
        {
            if (taskModel == null)
                return BadRequest("Task model cannot be null");

            var created = await _taskCollectionService.Create(taskModel);
            if (created)
                return Ok(taskModel);

            return BadRequest("Failed to create task");
        }
    /// <summary>
    /// Updates an existing task identified by the ID.
    /// </summary>
    /// <param name="id">The ID of the task to update.</param>
    /// <param name="task">Task model from the request body with updated data.</param>
    /// <returns>The updated task.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskModel task)
    {
        if (task == null)
        {
            return BadRequest("Task model cannot be null");
        }

        var updated = await _taskCollectionService.Update(id, task);
        if (updated)
        {
            return Ok(task);
        }
        return NotFound("Task with the specified ID does not exist or could not be updated");
    }



    /// <summary>
    /// Deletes a task identified by the ID.
    /// </summary>
    /// <param name="id">The ID of the task to delete.</param>
    /// <returns>Action result indicating success or failure.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var deleted = await _taskCollectionService.Delete(id);
        if (deleted)
        {
            return Ok($"Task with ID {id} has been deleted");
        }
        return NotFound("Task with the specified ID does not exist or could not be deleted");
    }
    }



    //[HttpGet()]
    //public IActionResult Get()
    //{
    //    return Ok("Hello, World!");
    //}
    //[HttpPost]
    //public IActionResult Post()
    //{
    //    return Ok("Hello, World!");
    //}
    //[HttpPut]
    //public IActionResult Put()
    //{
    //    return Ok("Hello, World!");
    //}
    //[HttpDelete]
    //public IActionResult Delete()
    //{
    //    return Ok("Hello, World!");
    //}
}





