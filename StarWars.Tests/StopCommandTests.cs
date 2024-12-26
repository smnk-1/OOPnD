using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

namespace StarWars.Tests;

public class StopCommandTests
{
    public StopCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var iocScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void StopCmdTest()
    {
        var gameObject = new Dictionary<string, object>();
        var cmdType = "Type";
        var injectableCommand = new Mock<CommandInjectableCommand>();
        gameObject[$"repeatable{cmdType}"] = injectableCommand.Object;

        var stopCommand = new StopCommand(gameObject, cmdType);
        stopCommand.Execute();

        Assert.IsType<EmptyCommand>(injectableCommand.Object._injectedCommand);
    }
}