namespace Kobra.Model.Db;

using Kobra.Model.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Link
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Url { get; set; } = null!;

    public string? Title { get; set; }

    public string? Folder { get; set; }

    public DownloadFormat Format { get; set; }

    public DownloadStatus DownloadStatus { get; set; }
}
