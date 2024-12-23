using Hwdtech;

namespace StarWars.Lib;

public class RegisterIoCDependencyActionsStop : ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            "Actions.Stop",
            (object[] args) =>
            {
                if (args.Length != 1 || args[0] is not IDictionary<string, object> order)
                {
                    throw new ArgumentException("Invalid arguments for Actions.Stop");
                }

                return new MacroCommand(Array.Empty<StarWars.Lib.ICommand>());
            }
        ).Execute();
    }
}
