using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Controllers.Alerts.Tests;

[Collection("Collection")]
public class AlertsControllerTests : FixturedUnitTest
{

    public AlertsControllerTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {

    }
}
