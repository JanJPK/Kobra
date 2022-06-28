namespace Kobra.Model.Dto;

using Kobra.Model.Enum;
using System.ComponentModel.DataAnnotations;

public class Link
{
    public string? Id { get; set; }

    [Required]
    public string Url { get; set; } = null!;

    public string? Title { get; set; }

    public DownloadStatus DownloadStatus { get; set; }
}
