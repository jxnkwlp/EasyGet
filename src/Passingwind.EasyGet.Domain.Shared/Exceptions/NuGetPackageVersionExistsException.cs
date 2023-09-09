using System;
using Volo.Abp.ExceptionHandling;

namespace Passingwind.EasyGet.Exceptions;

public class NuGetPackageVersionExistsException : Exception, IHasHttpStatusCode
{
    public NuGetPackageVersionExistsException()
    {
    }

    public NuGetPackageVersionExistsException(string message) : base(message)
    {
    }

    public NuGetPackageVersionExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public int HttpStatusCode => 409;
}
