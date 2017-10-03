using System.Collections.Generic;

namespace Handyman.Mediator.TestRunner
{
    internal class Tracer : IVerify
    {
        public List<object> Events { get; } = new List<object>();
        public List<object> Requests { get; } = new List<object>();

        IReadOnlyList<object> IVerify.PublishedEvents => Events;
        IReadOnlyList<object> IVerify.SentRequests => Requests;
    }
}