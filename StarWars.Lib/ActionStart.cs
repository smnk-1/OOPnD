using Hwdtech;

namespace StarWars.Lib;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Actions.Start", (object[] args) =>
        {
            var order = (IDictionary<string, object>)args[0];

            return new StartCommand(
                (IDictionary<string, object>)order["gameObject"],
                (ICommandReceiver)order["queue"],
                (string)order["cmdType"]);
        }).Execute();
    }
}
