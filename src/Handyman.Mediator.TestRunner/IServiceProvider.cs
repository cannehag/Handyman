using System;
using System.Collections.Generic;

namespace Handyman.Mediator.TestRunner
{
    internal interface IServiceProvider
    {
        object GetService(Type type);
        IEnumerable<object> GetServices(Type type);
    }
}