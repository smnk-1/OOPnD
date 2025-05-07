using Hwdtech;
    using System;

    namespace StarWars.Lib
    {
        public class ShootCommand : IShootCommand
        {
            private readonly IGameObjectRepository gameObjectRepository;
            private readonly IFotonTorpedoFactory fotonTorpedoFactory;
            private readonly Func<ICommand, object[], ICommand> createMoveCommand;
            private readonly ICommandReceiver commandReceiver;

            public Guid ShipId { get; }

            public ShootCommand(
                Guid shipId,
                IGameObjectRepository gameObjectRepository,
                IFotonTorpedoFactory fotonTorpedoFactory,
                Func<ICommand, object[], ICommand> createMoveCommand,
                ICommandReceiver commandReceiver
            )
            {
                ShipId = shipId;
                this.gameObjectRepository = gameObjectRepository;
                this.fotonTorpedoFactory = fotonTorpedoFactory;
                this.createMoveCommand = createMoveCommand;
                this.commandReceiver = commandReceiver;
            }

            public void Execute()
            {
                var ship = gameObjectRepository.GetById(ShipId) as IShip;
                if (ship == null)
                {
                    throw new InvalidOperationException($"Ship with id '{ShipId}' not found or is not a ship.");
                }

                var torpedo = fotonTorpedoFactory.Create(ship);

                gameObjectRepository.Add(torpedo);


                var moveCommand = createMoveCommand(null, new object[] {torpedo.Id});

                commandReceiver.AddCommand(moveCommand);
            }
        }
    }