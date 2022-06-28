namespace Kobra.Plugin.Downloader.Abstraction;

using Kobra.Model.Common;
using Kobra.Model.Db;
using System.Threading.Tasks;

public interface IDownloader
{
    public Task<Result> Download(Link link);
}
