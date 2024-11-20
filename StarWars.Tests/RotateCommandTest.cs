using Moq;
using StarWars.Lib;

namespace StarWars.Test;
public class RotateCommandTests
{
    [Fact]
    public void Test45plus90()
    {
        var rotating = new Mock<IRotating>();
        rotating.SetupGet(r => r.Angle).Returns(() => new Degree(45));
        rotating.SetupGet(r => r.RotateVelocity).Returns(() => new Degree(90));

        var cmd = new RotateCommand(rotating.Object);
        cmd.Execute();

        rotating.VerifySet(r => r.Angle = It.Is<Degree>(d => d.Value == 135), Times.Once());
    }

    [Fact]
    public void TestCannotReadAngle()
    {
        var rotating = new Mock<IRotating>();
        rotating.SetupGet(m => m.Angle).Throws(new Exception("Cannot read angle"));
        rotating.SetupGet(m => m.RotateVelocity).Returns(() => new Degree(90));
        ICommand rotate = new RotateCommand(rotating.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }

    [Fact]
    public void TestCannotReadRotateVelocity()
    {
        var rotating = new Mock<IRotating>();
        rotating.SetupGet(m => m.Angle).Returns(() => new Degree(45));
        rotating.SetupGet(m => m.RotateVelocity).Throws(new Exception("Cannot read rotate velocity"));
        ICommand rotate = new RotateCommand(rotating.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }

    [Fact]
    public void TestCannotSetAngle()
    {
        var rotating = new Mock<IRotating>();
        rotating.SetupGet(m => m.Angle).Returns(() => new Degree(45));
        rotating.SetupGet(m => m.RotateVelocity).Returns(() => new Degree(90));
        rotating.SetupSet(m => m.Angle = It.IsAny<Degree>()).Throws(new Exception("Cannot set angle"));
        ICommand rotate = new RotateCommand(rotating.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }

    [Fact]  
    public void Addition_ShouldNormalizeResult()
    {
        var degree1 = new Degree(350);
        var degree2 = new Degree(20);
        var result = degree1 + degree2;

        Assert.Equal(10, result.Value);
    }

    [Fact]
    public void NegativeAngle_ShouldNormalizeToPositive()
    {
        var degree = new Degree(-45);
        var result = degree.Value;

        Assert.Equal(315, result);
    }
}

