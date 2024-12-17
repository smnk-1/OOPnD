﻿namespace StarWars.Lib;

public interface ICommandInjectable
{
    void Inject(ICommand command);
    void Execute();
}

public class CommandInjectableCommand : ICommand, ICommandInjectable
{
    private ICommand _injectedCommand;

    public void Inject(ICommand command)
    {
        _injectedCommand = command;
    }

    public void Execute()
    {
        if (_injectedCommand == null)
        {
            throw new InvalidOperationException("Command not injected");
        }
        _injectedCommand.Execute();
    }
}
