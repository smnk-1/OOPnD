namespace StarWars.Lib;

public class MacroCommand : ICommand
{
    private readonly ICommand[] cmds;
    public MacroCommand(ICommand[] commands)
    {
        cmds = commands;
    }

    public void Execute()
    {
        var commandList = new List<ICommand>();
        commandList = cmds.ToList<ICommand>();
        commandList.ForEach(c => c.Execute());
    }
}
