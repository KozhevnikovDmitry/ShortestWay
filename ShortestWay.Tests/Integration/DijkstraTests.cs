using NUnit.Framework;
using ShortestWay.Dijkstra;

namespace ShortestWay.Tests.Integration
{
    [TestFixture]
    public class DijkstraTests
    {
        [Test]
        public void SimpleValid_Test()
        {
            // Arrange
            var graph = new GraphProvider().Load<DijkstraGraph>(@"xml\simple-valid.xml");
            graph.Validate();
            graph.Markup();
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();

            // Act
            dijkstra.Compute(graph);
            var way = dijkstra.Find(graph);

            // Assert
            Assert.AreEqual(way[0].Id, 1);
            Assert.AreEqual(way[1].Id, 3);
            Assert.AreEqual(way[2].Id, 5);

            Assert.AreEqual(way.Count, 3);
            Assert.AreEqual((way[2] as DijkstraNode).Mark, 20);
        }

        [Test]
        public void AcceptanceSample_Test()
        {
            // Arrange
            var graph = new GraphProvider().Load<DijkstraGraph>(@"xml\shortestway-sample.xml");
            graph.Validate();
            graph.Markup();
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();

            // Act
            var way = dijkstra.Compute(graph).Find(graph);

            // Assert
            Assert.AreEqual(way[0].Id, 1);
            Assert.AreEqual(way[1].Id, 6);
            Assert.AreEqual(way[2].Id, 8);
            Assert.AreEqual(way[3].Id, 9);
            Assert.AreEqual(way[4].Id, 10);

            Assert.AreEqual(way.Count, 5);
            Assert.AreEqual((way[4] as DijkstraNode).Mark, 22);
        }
    }
}
