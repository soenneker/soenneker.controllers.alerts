using Soenneker.Tests.HostedUnit;

namespace Soenneker.Controllers.Alerts.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class AlertsControllerTests : HostedUnitTest
{

    public AlertsControllerTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}
