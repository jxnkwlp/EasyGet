using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Passingwind.EasyGet.Feeds;

public class Feed : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Feed()
    {
    }

    public Feed(Guid id, string name, FeedType type, Guid? tenantId = null) : base(id)
    {
        Name = name;
        Type = type;
        TenantId = tenantId;
    }

    public string Name { get; protected set; }
    public FeedType Type { get; protected set; }
    public string Description { get; set; }
    public Guid? TenantId { get; set; }

    public FeedMirror Mirror { get; set; }

    public bool HasUpStream()
    {
        return Mirror?.Enabled == true && !string.IsNullOrWhiteSpace(Mirror.MirrorUrl);
    }
}
