using System;
using System.Threading.Tasks;

namespace Handyman.Mediator.TestRunner
{
    internal class Configurator : IConfig
    {
        private readonly TestMediator _mediator;
        private readonly Configuration _configuration;

        public Configurator(TestMediator mediator, Configuration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        public void ServiceProvider(Func<IMediator, object> func)
        {
            var target = func(_mediator);
            var sp = Handyman.Mediator.TestRunner.ServiceProvider.Create(target);
            _mediator.ServiceProviderProxy.ServiceProvider = sp;
        }

        public void HandleEvent<TEvent>() where TEvent : IEvent
        {
            HandleEvent<TEvent>(_ => { });
        }

        public void HandleEvent<TEvent>(Action<TEvent> action) where TEvent : IEvent
        {
            _configuration.HandlerActions.Add(typeof(TEvent), o => action((TEvent)o));
        }

        public void HandleRequest<TRequest>() where TRequest : IRequest
        {
            _configuration.HandlerActions.Add(typeof(TRequest), _ => { });
        }

        public void HandleRequest<TRequest, TResponse>(TResponse response) where TRequest : IRequest<TResponse>
        {
            HandleRequest<TRequest, TResponse>(() => response);
        }

        public void HandleRequest<TRequest, TResponse>(Func<TResponse> func) where TRequest : IRequest<TResponse>
        {
            HandleRequest<TRequest, TResponse>(_ => func());
        }

        public void HandleRequest<TRequest, TResponse>(Func<TRequest, TResponse> func) where TRequest : IRequest<TResponse>
        {
            _configuration.HandlerFuncs.Add(typeof(TRequest), request => func((TRequest)request));
        }

        public void HandleAsyncRequest<TRequest>() where TRequest : IAsyncRequest
        {
            HandleRequest<TRequest, Task>(Task.CompletedTask);
        }

        public void HandleAsyncRequest<TRequest, TResponse>(TResponse response) where TRequest : IAsyncRequest<TResponse>
        {
            HandleAsyncRequest<TRequest, TResponse>(() => response);
        }

        public void HandleAsyncRequest<TRequest, TResponse>(Func<TResponse> func) where TRequest : IAsyncRequest<TResponse>
        {
            HandleAsyncRequest<TRequest, TResponse>(_ => func());
        }

        public void HandleAsyncRequest<TRequest, TResponse>(Func<TRequest, TResponse> func) where TRequest : IAsyncRequest<TResponse>
        {
            _configuration.HandlerFuncs.Add(typeof(TRequest), request => Task.FromResult(func((TRequest)request)));
        }
    }
}