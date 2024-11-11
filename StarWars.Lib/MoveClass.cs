namespace StarWars.Lib;

public interface IMoving
{
    CustomVector Position { get; set; }
    CustomVector Velocity { get; }
}

public class CustomVector
{
    private int[] elements;

    public CustomVector( params int[] elements)
    {
        this.elements = elements;
    }

    public static CustomVector operator +(CustomVector v1, CustomVector v2)
    {
        if (v1.elements.Length != v2.elements.Length)
            throw new InvalidOperationException("Vectors length is different");

        int[] result = v1.elements.Zip(v2.elements, (a, b) => a + b).ToArray();
        return new CustomVector(result);
    }

    public static bool operator ==(CustomVector v1, CustomVector v2)
    {
        if (ReferenceEquals(v1, null) && ReferenceEquals(v2, null)) return true;
        if (ReferenceEquals(v1, null) || ReferenceEquals(v2, null)) return false;

        if (v1.elements.Length != v2.elements.Length)
            return false;

        return v1.elements.SequenceEqual(v2.elements);
    }

    public static bool operator != (CustomVector v1, CustomVector v2)
    {
        return !(v1 == v2);
    }

    public override bool Equals(object? obj)
    {
    if (obj is null){return false;}
    if (obj is not CustomVector vector){return false;}
    return this == vector;
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
