using System;
using System.Collections.Generic;

namespace Handyman.Mediator.TestRunner
{
    internal class Configuration
    {
        internal Dictionary<Type, Action<object>> HandlerActions { get; } = new Dictionary<Type, Action<object>>();
        internal Dictionary<Type, Func<object, object>> HandlerFuncs { get; } = new Dictionary<Type, Func<object, object>>();
    }
}