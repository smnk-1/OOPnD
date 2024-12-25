using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

namespace StarWars.Test;
public class RegisterIoCDependencySendCommandTests
{
    public RegisterIoCDependencySendCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
    }

    [Fact]
    public void Execute_ShouldRegisterSendCommandDependency()
    {
        var iocScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", iocScope).Execute();

        var mockCommand = new Mock<StarWars.Lib.ICommand>();
        var mockReceiver = new Mock<ICommandReceiver>();

        new RegisterIoCDependencySendCommand().Execute();

        var sendCommand = IoC.Resolve<StarWars.Lib.ICommand>("Commands.Send", new object[] { mockCommand.Object, mockReceiver.Object });

        Assert.NotNull(sendCommand);
        Assert.IsType<SendCommand>(sendCommand);
    }
}
