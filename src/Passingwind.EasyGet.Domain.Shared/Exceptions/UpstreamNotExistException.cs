using System;
using Volo.Abp.ExceptionHandling;

namespace Passingwind.EasyGet.Exceptions;

public class UpstreamNotExistException : Exception, IHasHttpStatusCode
{
    public UpstreamNotExistException()
    {
    }

    public UpstreamNotExistException(string message) : base(message)
    {
    }

    public UpstreamNotExistException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public int HttpStatusCode => 404;
}
