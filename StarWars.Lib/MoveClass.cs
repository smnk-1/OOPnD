namespace StarWars.Lib;

public interface IMoving
{
    CustomVector Position { get; set; }
    CustomVector Velocity { get; }
}

public class CustomVector
{
    private readonly int[] elements;
    public int[] GetElements()
    {
        return elements;
    }
    public CustomVector(params int[] elements)
    {
        this.elements = elements;
    }

    public static CustomVector operator +(CustomVector v1, CustomVector v2)
    {
        if (v1.elements.Length != v2.elements.Length)
        {
            throw new InvalidOperationException("Vectors length is different");
        }

        var result = v1.elements.Zip(v2.elements, (a, b) => a + b).ToArray();
        return new CustomVector(result);
    }

    public static bool operator ==(CustomVector v1, CustomVector v2)
    {
        return ReferenceEquals(v1, v2);
    }

    public static bool operator !=(CustomVector v1, CustomVector v2)
    {
        return !(v1 == v2);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is not CustomVector vector)
        {
            return false;
        }

        return elements.SequenceEqual(vector.elements);
    }

    public override int GetHashCode()
    {
        return elements.Aggregate(0, (acc, x) => acc ^ x.GetHashCode());
    }
}

public class MoveCommand : ICommand
{
    private readonly IMoving obj;

    public MoveCommand(IMoving obj)
    {
        this.obj = obj;
    }

    public void Execute()
    {
        obj.Position += obj.Velocity;
    }
}
