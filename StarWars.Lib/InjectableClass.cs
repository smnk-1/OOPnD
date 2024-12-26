namespace StarWars.Lib;

public interface ICommandInjectable
{
    void Inject(Hwdtech.ICommand command);
}
public class CommandInjectableCommand : Hwdtech.ICommand, ICommandInjectable
{
    private Hwdtech.ICommand? _injectedCommand;

    public CommandInjectableCommand() { }

    public void Inject(Hwdtech.ICommand command)
    {
        _injectedCommand = command;
    }

    public void Execute()
    {
        if (_injectedCommand == null)
        {
            throw new InvalidOperationException("Command not injected.");
        }

        _injectedCommand.Execute();
    }
}
