global using System.Text.Json.Serialization;
global using Dapper;
global using Library.Brokers.Storages;
global using Library.Controllers;
global using Library.Models;
global using Library.Services.Foundation;
//global using Library.Services.Orchestration;
using Arora.GlobalExceptionHandler;
using Library.Services.Foundations;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var appVersion = builder.Configuration.GetValue<string>("AppVersion") ?? "1.0.0";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IOwnerService, OwnerService>();
builder.Services.AddTransient<IScheduleEntryService, ScheduleEntryService>();
builder.Services.AddTransient<IStorageBroker, StorageBroker>();
builder.Services.AddOpenApi();

//cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors(); //cors
app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Simple API")
        .WithTheme(ScalarTheme.DeepSpace)
        .WithDarkModeToggle(false)
        .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Axios)
        .WithCustomCss($$"""
            .open-api-client-button { display: none !important; }
            a[target="_blank"].no-underline { display: none !important; }
            .darklight-reference { display: flex;flex-flow: row;}
            .darklight-reference::before {
                content: "LORD AROЯA" !important;
                font-size: 22px !important;
                }
            .darklight-reference::after {
                content: "{{appVersion}}" !important;
                font-size: 20px !important;
            }
        """);
    //.WithFavicon(app.Configuration.GetValue<string>("FavIcon") ?? "");
});

app.MapOwnerController().MapScheduleEntryEndpoints().MapPublicScheduleEndpoints();

app.UseHttpsRedirection();
app.MapGet("/v", () => appVersion);
app.MapGet("/datetime", () => DateTime.UtcNow.ToString("yyyy-MM-dd"));
app.MapGet("/", () => Results.Redirect("/scalar/v1"));
app.UseGlobalExceptionHandler();

//app.UseAuthorization();

app.Run();
