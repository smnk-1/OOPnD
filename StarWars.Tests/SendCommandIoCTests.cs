using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

namespace StarWars.Test;
public class SendCommandIoCTests
{
    public SendCommandIoCTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
    }

    [Fact]
    public void Execute_ShouldRegisterSendCommandDependency()
    {
        var mockCommand = new Mock<Hwdtech.ICommand>();
        var mockReceiver = new Mock<ICommandReceiver>();
        var mockGameObject = new Mock<IDictionary<string, object>>();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.ICommand",
            (Func<object, Hwdtech.ICommand>)(obj =>
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