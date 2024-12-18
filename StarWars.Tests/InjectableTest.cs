using Moq;
using StarWars.Lib;

namespace StarWars.Tests
{
    public class CommandInjectableCommandTests
    {
        [Fact]
        public void Execute_InjectedCommand_ExecutesInjectedCommand()
        {
            // Arrange
            var mockCommand = new Mock<ICommand>();
            mockCommand.Setup(x => x.Execute()).Verifiable(); // Устанавливаем проверку
            var commandInjectableCommand = new CommandInjectableCommand();
            commandInjectableCommand.Inject(mockCommand.Object);

            // Act
            commandInjectableCommand.Execute();

            // Assert
            mockCommand.Verify(); // Проверяем, что Execute() был вызван
        }

        [Fact]
        public void Execute_NoInjectedCommand_ThrowsException()
        {
            // Arrange
            var commandInjectableCommand = new CommandInjectableCommand();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => commandInjectableCommand.Execute());
        }
    }
}

