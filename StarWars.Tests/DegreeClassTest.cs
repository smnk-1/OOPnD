using Moq;
using StarWars.Lib;

namespace StarWars.Test;

public class DegreeClassTest{
    [Fact]
    public void AngleOver360()
    {
        var degree = new Mock<Degree>();
        degree.Setup(x => x.Value).Returns(359);
        
    }
}