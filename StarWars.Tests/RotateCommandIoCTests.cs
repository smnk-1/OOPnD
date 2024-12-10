using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

public class RotateCommandIoCTests
{
    public RotateCommandIoCTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
    }

    [Fact]
    public void Execute_ShouldRegisterRotateCommandDependency()
    {
        var mockRotate = new Mock<IRotate>();
        var mockGameObject = new Mock<IDictionary<string, object>>();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.IRotate",
            (Func<object, IRotate>)(obj =>
            {
                return mockRotate.Object;
            })).Execute();

        new RegisterIoCDependencyRotateCommand().Execute();

        var rotateCommand = IoC.Resolve<StarWars.Lib.ICommand>("Commands.Rotate", mockGameObject.Object);
        Assert.NotNull(rotateCommand);
        Assert.IsType<RotateCommand>(rotateCommand);
    }
}
