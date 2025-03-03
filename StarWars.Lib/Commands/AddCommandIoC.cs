using Hwdtech;

namespace StarWars.Lib;

public class RegisterIoCDependencyAddCommand : Hwdtech.ICommand
{

    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Add",
            (Func<object[], object>)(obj =>
                new AddCommand(
                    (Dictionary<string, IDictionary<string, object>>)obj[0],
                    (object)obj[1],
                    (IDictionary<string, object>)obj[2]
                )
            )).Execute();
    }
}
