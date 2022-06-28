namespace Kobra.Plugin.Downloader;

using Kobra.Model.Common;
using Kobra.Model.Config;
using Kobra.Model.Db;
using Kobra.Model.Enum;
using Kobra.Plugin.Downloader.Abstraction;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Converter;

public sealed class Downloader : IDownloader
{
    private readonly static string[] unsafeCharacters = new[] { "<", ">", ":", "\"", "|", "?", "*", "/", "\\" };
    private readonly DownloaderConfig config;
    private readonly YoutubeClient client;

    public Downloader(IOptions<DownloaderConfig> config)
    {
        this.config = config.Value;
        this.client = new YoutubeClient();
    }

    public async Task<Result> Download(Link link)
    {
        var video = await client.Videos.GetAsync(link.Url);
        var title = string.IsNullOrEmpty(link.Title)
            ? RemoveUnsafeChars(video.Title)
            : link.Title;
        var path = BuildPath(title, link.Format, link.Folder);

        try
        {
            await client.Videos.DownloadAsync(
                video.Id,
                path,
                o => o.SetFFmpegPath(config.FfmpegPath));
        }
        catch (Exception ex)
        {
            return new Result
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }

        return new Result
        {
            Success = true
        };
    }

    private string BuildPath(string title, DownloadFormat format, string? folder)
    {
        var extension = format switch
        {
            DownloadFormat.Audio => ".mp3",
            DownloadFormat.Video => ".mp4",
            _ => ".mp3"
        };

        var path = string.IsNullOrEmpty(folder)
            ? $"{config.OutputPath}/{title}{extension}"
            : $"{config.OutputPath}/{folder}/{title}{extension}";

        if (File.Exists(path))
        {
            var i = 1;
            path = $"{config.OutputPath}/{title}-{i}{extension}";
            while (File.Exists(path))
            {
                i++;
                path = $"{config.OutputPath}/{title}-{i}{extension}";
            }
        }

        return path;
    }

    private static string RemoveUnsafeChars(string filename)
    {
        foreach (var character in unsafeCharacters)
        {
            filename = filename.Replace(character, "");
        }

        return filename;
    }
}
