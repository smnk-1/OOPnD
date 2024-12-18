using Moq;
using StarWars.Lib;

namespace StarWars.Test
{
    public class MacroCommandTests
    {
        [Fact]
        public void StopOnException()
        {
            var command_1 = new Mock<ICommand>();
            var command_2 = new Mock<ICommand>();
            var command_3 = new Mock<ICommand>();

            command_2.Setup(m => m.Execute()).Throws<Exception>();

            var macroCommand = new MacroCommand(command_1.Object, command_2.Object, command_3.Object);

            Assert.Throws<Exception>(() => macroCommand.Execute());
            command_1.Verify(m => m.Execute(), Times.Once());
            command_2.Verify(m => m.Execute(), Times.Once());
            command_3.Verify(m => m.Execute(), Times.Never());
        }
    }
}
