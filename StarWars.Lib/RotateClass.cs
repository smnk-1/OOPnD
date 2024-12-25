namespace StarWars.Lib;

public class Angle
{
    private int _numerator;
    private const int Denominator = 8;

    public Angle(int numerator)
    {
        Numerator = numerator;
    }

   public int Numerator
    {
        get => _numerator;
        set => _numerator = (value % Denominator + Denominator) % Denominator;
    }

    public static implicit operator double(Angle angle)
    {
        return (double)angle._numerator / Denominator * 360;
    }

    public static Angle operator +(Angle a1, Angle a2)
    {
        return new Angle(a1.Numerator + a2.Numerator);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is Angle otherAngle)
        {
            return Numerator == otherAngle.Numerator;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return _numerator.GetHashCode();
    }

    public double Sin()
    {
        return Math.Sin((double)Numerator / Denominator * 2 * Math.PI);
    }

    public double Cos()
    {
        return Math.Cos((double)Numerator / Denominator * 2 * Math.PI);
    }
}

public interface IRotating
{
    Angle Angle { get; set; }
    Angle RotateVelocity { get; }
}

public class RotateCommand : ICommand
{
    private readonly IRotating obj;

    public RotateCommand(IRotating obj)
    {
        this.obj = obj;
    }

    public void Execute()
    {
        obj.Angle = obj.Angle + obj.RotateVelocity;
    }
}
