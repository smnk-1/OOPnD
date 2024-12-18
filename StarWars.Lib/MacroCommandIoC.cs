using Hwdtech;
namespace StarWars.Lib;

public class RegisterIoCDependencyMacroCommand : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            "Commands.Macro",
            (object[] args) => (Hwdtech.ICommand)new MacroCommand(args.Select(x => (StarWars.Lib.ICommand)x).ToArray())).Execute();
    }
}
