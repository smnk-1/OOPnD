using Hwdtech;
using StarWars.Lib;

public class CreateMacroCommandStrategy
{
    private readonly string commandSpec;

    public CreateMacroCommandStrategy(string commandSpec)
    {
        this.commandSpec = commandSpec;
    }

    public Hwdtech.ICommand Resolve(object[] args)
    {
        var commandNames = IoC.Resolve<object>("Specs." + commandSpec) as string[]
            ?? Array.Empty<string>();

        if (!commandNames.Any())
        {
            throw new InvalidOperationException($"No commands specified for macro-command '{commandSpec}'.");
        }

        var commands = commandNames.Select(name => IoC.Resolve<Hwdtech.ICommand>(name)).ToArray();
        return new MacroCommand(commands);
    }
}
