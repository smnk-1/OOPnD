using Moq;
using StarWars.Lib;

namespace StarWars.Test;

// dotnet test в каталоге star-wars чтобы запустить тесты
public class MoveCommandTests
{
    [Fact]
    public void MoveCommandPositionUpdateTest()
       {
        var moving = new Mock<IMoving>();

        moving.SetupGet(m => m.Position).Returns(new int[] { 12, 5 });
        moving.SetupGet(m => m.Velocity).Returns(new int[] { -7, 3 });

        var cmd = new MoveCommand(moving.Object);
        cmd.Execute();

        moving.VerifySet(m => m.Position = new int[] { 5, 8 });
    }

    [Fact]
    public void MoveCommandPositionIsNotReadableTest()
    {
        var movingObject = new Mock<IMoving>();

        movingObject.SetupGet(m => m.Position).Throws(new InvalidOperationException());
        movingObject.SetupGet(m => m.Velocity).Returns(new int[] { -7, 3 });
        
        var cmd = new MoveCommand(movingObject.Object);
        
        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }

    [Fact]
    public void MoveCommandVelocityIsNotReadableTest()
    {
        var movingObject = new Mock<IMoving>();

        movingObject.SetupGet(m => m.Position).Returns(new int[] { 12, 5 });
        movingObject.SetupGet(m => m.Velocity).Throws(new InvalidOperationException());
        
        var cmd = new MoveCommand(movingObject.Object);
        
        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }

    [Fact]
    public void MoveCommandPositionIsNotWritableTest()
    {
        var movingObject = new Mock<IMoving>();

        movingObject.SetupGet(m => m.Position).Returns(new int[] { 12, 5 });;
        movingObject.SetupGet(m => m.Velocity).Returns(new int[] { -7, 3 });
        movingObject.SetupSet(m => m.Position = It.IsAny<int[]>()).Throws(new InvalidOperationException());

        var cmd = new MoveCommand(movingObject.Object);

        Assert.Throws<InvalidOperationException>(() => cmd.Execute());
    }
}