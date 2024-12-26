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
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        }

        [Fact]
        public void ResolveMacroCommand_Success()
        {
            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Specs.Test",
                (Func<object[], object>)((args) => new[] { "Command1", "Command2" })
            ).Execute();

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Command1",
                (Func<object[], object>)((args) => new Mock<Hwdtech.ICommand>().Object)
            ).Execute();

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Command2",
                (Func<object[], object>)((args) => new Mock<Hwdtech.ICommand>().Object)
            ).Execute();

            var strategy = new CreateMacroCommandStrategy("Test");

            var macroCommand = strategy.Resolve(Array.Empty<object>());
            macroCommand.Execute();

            var command1Mock = IoC.Resolve<Hwdtech.ICommand>("Command1") as Mock<Hwdtech.ICommand>;
            var command2Mock = IoC.Resolve<Hwdtech.ICommand>("Command2") as Mock<Hwdtech.ICommand>;

            command1Mock?.Verify(m => m.Execute(), Times.Once());
            command2Mock?.Verify(m => m.Execute(), Times.Once());
        }

        [Fact]
        public void ResolveMacroCommand_ThrowsIfNoSpec()
        {
            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Specs.Invalid",
                (Func<object[], object>)((args) => null!)
            ).Execute();

            var strategy = new CreateMacroCommandStrategy("Invalid");

            Assert.Throws<InvalidOperationException>(() => strategy.Resolve(Array.Empty<object>()));
        }

        [Fact]
        public void ResolveMacroCommand_ThrowsIfNoCommands()
        {
            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Specs.Empty",
                (Func<object[], object>)((args) => Array.Empty<string>())
            ).Execute();

            var strategy = new CreateMacroCommandStrategy("Empty");

            Assert.Throws<InvalidOperationException>(() => strategy.Resolve(Array.Empty<object>()));
        }
    }
}
