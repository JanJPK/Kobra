namespace Kobra.Mapper;

using Db = Kobra.Model.Db;
using Dto = Kobra.Model.Dto;

public static class LinkMapper
{
    public static Db.Link Map(Dto.Link link)
        => new Db.Link
        {
            Id = link.Id,
            Url = link.Url,
            Title = link.Title,
            DownloadStatus = link.DownloadStatus
        };

    public static Dto.Link Map(Db.Link link)
        => new Dto.Link
        {
            Id = link.Id,
            Url = link.Url,
            Title = link.Title,
            DownloadStatus = link.DownloadStatus
        };
}
