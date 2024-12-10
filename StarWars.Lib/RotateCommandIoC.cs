using Hwdtech;
namespace StarWars.Lib;
public class RegisterIoCDependencyRotateCommand : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Rotate",
            (Func<object, object>)(obj => 
            new RotateCommand(IoC.Resolve<IRotate>("Adapters.IRotate", obj)))).Execute();
    }
}