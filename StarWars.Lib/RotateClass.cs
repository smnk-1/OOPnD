namespace StarWars.Lib;

public class Degree 
{

}

public interface IRotating
{
    int Angle   { get; set; }
    int RotateVelocity { get; }
}

public class RotateCommand : ICommand
{
    private IRotating obj;

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
