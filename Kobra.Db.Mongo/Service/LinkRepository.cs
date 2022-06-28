namespace Kobra.Db.Mongo.Service;

using Kobra.Db.Mongo.Service.Abstraction;
using Kobra.Model.Config;
using Kobra.Model.Db;
using Kobra.Model.Enum;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class LinkRepository : ILinkRepository
{
    private readonly IMongoCollection<Link> links;

    public LinkRepository(IOptions<DbConfig> dbConfig)
    {
        var client = new MongoClient(dbConfig.Value.ConnectionString);
        var db = client.GetDatabase(dbConfig.Value.DatabaseName);

        links = db.GetCollection<Link>("Links");
    }

    public async Task<IList<Link>> GetAsync()
        => await links.Find(_ => true).ToListAsync();

    public async Task<Link?> GetByIdAsync(string id)
        => await links.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<IList<Link>> GetByStatusAsync(DownloadStatus status)
        => await links.Find(x => x.DownloadStatus == status).ToListAsync();

    public async Task InsertAsync(Link link)
        => await links.InsertOneAsync(link);

    public async Task UpdateAsync(Link link)
        => await links.ReplaceOneAsync(x => x.Id == link.Id, link);

    public async Task ReplaceAsync(Link link, string id)
        => await links.ReplaceOneAsync(x => x.Id == id, link);

    public async Task DeleteAsync(Link link)
        => await links.DeleteOneAsync(x => x.Id == link.Id);

    public async Task DeleteAsync(string id)
        => await links.DeleteOneAsync(x => x.Id == id);

    public async Task DeleteAllAsync()
        => await links.DeleteManyAsync(_ => true);
}
