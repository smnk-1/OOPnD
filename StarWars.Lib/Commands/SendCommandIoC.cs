using Hwdtech;
namespace StarWars.Lib;
public class RegisterIoCDependencySendCommand : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Send",
            (Func<object, object>)(obj =>
            new SendCommand(IoC.Resolve<ICommand>("Adapters.ICommand", obj), IoC.Resolve<ICommandReceiver>("Adapters.ICommandReceiver", obj)))).Execute();
    }
}
