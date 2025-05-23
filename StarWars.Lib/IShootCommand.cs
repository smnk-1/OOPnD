    using Hwdtech;

    namespace StarWars.Lib
    {
        public interface IShootCommand : ICommand
        {
            Guid ShipId { get; }
        }
    }