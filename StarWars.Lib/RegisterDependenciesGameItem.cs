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
            (Func<object[], object>)(args =>
            {
                gameItems.Add((string)args[0], (IDictionary<string, object>)args[1]);
                return null;
            })
        ).Execute();

        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            "GameItem.Remove",
            (Func<object[], object>)(args =>
            {
                gameItems.Remove((string)args[0]);
                return null;
            })
        ).Execute();

    }
}
