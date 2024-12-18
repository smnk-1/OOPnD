using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyMacroCommandTests
    {
        public RegisterIoCDependencyMacroCommandTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
        }

        [Fact]
        public void Execute_ShouldRegisterMacroCommandDependency()
        {
            var command_1 = new Mock<Hwdtech.ICommand>();
            var command_2 = new Mock<Hwdtech.ICommand>();

            new RegisterIoCDependencyMacroCommand().Execute();

            Hwdtech.ICommand[] commandArray = { command_1.Object, command_2.Object };

            var macroCommand = IoC.Resolve<StarWars.Lib.ICommand>("Commands.Macro", commandArray);
            macroCommand.Execute();

            command_1.Verify(m => m.Execute(), Times.Once());
            command_2.Verify(m => m.Execute(), Times.Once());
        }
    }
}
