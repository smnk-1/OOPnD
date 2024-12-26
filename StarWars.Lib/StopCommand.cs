using Hwdtech;

namespace StarWars.Lib
{
    public class StopCommand : Hwdtech.ICommand
    {
        private readonly string _objId;
        private readonly string _cmdName;
        public StopCommand(string objId, string cmdName)
        {
            _cmdName = cmdName;
            _objId = objId;

        }

        public void Execute()
        {
            IoC.Resolve<ICommandInjectable>("Game.Object.GetInjectable", _objId, _cmdName)
                .Inject(IoC.Resolve<ICommand>("Commands.Empty"));

        }
    }
}
