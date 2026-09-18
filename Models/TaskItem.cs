namespace TaskManagementApi.Models;

/// <summary>
/// Модель задачи (Task) — как описано в задании лабораторной работы.
/// </summary>
public class TaskItem
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }
}
