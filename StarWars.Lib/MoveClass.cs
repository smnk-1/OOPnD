namespace StarWars.Lib;

public interface IMoving
{
    int[] Position { get; set; }
    int[] Velocity { get; }
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
        obj.Position = obj.Position.Select((value, index) => value + obj.Velocity[index]).ToArray();
    }
}
