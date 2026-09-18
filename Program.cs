using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Регистрируем контроллеры (Controller-based Web API, как того требует задание).
builder.Services.AddControllers();

// Swagger/OpenAPI — используется для описания и ручного тестирования API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Task Management API",
        Version = "v1",
        Description = "Лабораторная работа №2 (CSE5032) — REST API для управления списком задач (TaskItem)."
    });
});

var app = builder.Build();

// Swagger UI включён всегда (а не только в Development), чтобы гарантированно
// быть доступным после запуска проекта, как того требует задание.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management API v1");
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
