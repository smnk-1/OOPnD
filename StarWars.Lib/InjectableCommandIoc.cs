using Hwdtech;

namespace StarWars.Lib
{
    public class RegisterDependencyCommandInjectableCommand : ICommand
    {
        public void Execute()
        {
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.CommandInjectable",
            (Func<object, object>)(obj =>
            new CommandInjectableCommand())).Execute();
        }
    }
}
