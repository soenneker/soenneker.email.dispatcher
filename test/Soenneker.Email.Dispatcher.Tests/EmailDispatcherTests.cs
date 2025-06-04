using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Email.Dispatcher.Tests;

[Collection("Collection")]
public sealed class EmailDispatcherTests : FixturedUnitTest
{

    public EmailDispatcherTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {

    }

    [Fact]
    public void Default()
    {

    }
}
