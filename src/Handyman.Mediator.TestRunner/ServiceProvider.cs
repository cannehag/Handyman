using System;
using System.Collections.Generic;

namespace Handyman.Mediator.TestRunner
{
    internal class ServiceProvider : IServiceProvider
    {
        public static IServiceProvider Create(object target)
        {
            var type = target.GetType();

            var sp = new ServiceProvider();

            var getService = type.GetMethod("GetService", new[] { typeof(Type) });
            if (getService == null) throw new InvalidOperationException();
            sp.GetService = t => getService.Invoke(target, new object[] { t });

            var getServices = type.GetMethod("GetServices", new[] { typeof(Type) });
            if (getServices == null)
            {
                sp.GetServices = t =>
                {
                    var enumerableType = typeof(IEnumerable<>).MakeGenericType(t);
                    return (IEnumerable<object>)sp.GetService(enumerableType);
                };
            }
            else
            {
                sp.GetServices = t => (IEnumerable<object>)getServices.Invoke(target, new object[] { t });
            }

            return sp;
        }

        public Func<Type, object> GetService { get; set; }
        public Func<Type, IEnumerable<object>> GetServices { get; set; }

        object IServiceProvider.GetService(Type type)
        {
            return GetService(type);
        }

        IEnumerable<object> IServiceProvider.GetServices(Type type)
        {
            return GetServices(type);
        }
    }
}