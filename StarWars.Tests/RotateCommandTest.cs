using Moq;
using StarWars.Lib;

namespace StarWars.Test;
public class RotateCommandTests
{
    [Fact]
    public void Test45plus90()
    {
        var rotating = new Mock<IRotating>();
        rotating.SetupGet(r => r.Angle).Returns(() => 45);
        rotating.SetupGet(r => r.RotateVelocity) .Returns(() => 90);

        var cmd = new RotateCommand(rotating.Object); 
        cmd.Execute();

        rotating.VerifySet(r => r.Angle = 135);
    }

    [Fact]
    public void TestCannotReadAngle()
    {
        var rotating = new Mock<IRotating>();
        rotating.SetupGet(m => m.Angle).Throws(new Exception("Cannot read angle"));
        rotating.SetupGet(m => m.RotateVelocity).Returns(() => 90);
        ICommand rotate = new RotateCommand(rotating.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }

    [Fact]
    public void TestCannotReadRotateVelocity() 
    {
        var rotating = new Mock<IRotating>();
        rotating.SetupGet(m => m.Angle).Returns(() => 45);
        rotating.SetupGet(m => m.RotateVelocity).Throws(new Exception("Cannot read rotate velocity"));
        ICommand rotate = new RotateCommand(rotating.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }

    [Fact]
    public void TestCannotSetAngle() 
    {
        var rotating = new Mock<IRotating>();
        rotating.SetupGet(m => m.Angle).Returns(() => 45);
        rotating.SetupGet(m => m.RotateVelocity).Returns(() => 90);
        rotating.SetupSet(m => m.Angle = It.IsAny<int>()).Throws(new Exception("Cannot set angle"));
        ICommand rotate = new RotateCommand(rotating.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }
}
