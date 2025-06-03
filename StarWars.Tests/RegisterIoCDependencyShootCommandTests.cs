using StarWars.Tests;

using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

namespace StarWars.Tests;

public class RegisterIoCDependencyShootCommandTests
{
    public RegisterIoCDependencyShootCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>(
                "Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            )
            .Execute();
    }

    [Fact]
    public void RegisterIoCDependencyShootCommandResolvePositiveTest()
    {
        new RegisterShootDependency().Execute();
        var shootingObj = new Mock<IWeapon>().Object;
        var resolvedCommand = IoC.Resolve<Hwdtech.ICommand>("Command.Shoot", shootingObj);

        Assert.NotNull(resolvedCommand);
        Assert.IsType<ShootCommand>(resolvedCommand);
    }

    [Fact]
    public void RegisterIoCDependencyShootCommandResolveNegativeTest()
    {
        Assert.ThrowsAny<Exception>(() => IoC.Resolve<Hwdtech.ICommand>("Command.Shoot"));
    }
}