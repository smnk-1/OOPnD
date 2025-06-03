using Hwdtech;

namespace StarWars.Lib;

public class RegisterShootDependency : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
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