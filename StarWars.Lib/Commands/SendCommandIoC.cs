using Hwdtech;

namespace StarWars.Lib;

public class RegisterIoCDependencySendCommand : Hwdtech.ICommand
{

    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Send",
            (Func<object[], object>)(obj =>
                new SendCommand(
                    (Hwdtech.ICommand)obj[0],
                    (ICommandReceiver)obj[1]
                )
            )).Execute();
    }
}
