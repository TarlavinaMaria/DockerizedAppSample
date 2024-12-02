using DockerizedAppSample;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();

var app = builder.Build();

app.MapGet("/", () => new {Message = "server is running"});
app.MapGet("/ping", () => new {Message = "pong"});


app.MapGet("/labels/add", async (string label, ApplicationDbContext db) =>
{
    await db.Labels.AddAsync(new Label() { Value = label });
    await db.SaveChangesAsync();
});

app.MapGet("/labels", async (ApplicationDbContext db) =>
{
    return await db.Labels.ToListAsync();
});


app.Run();
