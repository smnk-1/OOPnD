namespace StarWars.Lib;

public interface ICommand
{
    public void Execute();
}

public interface ICommandReceiver
{
    void Receive(ICommand cmd);
}
