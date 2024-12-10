using Hwdtech;
namespace StarWars.Lib;

public class RegisterIoCDependencyMoveCommand : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Move",
            (Func<object, object>)(obj => 
            new MoveCommand(IoC.Resolve<IMoving>("Adapters.IMoving", obj)))).Execute();
    }
}
