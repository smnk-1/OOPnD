using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

public class RotateCommandIoCTests
{
    public RotateCommandIoCTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var iocScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void Execute_ShouldRegisterRotateCommandDependency()
    {
        var mockRotate = new Mock<IRotating>();
        var mockGameObject = new Mock<IDictionary<string, object>>();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.IRotatingObject",
            (Func<object, IRotating>)(obj =>
            {
                return mockRotate.Object;
            })).Execute();

        new RegisterIoCDependencyRotateCommand().Execute();

        var rotateCommand = IoC.Resolve<StarWars.Lib.ICommand>("Commands.Rotate", mockGameObject.Object);
        Assert.NotNull(rotateCommand);
        Assert.IsType<RotateCommand>(rotateCommand);
    }
}
