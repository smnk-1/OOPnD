using Hwdtech;
namespace StarWars.Lib;

public class RegisterIoCDependencyMacroCommand : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.MacroCommand",
            (Func<object, object>)(obj =>
            new MacroCommand(IoC.Resolve<ICommand>("Adapters.MacroCommand", obj)))).Execute();
    }
}
