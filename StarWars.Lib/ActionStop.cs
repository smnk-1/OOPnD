using Hwdtech;

using StarWars.Lib;

public class RegisterIoCDependencyActionsStop : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Actions.Stop", (object[] args) =>
        {
            var order = (IDictionary<string, object>)args[0];

            return new StopCommand(
                (IDictionary<string, object>)order["gameObject"],
                (string)order["commandType"]);
        }).Execute();
    }
}
