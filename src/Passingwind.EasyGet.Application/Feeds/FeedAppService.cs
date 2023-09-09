using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;

namespace Passingwind.EasyGet.Feeds;

public class FeedAppService : EasyGetAppService, IFeedAppService
{
    private readonly IFeedRepository _feedRepository;

    public FeedAppService(IFeedRepository feedRepository)
    {
        _feedRepository = feedRepository;
    }

    public virtual async Task<PagedResultDto<FeedDto>> GetListAsync(FeedListRequestDto input)
    {
        var count = await _feedRepository.GetCountAsync();
        var list = await _feedRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, nameof(Feed.CreationTime) + " desc");

        return new PagedResultDto<FeedDto>()
        {
            Items = ObjectMapper.Map<List<Feed>, List<FeedDto>>(list),
            TotalCount = count,
        };
    }

    public virtual async Task<FeedDto> GetAsync(Guid id)
    {
        var entity = await _feedRepository.GetAsync(id);

        return ObjectMapper.Map<Feed, FeedDto>(entity);
    }

    public virtual async Task<FeedDto> CreateAsync(FeedCreateOrUpdateDto input)
    {
        var entity = new Feed(GuidGenerator.Create(), input.Name, input.Type, CurrentTenant.Id)
        {
            Description = input.Description,
            TenantId = input.TenantId,
        };

        input.MapExtraPropertiesTo(entity);

        await _feedRepository.InsertAsync(entity);

        return ObjectMapper.Map<Feed, FeedDto>(entity);
    }

    public virtual async Task<FeedDto> UpdateAsync(Guid id, FeedCreateOrUpdateDto input)
    {
        var entity = await _feedRepository.GetAsync(id);

        entity.Description = input.Description;
        entity.TenantId = input.TenantId;

        input.MapExtraPropertiesTo(entity);

        await _feedRepository.UpdateAsync(entity);

        return ObjectMapper.Map<Feed, FeedDto>(entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await _feedRepository.DeleteAsync(id);
    }
}
