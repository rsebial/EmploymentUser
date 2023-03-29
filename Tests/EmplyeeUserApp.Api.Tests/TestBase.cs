using EmployeeUserApp.Api.Controllers;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

[assembly: ExcludeFromCodeCoverage]
namespace EmployeeUserApp.Api.Tests;

public abstract class TestBase
{
    protected TestBase()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        Fixture = fixture;
    }

    protected IFixture Fixture { get; }

    [OneTimeSetUp]
    protected static void InitMapTest()
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(UserController).Assembly);
    }
}