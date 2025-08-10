global using Dapper;
global using ShareWithYourLovedOne.Brokers.Storages;
global using ShareWithYourLovedOne.Controllers;
global using ShareWithYourLovedOne.Models;
global using ShareWithYourLovedOne.Services.Foundations;
global using System.Text.Json.Serialization;
using Arora.Blazor.StateContainer;
using Arora.GlobalExceptionHandler;
using ShareWithYourLovedOne.Components;
using Scalar.AspNetCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var appVersion = builder.Configuration.GetValue<string>("AppVersion") ?? "1.0.0";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IOwnerService, OwnerService>();
builder.Services.AddTransient<IScheduleEntryService, ScheduleEntryService>();
builder.Services.AddTransient<IStorageBroker, StorageBroker>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddStateContainer();
builder.Services.AddOpenApi();

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
    .AddInteractiveServerRenderMode(); //for backend comment this
app.MapGet("/v", () => appVersion);
app.MapGet("/datetime", () => DateTime.UtcNow.ToString("yyyy-MM-dd"));
//app.MapGet("/", () => Results.Redirect("/scalar/v1")); //for backend uncommet this 
app.UseGlobalExceptionHandler();
app.UseAntiforgery();
app.MapStaticAssets();
//app.UseAuthorization();

app.Run();
