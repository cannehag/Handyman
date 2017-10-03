using System.Collections.Generic;
using System.Threading.Tasks;

namespace Handyman.Mediator.TestRunner
{
    internal class TestMediator : Mediator
    {
        private readonly Configuration _configuration;
        private readonly Tracer _tracer;

        public TestMediator(Configuration configuration, Tracer tracer) : this(new ServiceProviderProxy())
        {
            _configuration = configuration;
            _tracer = tracer;
        }

        private TestMediator(ServiceProviderProxy serviceProviderProxy)
            : base(serviceProviderProxy.GetService, serviceProviderProxy.GetServices)
        {
            ServiceProviderProxy = serviceProviderProxy;
        }

        internal ServiceProviderProxy ServiceProviderProxy { get; set; }

        public override IEnumerable<Task> Publish(IAsyncEvent @event)
        {
            _tracer.Events.Add(@event);

            if (_configuration.HandlerActions.TryGetValue(@event.GetType(), out var handler))
            {
                handler.Invoke(@event);
                return new[] { Task.CompletedTask };
            }

            return base.Publish(@event);
        }

        public override void Publish(IEvent @event)
        {
            _tracer.Events.Add(@event);

            if (_configuration.HandlerActions.TryGetValue(@event.GetType(), out var handler))
            {
                handler.Invoke(@event);
            }
            else
            {
                base.Publish(@event);
            }
        }

        public override void Send(IRequest request)
        {
            _tracer.Requests.Add(request);

            if (_configuration.HandlerActions.TryGetValue(request.GetType(), out var handler))
                handler.Invoke(request);
            else
                base.Send(request);
        }

        public override TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            _tracer.Requests.Add(request);

            return _configuration.HandlerFuncs.TryGetValue(request.GetType(), out var handler)
                ? (TResponse)handler.Invoke(request)
                : base.Send(request);
        }
    }
}