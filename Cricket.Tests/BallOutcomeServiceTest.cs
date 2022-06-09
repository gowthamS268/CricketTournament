using Xunit;
namespace Cricket.Tests;

public class BallOutcomeServiceTest
{
    [Fact]
    public void TestGetEnergySupplier() {
        Assert.Equal(Supplier.TheGreenEco, _pricePlan.EnergySupplier);
    }

}