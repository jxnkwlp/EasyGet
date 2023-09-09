using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;

namespace Passingwind.EasyGet.Feeds;

public class FeedCreateOrUpdateDto : ExtensibleEntityDto
{
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    public virtual FeedType Type { get; set; }
    public virtual Guid? TenantId { get; set; }
}
