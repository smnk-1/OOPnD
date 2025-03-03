using Hwdtech;

namespace StarWars.Lib;

public class RegisterDependenciesGameItem : Hwdtech.ICommand
{
    public void Execute()
    {
        var gameItems = new Dictionary<string, IDictionary<string, object>>();

        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            "GameItem",
            (object[] args) => gameItems[(string)args[0]]
        ).Execute();

        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            "GameItem.Add",
            (object[] args) => IoC.Resolve<Hwdtech.ICommand>(
                "Commands.Add",
                gameItems,
                (string)args[0], 
                (IDictionary<string, object>)args[1])
        ).Execute();

        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            "GameItem.Remove",
            (object[] args) => IoC.Resolve<Hwdtech.ICommand>(
                "Commands.Remove",
                gameItems,
                (string)args[0])
        ).Execute();
    }
}
