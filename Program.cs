global using Dapper;
global using Library.Brokers.Storages;
global using Library.Controllers;
global using Library.Models;
global using Library.Services.Foundations;
global using Library.Services.Orchestration;
global using System.Text.Json.Serialization;
using Arora.Blazor.StateContainer;
using Arora.GlobalExceptionHandler;
using Library.Components;
using Scalar.AspNetCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var appVersion = builder.Configuration.GetValue<string>("AppVersion") ?? "1.0.0";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IOwnerService, OwnerService>();
builder.Services.AddTransient<IScheduleEntryService, ScheduleEntryService>();
builder.Services.AddTransient<IStorageBroker, StorageBroker>();
builder.Services.AddTransient<IScheduleOrchestrationService, ScheduleOrchestrationService>();
builder.Services.AddStateContainer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    Process.Start(new ProcessStartInfo
    {
        FileName = "cmd.exe",
        Arguments = "/K bunx @tailwindcss/cli -i ./wwwroot/app.css -o ./wwwroot/style.css --watch --minify",
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = false
    });


app.Lifetime.ApplicationStopping.Register(() =>
{
    if (app.Environment.IsDevelopment())

        Process.Start(new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = "/C taskkill /IM node.exe /F\r\n",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        });
});


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
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); 
app.MapGet("/v", () => appVersion);
app.MapGet("/datetime", () => DateTime.UtcNow.ToString("yyyy-MM-dd"));
//app.MapGet("/", () => Results.Redirect("/scalar/v1"));
app.UseGlobalExceptionHandler();
app.UseAntiforgery();
app.MapStaticAssets();
//app.UseAuthorization();

app.Run();
