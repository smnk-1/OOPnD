public class Game : ICommand
{
    private readonly object _scope;

    public Game(object scope)
    {
        _scope = scope;
    }

    public void Execute()
    {
        IoC.Resolve<ICommand>("Scopes.Current.Set", _scope).Execute();

        var queue = IoC.Resolve<IQueue>("Game.Queue");

        while (!ShouldStop(queue))
        {
            var cmd = IoC.Resolve<ICommand>("Game.Scheduler.NextCommand");
            try
            {
                cmd.Execute();
            }
            catch (Exception e)
            {
                IoC.Resolve<ICommand>("ExceptionHandler", cmd, e).Execute();
            }
        }
    }

    private bool ShouldStop(IQueue queue)
    {
        return queue.IsEmpty();
    }
}
