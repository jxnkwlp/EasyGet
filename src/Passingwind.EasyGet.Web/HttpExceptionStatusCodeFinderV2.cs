using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.DependencyInjection;

namespace Passingwind.EasyGet;

// TODO
[ExposeServices(typeof(IHttpExceptionStatusCodeFinder))]
public class HttpExceptionStatusCodeFinderV2 : DefaultHttpExceptionStatusCodeFinder
{
    public HttpExceptionStatusCodeFinderV2(IOptions<AbpExceptionHttpStatusCodeOptions> options) : base(options)
    {
    }
}
