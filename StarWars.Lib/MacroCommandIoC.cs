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
                new Func<object[], MacroCommand>(args =>
                {
                    return new MacroCommand((Hwdtech.ICommand[])args);
                })
            ).Execute();
        }
    }
}