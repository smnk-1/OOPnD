using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

namespace StarWars.Tests
{
    public class ShootCommandTests
    {
        public ShootCommandTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();

            IoC.Resolve<Hwdtech.ICommand>(
                    "Scopes.Current.Set",
                    IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
                )
                .Execute();
        }

        [Fact]
        public void Execute_ShouldCreateAndSetupWeaponCorrectly()
        {
            var spawnPosition = new CustomVector(new[] { 0, 0 });
            var direction = new CustomVector(new[] { 2, 1 });
            var projectileSpeed = 2;

            var weaponMock = new Mock<IWeapon>();
            weaponMock.Setup(w => w.SpawnPosition).Returns(spawnPosition);
            weaponMock.Setup(w => w.Direction).Returns(direction);
            weaponMock.Setup(w => w.ProjectileSpeed).Returns(projectileSpeed);

            var setupCommandMock = new Mock<Hwdtech.ICommand>();
            var startCommandMock = new Mock<Hwdtech.ICommand>();

            IoC.Resolve<Hwdtech.ICommand>(
                    "IoC.Register",
                    "Weapon.Create",
                    (object[] args) =>
                    {
                        return weaponMock.Object;
                    }
                )
                .Execute();

            IoC.Resolve<Hwdtech.ICommand>(
                    "IoC.Register",
                    "Weapon.Setup",
                    (object[] args) =>
                    {
                        return setupCommandMock.Object;
                    }
                )
                .Execute();

            IoC.Resolve<Hwdtech.ICommand>(
                    "IoC.Register",
                    "Actions.Start",
                    (object[] args) =>
                    {
                        return startCommandMock.Object;
                    }
                )
                .Execute();

            var shootCommand = new ShootCommand(weaponMock.Object);
            shootCommand.Execute();

            Assert.IsType<ShootCommand>(shootCommand);
            setupCommandMock.Verify(c => c.Execute(), Times.Once());
            startCommandMock.Verify(c => c.Execute(), Times.Once());
        }

        [Fact]
        public void Execute_ShouldThrowWhenDependencyNotRegistered()
        {
            var weaponParamsMock = new Mock<IWeapon>();
            var shootCommand = new ShootCommand(weaponParamsMock.Object);

            Assert.Throws<ArgumentException>(shootCommand.Execute);
        }
    }
}
