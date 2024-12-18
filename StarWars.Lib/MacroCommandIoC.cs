using Hwdtech;
namespace StarWars.Lib;

public class RegisterIoCDependencyMacroCommand : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Commands.Macro",
            (object[] args) => (ICommand)new MacroCommand(args.Select(x => (ICommand)x).ToArray())).Execute();
    }
}
