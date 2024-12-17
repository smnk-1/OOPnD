namespace StarWars.Lib;

public interface ICommandInjectable : ICommand
{
    void InjectDependencies(IServiceProvider serviceProvider);
}

public class CommandInjectableCommand : ICommandInjectable
{
    private IMoving _obj;

    public void InjectDependencies(IServiceProvider serviceProvider)
    {
        _obj = serviceProvider.GetRequiredService<IMoving>();
    }

    public void Execute()
    {
        if (_obj == null) throw new InvalidOperationException("Dependencies not injected");
        _obj.Position += _obj.Velocity;
    }
}

