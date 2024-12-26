using Hwdtech;

namespace StarWars.Lib
{
    public class RegisterDependencyCommandInjectableCommand : Hwdtech.ICommand
    {
        public void Execute()
        {
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.CommandInjectable",
            (Func<object, object>)(obj =>
            new CommandInjectableCommand())).Execute();
        }
    }
}
