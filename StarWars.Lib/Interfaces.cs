namespace StarWars.Lib;

public interface ICommand
{
    void Execute();
}

public interface ICommandReceiver
{
    void Receive(Hwdtech.ICommand cmd);
}

public interface IAdapter
{
    object Adapt(object adaptee);
}
