using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace StarWars.Tests
{
    public class MacroCommandTests
    {
        public MacroCommandTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
        }

        [Fact]
        public void ResolveMacroCommand_Success()
        {
            var command_1 = new Mock<StarWars.Lib.ICommand>();
            var command_2 = new Mock<StarWars.Lib.ICommand>();

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Specs.Macro.Test",
                (Func<object[], object>)((args) => new[] { "Command1", "Command2" })
            ).Execute();

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Command1",
                (Func<object[], object>)((args) => command_1.Object)
            ).Execute();

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Command2",
                (Func<object[], object>)((args) => command_2.Object)
            ).Execute();

            var strategy = new CreateMacroCommandStrategy("Macro.Test");
            var macroCommand = strategy.Resolve(Array.Empty<object>());

            macroCommand.Execute();

            command_1.Verify(m => m.Execute(), Times.Once());
            command_2.Verify(m => m.Execute(), Times.Once());
        }

        [Fact]
        public void ResolveMacroCommand_ThrowsIfNoSpec()
        {
            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Specs.Macro.Invalid",
                (Func<object[], object>)((args) => null!)
            ).Execute();

            var strategy = new CreateMacroCommandStrategy("Macro.Invalid");

            Assert.Throws<InvalidOperationException>(() => strategy.Resolve(Array.Empty<object>()));
        }

        [Fact]
        public void ResolveMacroCommand_ThrowsIfNoCommands()
        {
            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Specs.Macro.Empty",
                (Func<object[], object>)((args) => Array.Empty<string>())
            ).Execute();

            var strategy = new CreateMacroCommandStrategy("Macro.Empty");

            Assert.Throws<InvalidOperationException>(() => strategy.Resolve(Array.Empty<object>()));
        }
    }
}
