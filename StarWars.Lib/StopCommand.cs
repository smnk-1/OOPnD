namespace StarWars.Lib;

public class StopCommand : Hwdtech.ICommand
{
    private readonly IDictionary<string, object> _gameObject;
    private readonly string _cmdType;
    public StopCommand(IDictionary<string, object> gameObject, string commandType)
    {
        _gameObject = gameObject;
        _cmdType = commandType;
    }
    public void Execute()
    {
        var injectable = (CommandInjectableCommand)_gameObject[$"repeatable{_cmdType}"];
        injectable.Inject(new EmptyCommand());
    }
}
