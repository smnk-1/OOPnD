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
    IDictionary<string, object> GetItem(object id);
    void AddItem(object id, IDictionary<string, object> GameItem);
    void RemoveItem(object id);
}
