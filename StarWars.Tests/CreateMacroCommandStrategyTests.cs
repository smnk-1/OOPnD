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

            var command1Mock = new Mock<Hwdtech.ICommand>();
            var command2Mock = new Mock<Hwdtech.ICommand>();

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Command1",
                (Func<object[], object>)((args) => command1Mock.Object)
            ).Execute();

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Command2",
                (Func<object[], object>)((args) => command2Mock.Object)
            ).Execute();

            var strategy = new CreateMacroCommandStrategy("Test");

            var macroCommand = strategy.Resolve(Array.Empty<object>());
            macroCommand.Execute();

            command1Mock.Verify(m => m.Execute(), Times.Once());
            command2Mock.Verify(m => m.Execute(), Times.Once());
        }

        [Fact]
        public void ResolveMacroCommand_EmptyCommandList()
        {
            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Specs.Empty",
                (Func<object[], object>)((args) => Array.Empty<string>())
            ).Execute();

            var strategy = new CreateMacroCommandStrategy("Empty");

            var macroCommand = strategy.Resolve(Array.Empty<object>());

            var exception = Record.Exception(() => macroCommand.Execute());
            Assert.Null(exception);
        }

        [Fact]
        public void ResolveMacroCommand_NullCommandList()
        {
            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Specs.Null",
                (Func<object[], object>)((args) => null!)
            ).Execute();

            var strategy = new CreateMacroCommandStrategy("Null");

            var macroCommand = strategy.Resolve(Array.Empty<object>());

            var exception = Record.Exception(() => macroCommand.Execute());
            Assert.Null(exception);
        }
    }
}
