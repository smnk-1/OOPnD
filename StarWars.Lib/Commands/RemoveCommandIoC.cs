using Hwdtech;

namespace StarWars.Lib;

public class RegisterIoCDependencyRemoveCommand : Hwdtech.ICommand
{

    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Remove",
            (Func<object[], object>)(obj =>
                new RemoveCommand(
                    (Dictionary<string, IDictionary<string, object>>)obj[0], 
                    (object)obj[1]
                )
            )).Execute();
    }
}
