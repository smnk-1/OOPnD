using Hwdtech;
using Hwdtech.Ioc;
using StarWars.Lib;

namespace StarWars.Tests;

public class RegisterIoCDependencyActionsStopTests
{
    public RegisterIoCDependencyActionsStopTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var iocScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void ExecuteShouldResolveRegisterStopCommandTest()
    {
        var order = new Dictionary<string, object>();
        order["gameObject"] = new Dictionary<string, object>();
        order["commandType"] = "Type";

        new RegisterIoCDependencyActionsStop().Execute();
        var stopCommand = IoC.Resolve<Hwdtech.ICommand>("Actions.Stop", order);

        Assert.NotNull(stopCommand);
        Assert.IsType<StopCommand>(stopCommand);
    }
}
