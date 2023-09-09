using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Passingwind.EasyGet.Feeds;

public interface IFeedAppService : IApplicationService
{
    Task<PagedResultDto<FeedDto>> GetListAsync(FeedListRequestDto input);

    Task<FeedDto> GetAsync(Guid id);

    Task<FeedDto> CreateAsync(FeedCreateOrUpdateDto input);

    Task<FeedDto> UpdateAsync(Guid id, FeedCreateOrUpdateDto input);

    Task DeleteAsync(Guid id);

}
