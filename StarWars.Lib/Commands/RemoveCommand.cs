namespace StarWars.Lib;

public class RemoveCommand : Hwdtech.ICommand
{
    private readonly Dictionary<string, IDictionary<string, object>> _dictionary;
    private readonly object _id;

    public RemoveCommand(Dictionary<string, IDictionary<string, object>> dictionary, object id)
    {
        _dictionary = dictionary;
        _id = id;
    }

    public void Execute()
    {
        _dictionary.Remove((string)_id);
    }
}
