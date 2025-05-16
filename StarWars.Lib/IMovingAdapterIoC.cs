using Hwdtech;
namespace StarWars.Lib;

public class RegisterIoCDependencyIMovingAdapter : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<IMoving>(
            "IoC.Register", 
            "Adapters.IMoving",
            (Func<object, object>)(obj => {
                if (obj is IMoving movingObject)
                {
                    return movingObject;
                }
                else
                {
                    // Если не реализует - вызываем адаптер
                    var adapted = IoC.Resolve<IMoving>("Adapters.Adaptee", obj, typeof(IMoving));
                    return adapted;
                }
    }));
    }
}
