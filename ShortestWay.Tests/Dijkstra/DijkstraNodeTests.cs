using NUnit.Framework;
using ShortestWay.Dijkstra;

namespace ShortestWay.Tests.Dijkstra
{
    [TestFixture]
    public class DijkstraNodeTests
    {
        [Test]
        public void Ctor_DefaultValues_Test()
        {
            // Act
            var node = new DijkstraNode();

            // Assert
            Assert.IsNull(node.Mark);
            Assert.IsNull(node.Previous);
            Assert.False(node.IsVisited);
        }
    }
}
