﻿namespace StarWars.Lib;

public class Angle
{
    private int _numerator;
    private const int Denominator = 8;

    public Angle(int numerator)
    {
        _numerator = numerator;
        Normalize();
    }

    public int Numerator
    {
        get => _numerator;
        set
        {
            _numerator = value;
            Normalize();
        }
    }

    public double Value => (double)_numerator / Denominator * 360;

    private void Normalize()
    {
        if (_numerator >= Denominator)
        {
            _numerator = _numerator % Denominator;
        }
        else if (_numerator < 0)
        {
            _numerator = (Denominator + _numerator % Denominator) % Denominator;
        }
    }

    public static Angle operator +(Angle a1, Angle a2)
    {
        return new Angle(a1.Numerator + a2.Numerator);
    }

    public override bool Equals(object obj)
    {
        // Проверяем, является ли объект null
        if (obj == null)
        {
            return false;
        }

        // Проверяем, является ли объект типа Angle
        if (obj is Angle otherAngle)
        {
            // Сравниваем числители
            return Numerator == otherAngle.Numerator;
        }

        return false; // Если объект не Angle, возвращаем false
    }


    public override int GetHashCode()
    {
        return _numerator.GetHashCode();
    }

    public double Sin()
    {
        return Math.Sin(Value * Math.PI / 180);
    }

    public double Cos()
    {
        return Math.Cos(Value * Math.PI / 180);
    }

    public override string ToString()
    {
        return $"({_numerator}, {Denominator})";
    }
}

public interface IRotate
{
    Angle Angle { get; set; }
    Angle RotateVelocity { get; }
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