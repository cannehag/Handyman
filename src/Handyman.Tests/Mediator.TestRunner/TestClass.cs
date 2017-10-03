using Handyman.Mediator;
using System;
using System.Linq;
using Xunit;

namespace Handyman.Tests.Mediator.TestRunner
{
    public class TestClass
    {
        [Fact]
        public void TestMethod()
        {
            var tr = new Handyman.Mediator.TestRunner.TestRunner();
            tr.Configure.ServiceProvider(m => new SP(m));
            tr.Configure.HandleRequest<Request, string>("success");
            var s = tr.Run.Send(new Request());
            var request = tr.Verify.SentRequests.Single();
        }

        class SP
        {
            private readonly IMediator _mediator;

            public SP(IMediator mediator)
            {
                _mediator = mediator;
            }

            public object GetService(Type type)
            {
                if (type == typeof(IMediator)) return _mediator;
                throw new NotSupportedException();
            }
        }

        class Request : IRequest<string> { }
    }
}
