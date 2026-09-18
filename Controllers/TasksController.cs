using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    // Хранение в памяти (List<TaskItem>) — база данных для этой лабораторной
    // работы не требуется. static, чтобы данные сохранялись между запросами
    // в рамках одного запущенного процесса.
    private static readonly List<TaskItem> Tasks = new();
    private static int _nextId = 1;

    /// <summary>
    /// GET /api/tasks — получить все задачи.
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<TaskItem>> GetAll()
    {
        return Ok(Tasks);
    }

    /// <summary>
    /// GET /api/tasks/{id} — получить задачу по Id.
    /// </summary>
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

    /// <summary>
    /// POST /api/tasks — добавить новую задачу.
    /// </summary>
    [HttpPost]
    public ActionResult<TaskItem> Create([FromBody] TaskItem newTask)
    {
        if (newTask is null || string.IsNullOrWhiteSpace(newTask.Title))
        {
            return BadRequest("Поле Title обязательно для заполнения.");
        }

        newTask.Id = _nextId++;
        Tasks.Add(newTask);

        // 201 Created + Location-заголовок на GetById, как принято для POST.
        return CreatedAtAction(nameof(GetById), new { id = newTask.Id }, newTask);
    }

    /// <summary>
    /// PUT /api/tasks/{id} — изменить существующую задачу.
    /// </summary>
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

    /// <summary>
    /// DELETE /api/tasks/{id} — удалить задачу по Id.
    /// </summary>
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
