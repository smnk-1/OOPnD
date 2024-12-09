using Hwdtech;
namespace StarWars.Lib;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Adapters.IMoving", 
            (Func<object, IMoving>)(obj =>
            {
                return (IMoving)obj;
            }));

        IoC.Resolve<ICommand>("IoC.Register", "Commands.Move", 
            (Func<object, object>)(obj =>
            {
                return new MoveCommand(IoC.Resolve<IMoving>("Adapters.IMoving", obj));
            }));

    }
}
