global using Library.Brokers.Storages;
global using Library.Services.Foundation;
global using Library.Models;
global using Library.Controllers;
global using System.Text.Json.Serialization;
global using Dapper;
using Scalar.AspNetCore;
using Arora.GlobalExceptionHandler;

var builder = WebApplication.CreateBuilder(args);
var appVersion = builder.Configuration.GetValue<string>("AppVersion") ?? "1.0.0";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IStorageBroker, StorageBroker>();
builder.Services.AddOpenApi();

var app = builder.Build();
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

app.MapAuthorController().MapBookController();

app.UseHttpsRedirection();
app.MapGet("/v", () => appVersion);
app.MapGet("/datetime", () => DateTime.UtcNow.ToString("yyyy-MM-dd"));
app.MapGet("/", () => Results.Redirect("/scalar/v1"));
app.UseGlobalExceptionHandler();

//app.UseAuthorization();

app.Run();
