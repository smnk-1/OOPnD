using Hwdtech;

namespace StarWars.Lib;

public class RegisterIoCDependencyMacroMoveRotate : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            "Macro.Move",
            (object[] args) => new CreateMacroCommandStrategy("Move").Resolve(args)
        ).Execute();

        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            "Macro.Rotate",
            (object[] args) => new CreateMacroCommandStrategy("Rotate").Resolve(args)
        ).Execute();
    }
}
