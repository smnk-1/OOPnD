using Moq;
using StarWars.Lib;

namespace StarWars.Test;

// dotnet test в каталоге star-wars чтобы запустить тесты
public class MoveCommandTests
{
    [Fact]
    public void MoveCommandPos()
       {
        var moving = new Mock<IMoving>();

        moving.SetupGet(m => m.Position).Returns(new int[] { 12, 5 });
        moving.SetupGet(m => m.Velocity).Returns(new int[] { -7, 3 });

        var cmd = new MoveCommand(moving.Object);
        cmd.Execute();

        moving.VerifySet(m => m.Position = new int[] { 5, 8 });
    }

}