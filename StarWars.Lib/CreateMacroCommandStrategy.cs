using Hwdtech;
using StarWars.Lib;

public class CreateMacroCommandStrategy
{
    private readonly string commandSpec;

    public CreateMacroCommandStrategy(string commandSpec)
    {
        this.commandSpec = commandSpec;
    }

    public StarWars.Lib.ICommand Resolve()
    {
        if (IoC.Resolve<object>("Specs." + commandSpec) is not string[] commandNames)
        {
            throw new InvalidOperationException($"No specification found for command '{commandSpec}'.");
        }

        var commands = commandNames.Select(name => IoC.Resolve<StarWars.Lib.ICommand>(name)).ToArray();

        if (!commands.Any())
        {
            throw new InvalidOperationException($"No commands found for macro-command '{commandSpec}'.");
        }

        return new MacroCommand(commands);
    }
}
