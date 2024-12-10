namespace StarWars.Lib;

public class Degree
{
    private int _value;

    public Degree(int value)
    {
        Value = value;
    }

    public int Value
    {
        get => _value;
        set => _value = Normalize(value);
    }

    private static int Normalize(int value)
    {
        return (value % 360 + 360) % 360;
    }

    public static Degree operator +(Degree d1, Degree d2)
    {
        return new Degree(d1.Value + d2.Value);
    }
}

public interface IRotate
{
    Degree Angle { get; set; }
    Degree RotateVelocity { get; }
}

public class RotateCommand : ICommand
{
    private readonly IRotate obj;

    public RotateCommand(IRotate obj)
    {
        this.obj = obj;
    }

    public void Execute()
    {
        obj.Angle = obj.Angle + obj.RotateVelocity;
    }
}

