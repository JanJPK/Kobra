using Kobra.Db.Mongo.Service;
using Kobra.Db.Mongo.Service.Abstraction;
using Kobra.Model.Config;
using Kobra.Plugin.Downloader;
using Kobra.Service;
using Kobra.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.Configure<DbConfig>(builder.Configuration.GetSection("DbConfig"));
builder.Services.Configure<DownloaderConfig>(builder.Configuration.GetSection("DownloaderConfig"));
builder.Services.AddSingleton<ILinkRepository, LinkRepository>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<LinkService>();
builder.Services.AddSingleton<GenreService>();
builder.Services.AddSingleton<Downloader>();
builder.Services.AddSingleton<DownloaderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
