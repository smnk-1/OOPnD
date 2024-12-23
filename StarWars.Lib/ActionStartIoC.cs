using Hwdtech;

namespace StarWars.Lib;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Actions.Start", (object[] args) =>
        {
            if (args.Length != 1 || args[0] is not IDictionary<string, object> order)
            {
                throw new ArgumentException("Expected a single argument of type IDictionary<string, object>.");
            }

            var commandNames = order.ContainsKey("Commands") && order["Commands"] is IEnumerable<string> commands
                ? commands
                : throw new InvalidOperationException("Order must contain a 'Commands' key with a list of command names.");

            var commandsArray = commandNames
                .Select(name => IoC.Resolve<StarWars.Lib.ICommand>(name))
                .ToArray();

            return new MacroCommand(commandsArray);
        }).Execute();
    }
}
