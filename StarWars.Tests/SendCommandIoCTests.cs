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
        var mockGameObject = new Mock<IDictionary<string, object>>();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.ICommand",
            (Func<object, StarWars.Lib.ICommand>)(obj =>
            {
                return mockCommand.Object;
            })).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.ICommandReceiver",
            (Func<object, ICommandReceiver>)(obj =>
            {
                return mockReceiver.Object;
            })).Execute();

        new RegisterIoCDependencySendCommand().Execute();

        var sendCommand = IoC.Resolve<StarWars.Lib.ICommand>("Commands.Send", mockGameObject.Object);

        Assert.NotNull(sendCommand);
        Assert.IsType<SendCommand>(sendCommand);
    }
}
