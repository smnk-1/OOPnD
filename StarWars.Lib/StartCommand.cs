using Hwdtech;

namespace StarWars.Lib;

public class StartCommand : Hwdtech.ICommand
{
    private readonly IDictionary<string, object> _gameObject;
    private readonly ICommandReceiver _queue;
    private readonly string _cmdType;

    public StartCommand(IDictionary<string, object> gameObject, ICommandReceiver queue, string commandType)
    {
        _gameObject = gameObject;
        _queue = queue;
        _cmdType = commandType;
    }
    public void Execute()
    {
        var cmd = IoC.Resolve<Hwdtech.ICommand>($"Commands.{_cmdType}", _gameObject);

        var injectable = IoC.Resolve<CommandInjectableCommand>("Commands.CommandInjectable");
        var sendcmd = IoC.Resolve<Hwdtech.ICommand>("Commands.Send", _queue, injectable);
        var macrocmd = IoC.Resolve<Hwdtech.ICommand>("Commands.Macro", new List<Hwdtech.ICommand> { cmd, sendcmd });

        injectable.Inject(macrocmd);
        _gameObject[$"repeatable{_cmdType}"] = injectable;

        _queue.Receive(macrocmd);
    }
}
