using Moq;
using Hwdtech;
using Hwdtech.Ioc;
using StarWars.Lib;

public class MoveCommandIoCTests
{
    public MoveCommandIoCTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
    }

    [Fact]
    public void Execute_ShouldRegisterMoveCommandDependency()
    {
        var mockMoving = new Mock<IMoving>();
        var mockGameObject = new Mock<IDictionary<string, object>>();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.IMoving",
            (Func<object, IMoving>)(obj =>
            {
                return mockMoving.Object;
            })).Execute();

        new RegisterIoCDependencyMoveCommand().Execute();

        var moveCommand = IoC.Resolve<StarWars.Lib.ICommand>("Commands.Move", mockGameObject.Object); 
        Assert.NotNull(moveCommand);
        Assert.IsType<MoveCommand>(moveCommand);
    }
}
