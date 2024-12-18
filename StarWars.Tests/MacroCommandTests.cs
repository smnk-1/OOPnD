using Moq;
using StarWars.Lib;

namespace StarWars.Tests
{
    public class MacroCommandTests
    {
        [Fact]
        public void StopOnException()
        {
            var cmd1 = new Mock<ICommand>();
            var cmd2 = new Mock<ICommand>();
            var cmd3 = new Mock<ICommand>();

            cmd2.Setup(m => m.Execute()).Throws<Exception>();

            var macroCommand = new MacroCommand(cmd1.Object, cmd2.Object, cmd3.Object);

            Assert.Throws<Exception>(() => macroCommand.Execute());
            cmd1.Verify(m => m.Execute(), Times.Once());
            cmd2.Verify(m => m.Execute(), Times.Once());
            cmd3.Verify(m => m.Execute(), Times.Never());
        }
    }
}
