using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

namespace StarWars.Tests;

public class RegisterIoCDependencyActionsStartTest
{
    public RegisterIoCDependencyActionsStartTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var iocScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void Execute_ShouldRegisterAndResolveActionsStart()
    {
        var order = new Dictionary<string, object>
            {
                { "gameObject", new Dictionary<string, object>() },
                { "queue",  new Mock<ICommandReceiver>().Object},
                { "cmdType", "SomeCommandType" }
            };

        new RegisterIoCDependencyActionsStart().Execute();

        var resolvedCommand = IoC.Resolve<Hwdtech.ICommand>("Actions.Start", order);

        Assert.NotNull(resolvedCommand);
        Assert.IsType<StartCommand>(resolvedCommand);
    }
}
