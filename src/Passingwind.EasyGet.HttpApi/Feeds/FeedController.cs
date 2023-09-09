using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Passingwind.EasyGet.Feeds;

[RemoteService]
[Route("api/feeds")]
public class FeedController : EasyGetController, IFeedAppService
{
    private readonly IFeedAppService _service;

    public FeedController(IFeedAppService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public virtual Task<FeedDto> GetAsync(Guid id)
    {
        return _service.GetAsync(id);
    }

    [HttpGet]
    public virtual Task<PagedResultDto<FeedDto>> GetListAsync(FeedListRequestDto input)
    {
        return _service.GetListAsync(input);
    }

    [HttpPost]
    public virtual Task<FeedDto> CreateAsync(FeedCreateOrUpdateDto input)
    {
        return _service.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public virtual Task<FeedDto> UpdateAsync(Guid id, FeedCreateOrUpdateDto input)
    {
        return _service.UpdateAsync(id, input);
    }

    [HttpDelete("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return _service.DeleteAsync(id);
    }
}
