using Moq;
using StarWars.Lib;

namespace StarWars.Test;

public class MoveCommandTests
{
    [Fact]
    public void MoveCommandPositionUpdateTest()
       {
        var moving = new Mock<IMoving>();

        moving.SetupGet(m => m.Position).Returns(new CustomVector(12, 5));
        moving.SetupGet(m => m.Velocity).Returns(new CustomVector( -7, 3 ));

        var cmd = new MoveCommand(moving.Object);
        cmd.Execute();

        moving.VerifySet(m => m.Position = new CustomVector(5, 8));
    }

    [Fact]
    public void MoveCommandPositionIsNotReadableTest()
    {
        var movingObject = new Mock<IMoving>();

        movingObject.SetupGet(m => m.Position).Throws(new InvalidOperationException());
        movingObject.SetupGet(m => m.Velocity).Returns(new CustomVector(-7, 3 ));
        
        var cmd = new MoveCommand(movingObject.Object);
        
        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }

    [Fact]
    public void MoveCommandVelocityIsNotReadableTest()
    {
        var movingObject = new Mock<IMoving>();

        movingObject.SetupGet(m => m.Position).Returns(new CustomVector(12, 5));
        movingObject.SetupGet(m => m.Velocity).Throws(new InvalidOperationException());
        
        var cmd = new MoveCommand(movingObject.Object);
        
        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }

    [Fact]
    public void MoveCommandPositionIsNotWritableTest()
    {
        var movingObject = new Mock<IMoving>();

        movingObject.SetupGet(m => m.Position).Returns(new CustomVector(12, 5));;
        movingObject.SetupGet(m => m.Velocity).Returns(new CustomVector(-7, 3 ));
        movingObject.SetupSet(m => m.Position = It.IsAny<CustomVector>()).Throws(new InvalidOperationException());

        var cmd = new MoveCommand(movingObject.Object);

        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }
}