global using Analytics.Data;
global using Microsoft.EntityFrameworkCore;
using Services;

var builder = WebApplication.CreateBuilder(args);

// The following line enables Application Insights telemetry collection.
builder.Services.AddApplicationInsightsTelemetry();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IUploadFileService, UploadFileService>();
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.WithOrigins(
    "http://localhost:3000",
    "https://123analytic.000webhostapp.com",
    "https://testingtestingfaraz.000webhostapp.com"
    ).AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();
