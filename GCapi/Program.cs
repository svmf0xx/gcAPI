using gcapi.Controllers;
using gcapi.DataBase;
using gcapi.Interfaces;
using gcapi.Interfaces.Services;
using gcapi.Realizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    { 
        options.JsonSerializerOptions.ReferenceHandler = null;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "gcAPI",
        Description = "API for handling gcal database",
    });
});

builder.Services.AddDbContext<gContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSignalR();

//DI Containers
builder.Services.AddTransient<ICalendarObjectService, CalendarObjectService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IGroupService, GroupService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ISignalRHub, SignalRHub>();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "gcAPI v1");
    c.RoutePrefix = "swagger";
});

app.MapHub<SignalRHub>("/signalRHub");
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
