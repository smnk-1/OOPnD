using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

public class RegisterIoCDependencyActionsStartTests
{
    public RegisterIoCDependencyActionsStartTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
    }

    [Fact]
    public void Execute_ShouldRegisterActionsStartDependency()
    {

        var iocScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", iocScope).Execute();

        var command_1 = new Mock<StarWars.Lib.ICommand>();
        var command_2 = new Mock<StarWars.Lib.ICommand>();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Command1", (object[] args) => command_1.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Command2", (object[] args) => command_2.Object).Execute();

        var order = new Dictionary<string, object>
        {
            { "Commands", new[] { "Command1", "Command2" } }
        };

        new RegisterIoCDependencyActionsStart().Execute();

        var macroCommand = IoC.Resolve<StarWars.Lib.ICommand>("Actions.Start", order);

        Assert.NotNull(macroCommand);
        Assert.IsType<MacroCommand>(macroCommand);

        macroCommand.Execute();
        command_1.Verify(c => c.Execute(), Times.Once());
        command_2.Verify(c => c.Execute(), Times.Once());
    }
}
