namespace StarWars.Lib;

public interface ICommand
{
    public void Execute();
}

public interface ICommandReceiver
{
    void Receive(Hwdtech.ICommand cmd);
}

public interface IRepository
{
    object GetItem(object id);
    void AddItem(object id, object GameItem);
    void RemoveItem(object id);
}
