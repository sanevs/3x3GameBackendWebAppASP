using _3x3GameBackendWebAppASP;
using _3x3GameBackendWebAppASP.Reps;
using _3x3GameBackendWebAppASP.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbPath = "game.db";
builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<CellRepository>();
builder.Services.AddScoped<CellService>();

builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();