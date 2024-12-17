using StarWars.Lib;
using Moq;
using System;

namespace StarWars.Tests
{
    public class CommandInjectableCommandTests
    {
        public void Execute_InjectedCommand_ExecutesInjectedCommand()
        {
            var mockCommand = new Mock<ICommand>();
            var commandInjectableCommand = new CommandInjectableCommand();
            commandInjectableCommand.Inject(mockCommand.Object);

            commandInjectableCommand.Execute();

            if (!mockCommand.Verify(c => c.Execute(), Moq.Times.Once))
            {
                throw new AssertionException("Injected command was not executed.");
            }
        }

        public void Execute_NoInjectedCommand_ThrowsException()
        {
            var commandInjectableCommand = new CommandInjectableCommand();

            try
            {
                commandInjectableCommand.Execute();
                throw new AssertionException("Expected exception was not thrown.");
            }
            catch (InvalidOperationException)
            {

            }
        }

        private class AssertionException : Exception
        {
            public AssertionException(string message) : base(message) { }
        }
    }
}

