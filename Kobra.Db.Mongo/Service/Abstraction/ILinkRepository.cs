namespace Kobra.Db.Mongo.Service.Abstraction;

using Kobra.Model.Db;
using Kobra.Model.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ILinkRepository
{
    Task DeleteAsync(Link link);

    Task DeleteAsync(string id);

    Task<IList<Link>> GetAsync();

    Task<IList<Link>> GetByStatusAsync(DownloadStatus status);

    Task<Link?> GetByIdAsync(string id);

    Task InsertAsync(Link link);

    Task ReplaceAsync(Link link, string id);

    Task UpdateAsync(Link link);
}