using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{

    private static readonly List<TaskItem> Tasks = new();
    private static int _nextId = 1;


    [HttpGet]
    public ActionResult<IEnumerable<TaskItem>> GetAll()
    {
        return Ok(Tasks);
    }


    [HttpGet("{id:int}")]
    public ActionResult<TaskItem> GetById(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);

        if (task is null)
        {
            return NotFound($"Задача с Id = {id} не найдена.");
        }

        return Ok(task);
    }


    [HttpPost]
    public ActionResult<TaskItem> Create([FromBody] TaskItem newTask)
    {
        if (newTask is null || string.IsNullOrWhiteSpace(newTask.Title))
        {
            return BadRequest("Поле Title обязательно для заполнения.");
        }

        newTask.Id = _nextId++;
        Tasks.Add(newTask);

        return CreatedAtAction(nameof(GetById), new { id = newTask.Id }, newTask);
    }


    [HttpPut("{id:int}")]
    public ActionResult<TaskItem> Update(int id, [FromBody] TaskItem updatedTask)
    {
        if (updatedTask is null || string.IsNullOrWhiteSpace(updatedTask.Title))
        {
            return BadRequest("Поле Title обязательно для заполнения.");
        }

        var task = Tasks.FirstOrDefault(t => t.Id == id);

        if (task is null)
        {
            return NotFound($"Задача с Id = {id} не найдена.");
        }

        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.IsCompleted = updatedTask.IsCompleted;

        return Ok(task);
    }


    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);

        if (task is null)
        {
            return NotFound($"Задача с Id = {id} не найдена.");
        }

        Tasks.Remove(task);

        return Ok($"Задача с Id = {id} удалена.");
    }
}
