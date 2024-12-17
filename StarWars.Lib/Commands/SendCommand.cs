namespace StarWars.Lib;

class SendCommand : ICommand
{
    ICommand cmd;
    ICommandReceiver receiver;

    public SendCommand(ICommand cmd, ICommandReceiver receiver)
    {
        this.cmd = cmd;
        this.receiver = receiver;
    }
    
    public void Execute()
    {
        try
        {
        receiver.Receive(cmd);
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException("Failed to send command", exception);
        }
    }
}
