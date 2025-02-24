using Hwdtech;

namespace StarWars.Lib;

public class GameRepository : IRepository
{
    public IDictionary<string, object> GetItem(object id)
    {
        return IoC.Resolve<IDictionary<string, object>>("GameItem", id);
    }
    public void AddItem(object id, IDictionary<string, object> GameItem)
    {
        IoC.Resolve<object>("GameItem.Add", id, GameItem);
    }
    public void RemoveItem(object id)
    {
        IoC.Resolve<IDictionary<string, object>>("GameItem.Remove", id);
    }
}
