using Hwdtech;
using Hwdtech.Ioc;
using StarWars.Lib;

public class RegisterDependencyCommandInjectableCommandTests
{
    public RegisterDependencyCommandInjectableCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
    }

    [Fact]
    public void Execute_RegistersCommandInjectableCommandDependency()
    {
        var iocScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", iocScope).Execute();

        new InitScopeBasedIoCImplementationCommand().Execute();
        new RegisterDependencyCommandInjectableCommand().Execute();

        var command = IoC.Resolve<Hwdtech.ICommand>("Commands.CommandInjectable");
        Assert.NotNull(command);
        Assert.IsType<CommandInjectableCommand>(command);

        var injectable = IoC.Resolve<ICommandInjectable>("Commands.CommandInjectable");
        Assert.NotNull(injectable);
        Assert.IsType<CommandInjectableCommand>(injectable);

        var concreteCommand = IoC.Resolve<CommandInjectableCommand>("Commands.CommandInjectable");
        Assert.NotNull(concreteCommand);
    }
}
