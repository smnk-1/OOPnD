using Hwdtech;
using Hwdtech.Ioc;
using StarWars.Lib;

namespace StarWars.Tests;

public class GameItemRepositoryTests
{
    public GameItemRepositoryTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

    }

    [Fact]
    public void GameRepositoryTests()
    {
        new RegisterIoCDependencyAddCommand().Execute();
        new RegisterIoCDependencyRemoveCommand().Execute();
        new RegisterDependenciesGameItem().Execute();

        var Id = "spaceship01";
        var GameObject = new Dictionary<string, object>(){
            {"key", "value"}
        };
        var add_cmd = IoC.Resolve<Hwdtech.ICommand>("GameItem.Add" , [Id, GameObject]);

        Assert.NotNull(add_cmd);
        Assert.IsType<AddCommand>(add_cmd);     

        add_cmd.Execute();

        var recieved_item = IoC.Resolve<object>("GameItem", Id);

        Assert.Equal((Dictionary<string, object>)recieved_item, GameObject);

        var remove_cmd = IoC.Resolve<Hwdtech.ICommand>("GameItem.Remove", Id);

        Assert.NotNull(remove_cmd);
        Assert.IsType<RemoveCommand>(remove_cmd); 

        remove_cmd.Execute();

        Assert.Throws<KeyNotFoundException>(() => IoC.Resolve<object>("GameItem", Id));
    }
}
