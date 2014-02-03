using NUnit.Framework;
using ShortestWay.Dijkstra;
using ShortestWay.Exceptions;

namespace ShortestWay.Tests.Integration
{
    [TestFixture]
    public class DijkstraTests
    {
        [TestCase("xml/no-nodes.xml", ExpectedException = typeof(NoNodesException))]
        [TestCase("xml/no-single-start.xml", ExpectedException = typeof(NoSingleStartNodeException))]
        [TestCase("xml/no-single-finish.xml", ExpectedException = typeof(NoSingleFinishNodeException))]
        [TestCase("xml/finish-crash.xml", ExpectedException = typeof(FinishNodeIsCrashedException))]
        [TestCase("xml/not-unique-ids.xml", ExpectedException = typeof(NodesWithNotUniqueIdsException))]
        [TestCase("xml/link-is-not-bidirectional.xml", ExpectedException = typeof(NodeLinkIsNotBidirectionalException))]
        [TestCase("xml/link-is-inconsistent-weight.xml", ExpectedException = typeof(NodeLinkHasInconsistentWeightException))]
        [TestCase("xml/link-weight-is-nonpositive.xml", ExpectedException = typeof(NodeLinkHasNonPositiveWeightException))]
        public void Validate_Failure_Test(string xml)
        {
            // Arrange
            var graph = new GraphProvider().Load<DijkstraGraph>(xml);
            graph.Validate();
            graph.Markup();
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();

            // Assert
            dijkstra.Compute(graph).GetShortestWay(graph);
        }

        [Test]
        public void SimpleSample_Test()
        {
            // Arrange
            var graph = new GraphProvider().Load<DijkstraGraph>(@"xml\simple-valid.xml");
            graph.Validate();
            graph.Markup();
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();

            // Act
            var way = dijkstra.Compute(graph)
                              .GetShortestWay(graph);

            // Assert
            Assert.AreEqual(way[0].Id, 1);
            Assert.AreEqual(way[1].Id, 3);
            Assert.AreEqual(way[2].Id, 5);

            Assert.AreEqual(way.Count, 3);
            Assert.AreEqual((way[2] as DijkstraNode).TotalWeigth, 20);
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
            var way = dijkstra.Compute(graph)
                              .GetShortestWay(graph);

            // Assert
            Assert.AreEqual(way[0].Id, 1);
            Assert.AreEqual(way[1].Id, 6);
            Assert.AreEqual(way[2].Id, 8);
            Assert.AreEqual(way[3].Id, 9);
            Assert.AreEqual(way[4].Id, 10);

            Assert.AreEqual(way.Count, 5);
            Assert.AreEqual((way[4] as DijkstraNode).TotalWeigth, 22);
        }
    }
}
