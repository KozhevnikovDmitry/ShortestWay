using NUnit.Framework;
using ShortestWay.Data;

namespace ShortestWay.Tests
{
    [TestFixture]
    public class GraphTests
    {
        [Test]
        public void Create_Test()
        {
            // Act
            var graph = Graph.Create("shortestway-sample.xml");

            // Assert
            Assert.NotNull(graph);
        }
    }
}
