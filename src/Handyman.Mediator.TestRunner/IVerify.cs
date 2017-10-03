using System.Collections.Generic;

namespace Handyman.Mediator.TestRunner
{
    public interface IVerify
    {
        IReadOnlyList<object> PublishedEvents { get; }
        IReadOnlyList<object> SentRequests { get; }
    }
}