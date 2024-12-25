namespace StarWars.Lib;

public class MacroCommand : ICommand
{
    private readonly IEnumerable<ICommand> cmds;

    public MacroCommand(IEnumerable<ICommand> commands)
    {
        cmds = commands;
    }

    public void Execute()
    {
        var commandList = cmds.ToList();
        commandList.ForEach(c => c.Execute());
    }
}
