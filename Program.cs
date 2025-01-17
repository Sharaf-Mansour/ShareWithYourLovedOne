global using Library.Brokers.Storages;
global using Library.Foundation.Services;
global using Library.Models;
global using Library.Controllers;
using Scalar.AspNetCore;

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
            a[target="_blank"].darklight-reference-promo { display: none !important; }
            .darklight-reference-switch::before {
                content: "LORD AROЯA" !important;
                font-size: 27px !important;
            }
            .darklight-reference-switch::after {
                content: " v{{""}}" !important;
                font-size: 22px !important;
            }
        """)
        .WithFavicon(app.Configuration.GetValue<string>("FavIcon") ?? "");
});

app.MapAuthorController().MapBookController();

app.UseHttpsRedirection();
app.MapGet("/v", () => appVersion);
app.MapGet("/datetime", () => DateTime.UtcNow.ToString("yyyy-MM-dd"));
app.MapGet("/", () => Results.Redirect("/scalar/v1"));

//app.UseAuthorization();

app.Run();
