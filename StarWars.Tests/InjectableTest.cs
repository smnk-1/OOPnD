﻿using Moq;
using StarWars.Lib;

namespace StarWars.Tests
{
    public class CommandInjectableCommandTests
    {
        [Fact]
        public void Execute_InjectedCommand_ExecutesInjectedCommand()
        {

            var mockCommand = new Mock<Hwdtech.ICommand>();
            mockCommand.Setup(x => x.Execute()).Verifiable();
            var commandInjectableCommand = new CommandInjectableCommand();
            commandInjectableCommand.Inject(mockCommand.Object);

            commandInjectableCommand.Execute();

            mockCommand.Verify();
        }

        [Fact]
        public void Execute_NoInjectedCommand_ThrowsException()
        {
            var commandInjectableCommand = new CommandInjectableCommand();

            Assert.Throws<InvalidOperationException>(() => commandInjectableCommand.Execute());
        }
    }
}
