using Hwdtech;

namespace StarWars.Lib
{
    public class CreateMacroCommandStrategy
    {
        private readonly string cmdSpec;

        public CreateMacroCommandStrategy(string cmdSpec)
        {
            this.cmdSpec = cmdSpec;
        }

        public ICommand Resolve(object[] args)
        {
            var cmdsNames = IoC.Resolve<string[]>($"Specs.{cmdSpec}");
            var cmds = cmdsNames.Select(name => IoC.Resolve<ICommand>(name, args)).ToArray();
            return IoC.Resolve<ICommand>("Commands.Macro", cmds.Cast<object>().ToArray());
        }
    }
}
