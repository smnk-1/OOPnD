namespace StarWars.Lib;

public class SendCommand : ICommand
{
    private readonly ICommand cmd;
    private readonly ICommandReceiver receiver;

    public SendCommand(ICommand cmd, ICommandReceiver receiver)
    {
        this.cmd = cmd;
        this.receiver = receiver;
    }

    public void Execute()
    {
        receiver.Receive(cmd);
    }
}
