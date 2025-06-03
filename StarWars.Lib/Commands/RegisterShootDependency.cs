using Hwdtech;

namespace StarWars.Lib;

public class RegisterShootDependency : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Command.Shoot",
                (object[] args) =>
                {
                    return new ShootCommand((IWeapon)args[0]);
                }
            )
            .Execute();
    }
}
