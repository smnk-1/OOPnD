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

        new RegisterDependenciesGameItem().Execute();
    }

    [Fact]
    public void GameRepositoryTests()
    {
        var GameObject = new Dictionary<string, object>(){
            {"key", "value"}
        };
        var gameRepo = new GameRepository();

        gameRepo.AddItem("spaceship01", GameObject);

        var result = gameRepo.GetItem("spaceship01");

        Assert.Equal(GameObject, result);

        gameRepo.RemoveItem("spaceship01");

        Assert.Throws<KeyNotFoundException>(() => gameRepo.GetItem("spaceship01"));
    }
}
