using Moq;
using NUnit.Framework;
using StarWars.Lib;
using System;

namespace StarWars.Tests
{
    [TestFixture]
    public class CommandInjectableCommandTests
    {
        [Test]
        public void Execute_InjectedCommand_ExecutesInjectedCommand()
        {
            // Arrange
            var mockCommand = new Mock<ICommand>();
            var commandInjectableCommand = new CommandInjectableCommand();
            commandInjectableCommand.Inject(mockCommand.Object);

            // Act
            commandInjectableCommand.Execute();

            // Assert
            mockCommand.Verify(c => c.Execute(), Times.Once);
        }

        [Test]
        public void Execute_NoInjectedCommand_ThrowsException()
        {
            // Arrange
            var commandInjectableCommand = new CommandInjectableCommand();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => commandInjectableCommand.Execute());
        }
    }
}

