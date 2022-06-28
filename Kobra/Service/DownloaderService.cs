namespace Kobra.Service;

using Kobra.Db.Mongo.Service.Abstraction;
using Kobra.Model.Enum;
using Kobra.Plugin.Downloader;
using System.Threading.Tasks;

public class DownloaderService
{
    private readonly ILinkRepository linkRepository;
    private readonly Downloader downloader;

    public DownloaderService(ILinkRepository linkRepository, Downloader downloader)
    {
        this.linkRepository = linkRepository;
        this.downloader = downloader;
    }

    public async Task Download(string id)
    {
        var link = await linkRepository.GetByIdAsync(id);
        if (link == null) return;

        var result = await this.downloader.Download(link);
        link.DownloadStatus = result.Success
            ? DownloadStatus.Completed
            : DownloadStatus.Failed;
        await linkRepository.UpdateAsync(link);
    }
}
