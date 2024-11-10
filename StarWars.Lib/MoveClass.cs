namespace StarWars.Lib
{
    public interface IMoving
    {
        int[] Position { get; set; }
        int[] Velocity { get; }
    }

    public class MoveCommand : ICommand
    {
        private readonly IMoving obj;

        public MoveCommand(IMoving obj)
        {
            this.obj = obj;
        }

        public void Execute()
        {
            obj.Position = obj.Position.Select((value, index) => value + obj.Velocity[index]).ToArray();
        }
    }

    public class CommandProcessor
    {
        private readonly Queue<ICommand> commandQueue = new Queue<ICommand>();
        private readonly int interval;
        private CancellationTokenSource? cts;

        public CommandProcessor(int interval = 500)
        {
            this.interval = interval;
        }

        public void EnqueueCommand(ICommand command)
        {
            commandQueue.Enqueue(command);
        }

        public async Task ProcessCommandsAsync()
        {
            cts = new CancellationTokenSource();

            while (commandQueue.Count > 0)
            {
                if (cts.Token.IsCancellationRequested)
                    break;

                var command = commandQueue.Dequeue();
                command.Execute();

                await Task.Delay(interval, cts.Token);
            }
        }

        public void Cancel()
        {
            cts?.Cancel();
            cts = null;
        }
    }
}

