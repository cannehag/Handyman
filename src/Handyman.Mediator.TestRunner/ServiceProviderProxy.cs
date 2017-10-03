using System;
using System.Collections.Generic;

namespace Handyman.Mediator.TestRunner
{
    internal class ServiceProviderProxy : IServiceProvider
    {
        public IServiceProvider ServiceProvider { get; set; }

        public object GetService(Type type) => ServiceProvider.GetService(type);
        public IEnumerable<object> GetServices(Type type) => ServiceProvider.GetServices(type);
    }
}