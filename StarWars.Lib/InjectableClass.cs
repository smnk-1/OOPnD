namespace StarWars.Lib;

public interface ICommandInjectable
{
    void Inject(ICommand command);
    void Execute();
}

public class CommandInjectableCommand : ICommand, ICommandInjectable
{
    private ICommand? _injectedCommand;

    public CommandInjectableCommand() { }

    public void Inject(ICommand command)
    {
        _injectedCommand = command;
    }

    public void Execute()
    {
        _injectedCommand?.Execute();
    }
}


