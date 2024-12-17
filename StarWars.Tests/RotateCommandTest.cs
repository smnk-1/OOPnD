using Moq;
using StarWars.Lib;

namespace StarWars.Test;
public class RotateCommandTests
{
    [Fact]
    public void Test45plus90()
    {
        var rotating = new Mock<IRotate>();
        rotating.SetupGet(r => r.Angle).Returns(() => new Angle(1));
        rotating.SetupGet(r => r.RotateVelocity).Returns(() => new Angle(1));

        var cmd = new RotateCommand(rotating.Object);
        cmd.Execute();

        rotating.VerifySet(r => r.Angle = It.Is<Angle>(d => d.Numerator == 2), Times.Once());
    }

    [Fact]
    public void TestCannotReadAngle()
    {
        var rotating = new Mock<IRotate>();
        rotating.SetupGet(m => m.Angle).Throws(new Exception("Cannot read angle"));
        rotating.SetupGet(m => m.RotateVelocity).Returns(() => new Angle(2));
        ICommand rotate = new RotateCommand(rotating.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }

    [Fact]
    public void TestCannotReadRotateVelocity()
    {
        var rotating = new Mock<IRotate>();
        rotating.SetupGet(m => m.Angle).Returns(() => new Angle(1));
        rotating.SetupGet(m => m.RotateVelocity).Throws(new Exception("Cannot read rotate velocity"));
        ICommand rotate = new RotateCommand(rotating.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }

    [Fact]
    public void TestCannotSetAngle()
    {
        var rotating = new Mock<IRotate>();
        rotating.SetupGet(m => m.Angle).Returns(() => new Angle(1));
        rotating.SetupGet(m => m.RotateVelocity).Returns(() => new Angle(2));
        rotating.SetupSet(m => m.Angle = It.IsAny<Angle>()).Throws(new Exception("Cannot set angle"));
        ICommand rotate = new RotateCommand(rotating.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }
}
public class DegreeCommandTests
{
    [Fact]
    public void Addition_ShouldNormalizeResult()
    {
        var degree1 = new Angle(7);
        var degree2 = new Angle(2);
        var result = degree1 + degree2;

        Assert.Equal(1, result.Numerator);
    }

    [Fact]
    public void NegativeAngle_ShouldNormalizeToPositive()
    {
        var degree = new Angle(-1);
        var result = degree.Numerator;

        Assert.Equal(7, result);
    }

    [Fact]
    public void FivePlusSevenEqualsFour()
    {
        var degree1 = new Angle(5);
        var degree2 = new Angle(7);
        var result = degree1 + degree2;

        Assert.Equal(4, result.Numerator);
    }

    [Fact]
    public void FifteenEqualsTwentyThree()
    {
        var degree1 = new Angle(15);
        var degree2 = new Angle(23);

        Assert.True(degree1.Equals(degree2));
    }

    [Fact]
    public void FifteenSymbolEqualsTwentyThree()
    {
        var angle1 = new Angle(15);
        var angle2 = new Angle(23);
        Assert.True(angle1.Numerator == angle2.Numerator);
    }

    [Fact]
    public void OneNotEqualsTwo()
    {
        var angle1 = new Angle(1);
        var angle2 = new Angle(2);
        Assert.False(angle1.Equals(angle2));
    }

    [Fact]
    public void OneNotSymbolEqualsTwo()
    {
        var angle1 = new Angle(1);
        var angle2 = new Angle(2);
        Assert.True(angle1.Numerator != angle2.Numerator);
    }

    [Fact]
    public void AngleHasHashCode()
    {
        var angle = new Angle(15);
        Assert.True(angle.GetHashCode() != null);
    }

    [Fact]
    public void AngleNotEqualsNull()
    {
        var angle1 = new Angle(1);
        Assert.False(angle1.Equals(null));
    }

    [Fact]
    public void AngleNotEqualsNotAngle()
    {
        var angle1 = new Angle(1);
        Assert.False(angle1.Equals(1));
    }

    [Fact]
    public void SinGivesCorrectValue()
    {
        var angle = new Angle(3);
        var expected = Math.Sin(135 * Math.PI / 180);
        var actual = angle.Sin();
        Assert.Equal(expected, actual, 1e-10);
    }

    [Fact]
    public void CosGivesCorrectValue()
    {
        var angle = new Angle(3);
        var expected = Math.Cos(135 * Math.PI / 180);
        var actual = angle.Cos();
        Assert.Equal(expected, actual, 1e-10);
    }
    [Fact]
    public void Sin_ShouldReturnCorrectValue_For0Degrees()
    {
        var angle = new Angle(0);
        var expected = Math.Sin(0 * Math.PI / 180);
        var actual = angle.Sin();
        Assert.Equal(expected, actual, 1e-10);
    }

    [Fact]
    public void Cos_ShouldReturnCorrectValue_For0Degrees()
    {
        var angle = new Angle(0);
        var expected = Math.Cos(0 * Math.PI / 180);
        var actual = angle.Cos();
        Assert.Equal(expected, actual, 1e-10);
    }

    [Fact]
    public void Sin_ShouldReturnCorrectValue_For180Degrees()
    {
        var angle = new Angle(4);
        var expected = Math.Sin(180 * Math.PI / 180);
        var actual = angle.Sin();
        Assert.Equal(expected, actual, 1e-10);
    }

    [Fact]
    public void Cos_ShouldReturnCorrectValue_For180Degrees()
    {
        var angle = new Angle(4);
        var expected = Math.Cos(180 * Math.PI / 180);
        var actual = angle.Cos();
        Assert.Equal(expected, actual, 1e-10);
    }
}

