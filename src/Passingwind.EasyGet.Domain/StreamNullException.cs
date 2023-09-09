using System;
using Volo.Abp.ExceptionHandling;

namespace Passingwind.EasyGet;

public class StreamNullException : Exception, IHasHttpStatusCode
{
    public int HttpStatusCode => 404;

    public StreamNullException()
    {
    }

    public StreamNullException(string message) : base(message)
    {
    }

    public StreamNullException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
