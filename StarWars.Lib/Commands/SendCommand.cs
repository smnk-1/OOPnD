namespace StarWars.Lib;

public class SendCommand : Hwdtech.ICommand
{
    private readonly Hwdtech.ICommand cmd;
    private readonly ICommandReceiver receiver;

    public SendCommand(Hwdtech.ICommand cmd, ICommandReceiver receiver)
    {
        this.cmd = cmd;
        this.receiver = receiver;
    }

    public void Execute()
    {
        receiver.Receive(cmd);
    }
}
