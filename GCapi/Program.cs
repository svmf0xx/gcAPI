using gcapi.Controllers;
using gcapi.DataBase;
using gcapi.Interfaces;
using gcapi.Interfaces.Services;
using gcapi.Realizations;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
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

//DI Containers
builder.Services.AddTransient<ICalendarObjectService, CalendarObjectService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IGroupService, GroupService>();
builder.Services.AddTransient<IAuthService, AuthService>();


//builder.Services.AddAuthentication(
//    CertificateAuthenticationDefaults.AuthenticationScheme)
//    .AddCertificate(options =>
//    {
//        options.ChainTrustValidationMode = X509ChainTrustMode.CustomRootTrust;
//        options.CustomTrustStore = new X509Certificate2Collection(new X509Certificate2(
//            builder.Configuration.GetSection("Cert")["File"]));

//        options.RevocationMode = X509RevocationMode.NoCheck;

//    });

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.ConfigureHttpsDefaults(options =>
        options.ClientCertificateMode = ClientCertificateMode.RequireCertificate);

});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "gcAPI v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseAuthentication();

app.MapControllers();

app.Run();
