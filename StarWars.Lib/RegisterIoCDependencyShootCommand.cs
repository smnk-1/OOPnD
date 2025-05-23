    using Hwdtech;
    using Hwdtech.Ioc;
    using System;

    namespace StarWars.Lib
    {
        public class RegisterIoCDependencyShootCommand : ICommand
        {
            public void Execute()
            {
                IoC.Resolve<ICommand>(
                    "IoC.Register",
                    "Commands.Shoot",
                    (object[] args) =>
                    {

                        Guid shipId = (Guid)args[0];


                        var gameObjectRepository = IoC.Resolve<IGameObjectRepository>("GameObjectRepository");
                        var fotonTorpedoFactory = IoC.Resolve<IFotonTorpedoFactory>("FotonTorpedoFactory");
                        var createMoveCommand = IoC.Resolve<Func<ICommand, object[], ICommand>>("CreateMoveCommand");
                        var commandReceiver = IoC.Resolve<ICommandReceiver>("CommandReceiver");

                        return new ShootCommand(shipId, gameObjectRepository, fotonTorpedoFactory, createMoveCommand, commandReceiver);
                    }
                ).Execute();
            }
        }
    }