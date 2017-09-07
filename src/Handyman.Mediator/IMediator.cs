﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Handyman.Mediator
{
    public interface IMediator
    {
        Task Send(IRequest request);
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
        IEnumerable<Task> Publish(IMessage message);
    }
}