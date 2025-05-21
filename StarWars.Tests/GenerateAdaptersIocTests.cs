using System.Reflection;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

namespace StarWars.Tests;

public class GenerateAdaptersTests
{
    public GenerateAdaptersTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

    }

    [Fact]
    public void Generate_ReturnsSameObject_WhenAlreadyImplementsInterface()
    {
        new RegisterIoCDependencyAdaptersGenerate().Execute();
        var targetType = typeof(IMoving);
        var implementingObject = new Mock<IMoving>().Object;
        var agrs = new object[] { implementingObject, targetType };

        var result = IoC.Resolve<object>("Adapters.Generate", agrs);

        Assert.Same(implementingObject, result);
    }

    [Fact]
    public void Generate_ReturnsObjectFromDictionary_WhenAlreadyCreated()
    {
        var mockIMoving = new Mock<IMoving>().Object;
        var mockAdapter = new Mock<IAdapter>();

        mockAdapter.Setup(a => a.Adapt(It.IsAny<object>())).Returns(mockIMoving);

        var testObject = new object();
        var adapterKey = $"{testObject.GetType().FullName}->{typeof(IMoving).FullName}";

        var adaptersDict = new Dictionary<string, IAdapter> { [adapterKey] = mockAdapter.Object };
        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            "Adapters.Dictionary",
            (Func<object, object>)(_ => adaptersDict)
        ).Execute();

        new RegisterIoCDependencyAdaptersGenerate().Execute();
        var result = IoC.Resolve<object>("Adapters.Generate", testObject, typeof(IMoving));

        Assert.Same(mockIMoving, result);
        mockAdapter.Verify(a => a.Adapt(testObject), Times.Once);
    }

    [Fact]
    public void Generate_CreatesNewAdapter()
    {
        var expectedResult = new Mock<IMoving>().Object;
        var adapterType = typeof(MockAdapter);
        var testObj = new object();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.Dictionary",
            (Func<object, object>)(_ => new Dictionary<string, IAdapter>())).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.GetTemplate",
        (Func<object[], object>)(args =>
        {
            var targetType = (Type)args[0];
            return "moq-template";
        })).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.Compile",
        (Func<object[], object>)(_ => new MockAdapterAssembly(typeof(MockAdapter)))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.GenerateAdapterCode",
            (Func<object[], object>)(_ => "moq-code")).Execute();

        new RegisterIoCDependencyAdaptersGenerate().Execute();
        var result = IoC.Resolve<object>("Adapters.Generate", testObj, typeof(IMoving));

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IMoving>(result);
    }

    public class MockAdapterAssembly : Assembly
    {
        private readonly Type _adapterType;

        public MockAdapterAssembly(Type adapterType) => _adapterType = adapterType;

        public override Type[] GetTypes() => new[] { _adapterType };
    }

    public class MockAdapter : IAdapter
    {
        private readonly IMoving _mockMoving;

        public MockAdapter()
        {
            var mockMoving = new Mock<IMoving>();
            mockMoving.Setup(m => m.Position).Returns(new CustomVector());
            mockMoving.Setup(m => m.Velocity).Returns(new CustomVector());
            _mockMoving = mockMoving.Object;
        }

        public object Adapt(object adaptee) => _mockMoving;
    }
}
