using Hwdtech;

namespace StarWars.Lib
{
    public class RegisterIoCDependencyMacroCommand : Hwdtech.ICommand
    {
        public void Execute()
        {
            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Commands.Macro",
                (object[] args) => new MacroCommand(args.OfType<Hwdtech.ICommand>().ToArray())
            ).Execute();
        }
    }
}
