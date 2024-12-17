using Moq;
using NUnit.Framework;
using StarWars.Lib;

namespace StarWars.Tests
{
    [TestFixture]
    public class CommandInjectableCommandTests
    {
        [Test]
        public void Execute_InjectedCommand_ExecutesInjectedCommand()
        {

            var mockCommand = new Mock<ICommand>();
            var commandInjectableCommand = new CommandInjectableCommand();
            commandInjectableCommand.Inject(mockCommand.Object);

            commandInjectableCommand.Execute();

            mockCommand.Verify(c => c.Execute(), Times.Once);
        }

        [Test]
        public void Execute_NoInjectedCommand_ThrowsException()
        {
            var commandInjectableCommand = new CommandInjectableCommand();

            Assert.Throws<InvalidOperationException>(() => commandInjectableCommand.Execute());
        }
    }
}
