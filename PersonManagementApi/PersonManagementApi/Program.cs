using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog;
using Serilog.Core;
using Microsoft.Extensions.Configuration;
using PersonManagementApi.Middlewares;
using PersonManagementApi;
using PersonManagementApi.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IPersonManagerService, PersonManagerService>();
builder.Services.Configure<AppSettingsOption>(
    builder.Configuration.GetSection(AppSettingsOption.AppSettings));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        x =>
        {
            x.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

AppSettingsOption appsettings = builder.Configuration.GetSection(AppSettingsOption.AppSettings)
                                                     .Get<AppSettingsOption>();
DoStartupActivities();
AddLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();
app.Run();



void AddLogger()
{
    builder.Host.UseSerilog((hostContext, services, configuration) => {
        configuration.WriteTo.File($"{appsettings.LogsPath}\\PersonManagement-daily-.logs",
            LogEventLevel.Information,
        rollingInterval: RollingInterval.Day);
    });
}

void DoStartupActivities()
{
    File.WriteAllText(appsettings.DataSource, "[]");
}