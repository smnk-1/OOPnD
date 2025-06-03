using Hwdtech;

namespace StarWars.Lib
{
    public class ShootCommand : Hwdtech.ICommand
    {
        private readonly IWeapon _weaponParams;

        public ShootCommand(IWeapon obj)
        {
            _weaponParams = obj;
        }

        public void Execute()
        {
            var weaponObject = IoC.Resolve<IWeapon>("Weapon.Create");

            IoC.Resolve<Hwdtech.ICommand>("Weapon.Setup", weaponObject, _weaponParams).Execute();
            IoC.Resolve<Hwdtech.ICommand>("Actions.Start", weaponObject).Execute();
        }
    }
}
