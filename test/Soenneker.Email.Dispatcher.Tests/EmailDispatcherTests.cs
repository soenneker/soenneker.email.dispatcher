using Soenneker.Email.Dispatcher.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Email.Dispatcher.Tests;

[Collection("Collection")]
public sealed class EmailDispatcherTests : FixturedUnitTest
{
    private readonly IEmailDispatcher _util;

    public EmailDispatcherTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IEmailDispatcher>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
