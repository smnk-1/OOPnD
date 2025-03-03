namespace StarWars.Lib;

public class AddCommand : Hwdtech.ICommand
{
    private readonly Dictionary<string, IDictionary<string, object>> _dictionary;
    private readonly object _id;
    private readonly IDictionary<string, object> _item;

    public AddCommand(Dictionary<string, IDictionary<string, object>> dictionary, object id, IDictionary<string, object> item)
    {
        _dictionary = dictionary;
        _id = id;
        _item = item;
    }

    public void Execute()
    {
        _dictionary.Add((string)_id, _item);
    }
}
