using Moq;
using StarWars.Lib;

namespace StarWars.Test;

public class SendCommandTests
{
    [Fact]
    public void SendCommand_Execute_Test()
    {
        var cmd = new Mock<ICommand>().Object;
        var mockReceiver = new Mock<ICommandReceiver>();
        var sendCommand = new SendCommand(cmd, mockReceiver.Object);

        sendCommand.Execute();
        mockReceiver.Verify(receiver => receiver.Receive(cmd), Times.Once(), "The Receive method was not called correctly.");
    }

    [Fact]
    public void SendCommand_Receiver_Fails_Test()
    {
        var cmd = new Mock<ICommand>().Object;
        var mockReceiver = new Mock<ICommandReceiver>();
        var sendCommand = new SendCommand(cmd, mockReceiver.Object);

        mockReceiver.Setup(receiver => receiver.Receive(cmd)).Throws(new Exception("Receiver error"));
        var exception = Assert.Throws<InvalidOperationException>(() => sendCommand.Execute());
        Assert.Equal("Failed to send command", exception.Message);
    }
}
