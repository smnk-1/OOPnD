using Hwdtech;
using StarWars.Lib;

public class CreateMacroCommandStrategy
{
    private readonly string commandSpec;

    public CreateMacroCommandStrategy(string commandSpec)
    {
        this.commandSpec = commandSpec ?? throw new ArgumentNullException(nameof(commandSpec));
    }

    public Hwdtech.ICommand Resolve(object[] args)
    {
        var commandNames = IoC.Resolve<IEnumerable<string>>("Specs." + commandSpec)
                            ?? Enumerable.Empty<string>();

        var commands = commandNames.Select(name => IoC.Resolve<Hwdtech.ICommand>(name, args)).ToArray();

        return IoC.Resolve<Hwdtech.ICommand>("Commands.Macro", commands.Cast<object>().ToArray());
    }
}
