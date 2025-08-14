global using Dapper;
global using ShareWithYourLovedOne.Brokers.Storages;
global using ShareWithYourLovedOne.Models;
global using ShareWithYourLovedOne.Services.Foundations;
global using System.Text.Json.Serialization;
using Arora.Blazor.StateContainer;
using Arora.GlobalExceptionHandler;
using Blazored.Toast;
using ShareWithYourLovedOne.Components;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var appVersion = builder.Configuration.GetValue<string>("AppVersion") ?? "1.0.0";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IOwnerService, OwnerService>();
builder.Services.AddTransient<IScheduleEntryService, ScheduleEntryService>();
builder.Services.AddTransient<IStorageBroker, StorageBroker>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazoredToast();
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
 
app.UseHttpsRedirection();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); //for backend comment this
app.MapGet("/v", () => appVersion);
app.MapGet("/datetime", () => DateTime.UtcNow.ToString("yyyy-MM-dd"));
//app.MapGet("/", () => Results.Redirect("/scalar/v1")); //for backend uncommet this 
app.UseGlobalExceptionHandler();
app.UseAntiforgery();
app.MapStaticAssets();
//app.UseAuthorization();

app.Run();
