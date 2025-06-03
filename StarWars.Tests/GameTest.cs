using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

namespace StarWars.Tests
{
    public class GameTests
    {
        private readonly object _scope;
        private readonly Mock<Hwdtech.ICommand> _cmd1;
        private readonly Mock<Hwdtech.ICommand> _cmd2;
        private readonly Mock<Hwdtech.ICommand> _exCmd;
        private readonly Mock<Hwdtech.ICommand> _exHandler;
        private readonly Queue<Hwdtech.ICommand> _q;

        public GameTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            _scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", _scope).Execute();

            _cmd1 = new Mock<Hwdtech.ICommand>();
            _cmd2 = new Mock<Hwdtech.ICommand>();
            _exCmd = new Mock<Hwdtech.ICommand>();
            _exCmd.Setup(x => x.Execute()).Throws<Exception>();
            _exHandler = new Mock<Hwdtech.ICommand>();

            _q = new Queue<Hwdtech.ICommand>();

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Game.Queue.Count",
                (Func<object[], object>)(_ => (Func<int>)(() => _q.Count))
            ).Execute();

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Game.Queue.NextCommand",
                (Func<object[], object>)(_ => _q.Dequeue())
            ).Execute();

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "ExceptionHandler",
                (Func<object[], object>)(args => _exHandler.Object)
            ).Execute();
        }

        [Fact]
        public void AllCommandsInGameQueueAreExecuted()
        {
            _q.Enqueue(_cmd1.Object);
            _q.Enqueue(_cmd2.Object);

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Command.Time",
                (Func<object[], object>)(_ => TimeSpan.FromMilliseconds(400))
            ).Execute();

            new Game(_scope).Execute();

            _cmd1.Verify(x => x.Execute(), Times.Once());
            _cmd2.Verify(x => x.Execute(), Times.Once());
        }

        [Fact]
        public void NoCommandsAreExecutedWhenTimeIsUp()
        {
            _q.Enqueue(_cmd1.Object);

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Command.Time",
                (Func<object[], object>)(_ => TimeSpan.FromMilliseconds(-1))
            ).Execute();

            new Game(_scope).Execute();

            _cmd1.Verify(x => x.Execute(), Times.Never());
        }

        [Fact]
        public void ExceptionHandlerIsExecutedWhenCommandThrows()
        {
            _q.Enqueue(_exCmd.Object);

            IoC.Resolve<Hwdtech.ICommand>(
                "IoC.Register",
                "Command.Time",
                (Func<object[], object>)(_ => TimeSpan.FromMilliseconds(400))
            ).Execute();

            new Game(_scope).Execute();

            _exHandler.Verify(x => x.Execute(), Times.Once());
        }
    }
}
