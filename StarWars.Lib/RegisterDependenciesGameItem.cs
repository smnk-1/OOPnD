using Hwdtech;

namespace StarWars.Lib;

public class RegisterDependenciesGameItem: Hwdtech.ICommand
{
    public void Execute()
    {
        var gameItems = new Dictionary<string, Dictionary<string, object>>();

        IoC.Resolve<ICommand>(
            "IoC.Register", 
            "GameItem", 
            (object[] args) => gameItems[(string)args[0]]
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register", 
            "GameItem.Add",
            (object[] args) => gameItems.Add((string)args[0], (Dictionary<string, object>) args[1])
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register", 
            "GameItem.Remove",
            (object[] args) => gameItems.Remove((string)args[0])
        ).Execute();
    }
}
