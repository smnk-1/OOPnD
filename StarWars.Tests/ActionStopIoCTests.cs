using Hwdtech;
using Hwdtech.Ioc;
using StarWars.Lib;

public class RegisterIoCDependencyActionsStopTests
{
    public RegisterIoCDependencyActionsStopTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
    }

    [Fact]
    public void Execute_ShouldRegisterActionsStopDependency()
    {
        var iocScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", iocScope).Execute();

        var order = new Dictionary<string, object>();

        new RegisterIoCDependencyActionsStop().Execute();

        var stopCommand = IoC.Resolve<StarWars.Lib.ICommand>("Actions.Stop", order);

        Assert.NotNull(stopCommand);
        Assert.IsType<MacroCommand>(stopCommand);

        stopCommand.Execute();
    }
}
