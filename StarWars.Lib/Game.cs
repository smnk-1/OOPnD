using System.Diagnostics;
using Hwdtech;
namespace StarWars.Lib;

public class Game : Hwdtech.ICommand
{
    private readonly object _scope;
    public bool stop;
    private readonly Stopwatch _stopwatch;

    public Game(object scope)
    {
        _scope = scope;
        _stopwatch = new Stopwatch();
    }

    public void Execute()
    {
        _stopwatch.Reset();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", _scope).Execute();

        var cmdsTime = IoC.Resolve<TimeSpan>("Command.Time");

        while (IoC.Resolve<Func<int>>("Game.Queue.Count")() > 0 && _stopwatch.Elapsed <= cmdsTime)
        {
            _stopwatch.Start();
            var cmd = IoC.Resolve<Hwdtech.ICommand>("Game.Queue.NextCommand");
            try
            {
                cmd.Execute();
            }
            catch (Exception e)
            {
                IoC.Resolve<Hwdtech.ICommand>("ExceptionHandler", cmd, e).Execute();
            }
            _stopwatch.Stop();
        }
    }
}
