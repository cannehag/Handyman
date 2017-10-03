﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handyman.Mediator
{
    public class Mediator : IMediator
    {
        private readonly Func<Type, object> _getService;
        private readonly Func<Type, IEnumerable<object>> _getServices;
        private readonly ConcurrentDictionary<Type, CallContext> _contexts = new ConcurrentDictionary<Type, CallContext>();

        public Mediator(Func<Type, object> getService, Func<Type, IEnumerable<object>> getServices)
        {
            _getService = getService;
            _getServices = getServices;
        }

        public virtual void Publish(IEvent @event)
        {
            var eventType = @event.GetType();
            foreach (var handler in GetEventHandlers(eventType))
                handler.Handle(@event);
        }

        public virtual IEnumerable<Task> Publish(IAsyncEvent @event)
        {
            var eventType = @event.GetType();
            return GetAsyncEventHandlers(eventType)
                .Select(handler => handler.Handle(@event))
                .ToList();
        }

        public virtual void Send(IRequest request)
        {
            var requestType = request.GetType();
            var handler = GetRequestHandler(requestType);
            handler.Handle(request);
        }

        public virtual TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var requestType = request.GetType();
            var handler = GetRequestHandler<TResponse>(requestType);
            return handler.Handle(request);
        }

        private IEnumerable<IEventHandler<IEvent>> GetEventHandlers(Type eventType)
        {
            var context = _contexts.GetOrAdd(eventType, CallContextFactory.GetEventCallContext);
            foreach (var handler in _getServices.Invoke(context.HandlerInterface))
            {
                yield return (IEventHandler<IEvent>)context.AdapterFactory.Invoke(handler);
            }
        }

        private IEnumerable<IAsyncEventHandler<IAsyncEvent>> GetAsyncEventHandlers(Type eventType)
        {
            var context = _contexts.GetOrAdd(eventType, CallContextFactory.GetAsyncEventCallContext);
            foreach (var handler in _getServices.Invoke(context.HandlerInterface))
            {
                yield return (IAsyncEventHandler<IAsyncEvent>)context.AdapterFactory.Invoke(handler);
            }
        }

        private IRequestHandler<IRequest> GetRequestHandler(Type requestType)
        {
            var context = _contexts.GetOrAdd(requestType, CallContextFactory.GetRequestCallContext);
            var handler = _getService.Invoke(context.HandlerInterface);
            return (IRequestHandler<IRequest>)context.AdapterFactory.Invoke(handler);
        }

        private IRequestHandler<IRequest<TResponse>, TResponse> GetRequestHandler<TResponse>(Type requestType)
        {
            var context = _contexts.GetOrAdd(requestType, CallContextFactory.GetRequestResponseCallContext<TResponse>);
            var handler = _getService.Invoke(context.HandlerInterface);
            return (IRequestHandler<IRequest<TResponse>, TResponse>)context.AdapterFactory.Invoke(handler);
        }
    }
}