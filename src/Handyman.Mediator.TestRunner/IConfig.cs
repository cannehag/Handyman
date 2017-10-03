using System;

namespace Handyman.Mediator.TestRunner
{
    public interface IConfig
    {
        void ServiceProvider(Func<IMediator, object> func);

        void HandleEvent<TEvent>() where TEvent : IEvent;
        void HandleEvent<TEvent>(Action<TEvent> action) where TEvent : IEvent;

        void HandleRequest<TRequest>() where TRequest : IRequest;
        void HandleRequest<TRequest, TResponse>(TResponse response) where TRequest : IRequest<TResponse>;
        void HandleRequest<TRequest, TResponse>(Func<TResponse> func) where TRequest : IRequest<TResponse>;
        void HandleRequest<TRequest, TResponse>(Func<TRequest, TResponse> func) where TRequest : IRequest<TResponse>;
        void HandleAsyncRequest<TRequest>() where TRequest : IAsyncRequest;
        void HandleAsyncRequest<TRequest, TResponse>(TResponse response) where TRequest : IAsyncRequest<TResponse>;
        void HandleAsyncRequest<TRequest, TResponse>(Func<TResponse> func) where TRequest : IAsyncRequest<TResponse>;
        void HandleAsyncRequest<TRequest, TResponse>(Func<TRequest, TResponse> func) where TRequest : IAsyncRequest<TResponse>;
    }
}