namespace Kobra.Service;

using System.Linq;
using Kobra.Db.Mongo.Service.Abstraction;
using Kobra.Mapper;
using Kobra.Model.Dto;

public class LinkService
{
    private readonly ILinkRepository linkRepository;

    public LinkService(ILinkRepository linkRepository)
    {
        this.linkRepository = linkRepository;
    }

    public async Task<IList<Link>> GetAsync()
    {
        var links = await linkRepository.GetAsync();

        return links.Select(x => LinkMapper.Map(x)).ToList();
    }

    public async Task InsertAsync(Link link)
    {
        var entity = LinkMapper.Map(link);

        await linkRepository.InsertAsync(entity);
        link.Id = entity.Id;
    }

    public async Task DeleteAsync(string id)
        => await linkRepository.DeleteAsync(id);
}
