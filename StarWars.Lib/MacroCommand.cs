namespace StarWars.Lib;

public class MacroCommand : Hwdtech.ICommand
{
    private readonly IEnumerable<Hwdtech.ICommand> cmds;

    public MacroCommand(IEnumerable<Hwdtech.ICommand> commands)
    {
        cmds = commands;
    }

    public void Execute()
    {
        var commandList = cmds.ToList();
        commandList.ForEach(c => c.Execute());
    }
}