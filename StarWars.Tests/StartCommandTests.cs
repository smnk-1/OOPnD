using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

namespace StarWars.Tests;

public class StartCommandTest
{
    public StartCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var iocScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void StartCmdTest()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Test", (object[] args) => new EmptyCommand()).Execute();

        var gameObject = new Mock<IDictionary<string, object>>();
        var queue = new Mock<ICommandReceiver>();
        var commandType = "Test";

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Macro", (object[] args) => new MacroCommand((List<Hwdtech.ICommand>)args[0])).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Send", (object[] args) => new SendCommand((Hwdtech.ICommand)args[1], (ICommandReceiver)args[0])).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.CommandInjectable", (object[] args) => new CommandInjectableCommand()).Execute();

        var startCmd = new StartCommand(gameObject.Object, queue.Object, commandType);
        startCmd.Execute();

        queue.Verify(q => q.Receive(It.Is<MacroCommand>(c => c.cmds.Count() == 2 && c.cmds.ToArray()[0] is EmptyCommand &&
        c.cmds.ToArray()[1] is SendCommand)), Times.Once());
    }
}
