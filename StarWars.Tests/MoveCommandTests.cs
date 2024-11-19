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

public class CustomVectorTests
{
    [Fact]
    public void CustomVecorIsInitializableTest()
    {
        int[] elements = { 1, 2, 3 };
        var vector = new CustomVector(elements);
        Assert.True(elements.SequenceEqual(vector.GetElements()));
    }

    [Fact]
    public void CustomVectorEqualsReturnsTrueForIdenticalVectorsTest()
    {
        var v1 = new CustomVector(1, 2, 3);
        var v2 = new CustomVector(1, 2, 3);
        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void CustomVectorEqualsReturnsFalseForDifferentVectorsTest()
    {
        var v1 = new CustomVector(1, 2, 3);
        var v2 = new CustomVector(4, 5, 6);
        Assert.False(v1.Equals(v2));
    }

    [Fact]
    public void CustomVectorEqualsReturnsFalseForNullTest()
    {
        var v1 = new CustomVector(1, 2, 3);
        Assert.False(v1.Equals(null));
    }

    [Fact]
    public void CustomVectorEqualsReturnsFalseForDifferentTypeTest()
    {
        var v1 = new CustomVector(1, 2, 3);
        var obj = new object();
        Assert.False(v1.Equals(obj));
    }

    [Fact]
    public void CustomVectorOperatorPlusAddsVectorsCorrectlyTest()
    {
        var v1 = new CustomVector(1, 2, 3);
        var v2 = new CustomVector(4, 5, 6);
        var result = v1 + v2;
        var expected = new CustomVector(5, 7, 9);
        Assert.True(result.Equals(expected));
    }

    [Fact]
    public void CustomVectorOperatorPlusThrowsForDifferentLengthsTest()
    {
        var v1 = new CustomVector(1, 2);
        var v2 = new CustomVector(3, 4, 5);
        Assert.Throws<InvalidOperationException>(() => _ = v1 + v2);
    }

    [Fact]
    public void OperatorEquals_ShouldReturnFalseForDifferentVectors()
    {
        var v1 = new CustomVector(1, 2, 3);
        var v2 = new CustomVector(4, 5, 6);
        Assert.False(v1 == v2);
    }

    [Fact]
    public void CustomVectorOperatorEqualsReturnsTrueForSameReferenceTest()
    {
        var v1 = new CustomVector(1, 2, 3);
        var v2 = v1;
        Assert.True(v1 == v2);
    }

    [Fact]
    public void CustomVectorOperatorNotEqualsReturnsTrueForDifferentVectorsTest()
    {
        var v1 = new CustomVector(1, 2, 3);
        var v2 = new CustomVector(4, 5, 6);
        Assert.True(v1 != v2);
    }

    [Fact]
    public void CustomVectorGetHashCodeReturnsSameForIdenticalVectorsTest()
    {
        var v1 = new CustomVector(1, 2, 3);
        var v2 = new CustomVector(1, 2, 3);
        int hash1 = v1.GetHashCode();
        int hash2 = v2.GetHashCode();
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void CustomVectorGetHashCodeReturnsDifferentForDifferentVectorsTest()
    {
        var v1 = new CustomVector(1, 2, 3);
        var v2 = new CustomVector(4, 5, 6);
        int hash1 = v1.GetHashCode();
        int hash2 = v2.GetHashCode();
        Assert.NotEqual(hash1, hash2);
    }
}
