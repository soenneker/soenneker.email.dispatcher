using Soenneker.Tests.HostedUnit;

namespace Soenneker.Email.Dispatcher.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class EmailDispatcherTests : HostedUnitTest
{

    public EmailDispatcherTests(Host host) : base(host)
    {

    }

    [Test]
    public void Default()
    {

    }
}
