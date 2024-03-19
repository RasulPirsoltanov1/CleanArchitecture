namespace Application.FunctionalTests;

public abstract class BaseTestFixture
{

    [SetUp]
    public async Task TestSetup()
    {
        await Testing.ResetState();
    }
}
