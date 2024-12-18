using StarWars.Lib;

namespace StarWars.Test;

public class CommandInjectableCommandTests
{
    [Fact]
    public void Execute_CallsInjectedCommand()
    {
        var mockCommand = new MockCommand();
        var commandInjectableCommand = new CommandInjectableCommand();
        commandInjectableCommand.Inject(mockCommand);

        commandInjectableCommand.Execute();

        Assert.True(mockCommand.Executed);
    }

    [Fact]
    public void Execute_ThrowsException_IfCommandNotInjected()
    {
        var commandInjectableCommand = new CommandInjectableCommand();

        Assert.Throws<InvalidOperationException>(() => commandInjectableCommand.Execute());
    }
    private class MockCommand : ICommand
    {
        public bool Executed { get; private set; } = false;
        public void Execute() => Executed = true;
    }
}
