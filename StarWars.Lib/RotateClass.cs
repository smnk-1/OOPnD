namespace StarWars.Lib;

public interface IRotating
{
    int Angle { get; set; }
    int RotateVelocity { get; }
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
        obj.Angle += obj.RotateVelocity;
        obj.Angle = obj.Angle % 360;
    }
}
