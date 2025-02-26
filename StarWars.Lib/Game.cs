using Hwdtech;
namespace StarWars.Lib;

public class Game : Hwdtech.ICommand
{
    private readonly object _scope;
    public bool stop;

    public Game(object scope)
    {
        _scope = scope;
    }

    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", _scope).Execute();
        _ = IoC.Resolve<ICommandReceiver>("Game.Queue");

        while (!stop)
        {
            var cmd = IoC.Resolve<Hwdtech.ICommand>("Game.Scheduler.NextCommand");
            try
            {
                cmd.Execute();
            }
            catch (Exception e)
            {
                IoC.Resolve<Hwdtech.ICommand>("ExceptionHandler", cmd, e).Execute();
            }
        }
    }
}
