namespace Kobra.Model.Config;

public class DownloaderConfig : ConfigBase
{
    public string FfmpegPath { get; set; } = null!;

    public string OutputPath { get; set; } = null!;
}
