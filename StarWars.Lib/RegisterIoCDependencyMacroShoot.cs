    using Hwdtech;
    using Hwdtech.Ioc;
    using System;

    namespace StarWars.Lib
    {
        public class RegisterIoCDependencyMacroShoot : ICommand
        {
            public void Execute()
            {
                IoC.Resolve<ICommand>(
                    "IoC.Register",
                    "Macro.Shoot",
                    (object[] args) =>
                    {
                        Guid shipId = (Guid)args[0];

                        return IoC.Resolve<ICommand>("Commands.Shoot", new object[] { shipId });
                    }
                ).Execute();
            }
        }
    }