namespace StarWars.Lib;

public interface ICommand
{
    void Execute();
}

public interface ICommandReceiver
{
    void Receive(Hwdtech.ICommand cmd);
}

public interface IWeapon
{
    CustomVector SpawnPosition { get; }
    CustomVector Direction { get; }
    double ProjectileSpeed { get; }
}
