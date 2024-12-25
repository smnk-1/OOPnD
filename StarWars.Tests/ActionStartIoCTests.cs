using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

public class RegisterIoCDependencyActionsStartTests
{
    public RegisterIoCDependencyActionsStartTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void Execute_ShouldRegisterActionsStartDependency()
    {

        var iocScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", iocScope).Execute();

        var command_1 = new Mock<Hwdtech.ICommand>();
        var command_2 = new Mock<Hwdtech.ICommand>();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Command1", (object[] args) => command_1.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Command2", (object[] args) => command_2.Object).Execute();

        var order = new Dictionary<string, object>
        {
            { "Commands", new[] { "Command1", "Command2" } }
        };

        new RegisterIoCDependencyActionsStart().Execute();

        var macroCommand = IoC.Resolve<Hwdtech.ICommand>("Actions.Start", order);

        Assert.NotNull(macroCommand);
        Assert.IsType<MacroCommand>(macroCommand);

        macroCommand.Execute();
        command_1.Verify(c => c.Execute(), Times.Once());
        command_2.Verify(c => c.Execute(), Times.Once());
    }

    [Fact]
    public void Execute_ShouldThorowInvalidOperationException()
    {

        var iocScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", iocScope).Execute();

        var order = new Dictionary<string, object> { };

        new RegisterIoCDependencyActionsStart().Execute();

        Assert.Throws<InvalidOperationException>(() => IoC.Resolve<Hwdtech.ICommand>("Actions.Start", order));
    }

    [Fact]
    public void Execute_ShouldThrowArgumentException()
    {
        var iocScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", iocScope).Execute();

        var order = new object[] { "invalid_argument" };

        new RegisterIoCDependencyActionsStart().Execute();

        Assert.Throws<ArgumentException>(() => IoC.Resolve<Hwdtech.ICommand>("Actions.Start", order));
    }
}
