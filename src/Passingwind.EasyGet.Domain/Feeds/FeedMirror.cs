using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Passingwind.EasyGet.Feeds;

public class FeedMirror : AuditedEntity
{
    public Guid FeedId { get; set; }

    public bool Enabled { get; set; }

    //public FeedMirrorType MirrorType { get; set; }

    public string MirrorUrl { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { FeedId };
    }
}
