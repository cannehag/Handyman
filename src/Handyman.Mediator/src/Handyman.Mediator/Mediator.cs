﻿using Handyman.Mediator.Internals;
using System.Threading;
using System.Threading.Tasks;

namespace Handyman.Mediator
{
    public class Mediator : IMediator
    {
        private readonly Providers _providers;

        public Mediator(ServiceProvider serviceProvider)
            : this(serviceProvider, new Configuration())
        {
        }

        public Mediator(ServiceProvider serviceProvider, Configuration configuration)
        {
            _providers = new Providers
            {
                EventHandlerProvider = configuration.GetEventHandlerProvider(),
                EventPipelineHandlerProvider = configuration.GetEventPipelineHandlerProvider(),
                RequestHandlerProvider = configuration.GetRequestHandlerProvider(),
                RequestPipelineHandlerProvider = configuration.GetRequestPipelineHandlerProvider(),
                ServiceProvider = serviceProvider
            };
        }

        public Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken)
            where TEvent : IEvent
        {
            return EventProcessor.Process(@event, _providers, cancellationToken);
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            return RequestProcessorProvider.GetRequestProcessor(request).Process(request, _providers, cancellationToken);
        }
    }
}