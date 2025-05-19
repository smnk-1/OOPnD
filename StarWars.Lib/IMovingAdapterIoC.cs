using Hwdtech;
namespace StarWars.Lib;

public class RegisterIoCDependencyIMovingAdapter : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<IMoving>(
            "IoC.Register",
            "Adapters.IMoving",
            (Func<object, object>)(obj =>
            {
                return (IMoving)IoC.Resolve<object>("Adapters.Generate", obj, typeof(IMoving));
            }));
    }
}
