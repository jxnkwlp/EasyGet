using AutoMapper;
using Passingwind.EasyGet.Feeds;

namespace Passingwind.EasyGet;

public class EasyGetApplicationAutoMapperProfile : Profile
{
    public EasyGetApplicationAutoMapperProfile()
    { 
        CreateMap<Feed, FeedDto>();
    }
}
