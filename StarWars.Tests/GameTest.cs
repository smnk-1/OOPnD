using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

namespace StarWars.Tests
{
    public class GameTests
    {
        private readonly object _scope;

        private readonly Mock<Hwdtech.ICommand> command1;
        private readonly Mock<Hwdtech.ICommand> command2;

        private readonly Mock<Hwdtech.ICommand> errorCommand;
        private readonly Mock<Hwdtech.ICommand> errorHandler;

        private readonly Queue<Hwdtech.ICommand> queue;

        public GameTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            _scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", _scope).Execute();

            command1 = new Mock<Hwdtech.ICommand>();
            command2 = new Mock<Hwdtech.ICommand>();

            errorCommand = new Mock<Hwdtech.ICommand>();
            errorCommand.Setup(x => x.Execute()).Throws<Exception>();

            errorHandler = new Mock<Hwdtech.ICommand>();

            queue = new Queue<Hwdtech.ICommand>();

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Game.Queue.Count",
                (Func<object[], object>)(_ => (Func<int>)(() => queue.Count))
            ).Execute();

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Game.Queue.NextCommand",
                (Func<object[], object>)(_ => queue.Dequeue())
            ).Execute();

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "ExceptionHandler",
                (Func<object[], object>)(args => errorHandler.Object)
            ).Execute();
        }

        [Fact]
        public void AllCommandsInQueueExecuted()
        {
            queue.Enqueue(command1.Object);
            queue.Enqueue(command2.Object);

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Command.Time",
                (Func<object[], object>)(_ => TimeSpan.FromMilliseconds(400))
            ).Execute();

            new Game(_scope).Execute();

            command1.Verify(x => x.Execute(), Times.Once());
            command2.Verify(x => x.Execute(), Times.Once());
        }

        [Fact]
        public void NoCommandsExecutedOnOvertime()
        {
            queue.Enqueue(command1.Object);

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Command.Time",
                (Func<object[], object>)(_ => TimeSpan.FromMilliseconds(-1))
            ).Execute();

            new Game(_scope).Execute();

            command1.Verify(x => x.Execute(), Times.Never());
        }

        [Fact]
        public void ExceptionHandlerExecutionOnCommandThrows()
        {
            queue.Enqueue(errorCommand.Object);

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Command.Time",
                (Func<object[], object>)(_ => TimeSpan.FromMilliseconds(400))
            ).Execute();

            new Game(_scope).Execute();

            errorHandler.Verify(x => x.Execute(), Times.Once());
        }
    }
}
