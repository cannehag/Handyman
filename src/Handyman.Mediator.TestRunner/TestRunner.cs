namespace Handyman.Mediator.TestRunner
{
    public class TestRunner
    {
        public TestRunner()
        {
            var configuration = new Configuration();
            var tracer = new Tracer();
            var mediator = new TestMediator(configuration, tracer);

            Configure = new Configurator(mediator, configuration);
            Run = mediator;
            Verify = tracer;
        }

        public IConfig Configure { get; }
        public IMediator Run { get; }
        public IVerify Verify { get; }
    }
}
