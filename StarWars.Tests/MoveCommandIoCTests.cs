using Moq;
using Hwdtech;
using StarWars.Lib;

public class MoveCommandIoCTests
{
    public MoveCommandIoCTests()
    {
        var reg = new RegisterIoCDependencyMoveCommand();
        reg.Execute();
    }

   [Fact]
    public void MoveCommand_ShouldUpdatePositionBasedOnVelocity()
    {
        var mockObject = new Mock<IMoving>();
        var initialPosition = new CustomVector(0, 0);
        var velocity = new CustomVector(1, 1);

        mockObject.SetupProperty(m => m.Position, initialPosition);
        mockObject.Setup(m => m.Velocity).Returns(velocity);

        var command = (StarWars.Lib.ICommand)IoC.Resolve<StarWars.Lib.ICommand>("Commands.Move", mockObject.Object);

        command.Execute();

        Assert.Equal(new CustomVector(1, 1), mockObject.Object.Position);
    }
}
