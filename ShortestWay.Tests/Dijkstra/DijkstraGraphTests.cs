using Moq;
using NUnit.Framework;
using ShortestWay.Dijkstra;
using ShortestWay.Exceptions;

namespace ShortestWay.Tests.Dijkstra
{
    [TestFixture]
    public class DijkstraGraphTests
    {
        [Test]
        public void Markup_SetNodeInitialMark_Test()
        {
            // Arrange
            var graph = new DijkstraGraph
            {
                Nodes = new[]
                {
                    Mock.Of<DijkstraNode>(t => t.IsStart),
                    Mock.Of<DijkstraNode>(t => t.IsStart == false)
                }
            };

            // Act
            graph.Markup();

            // Assert
            Assert.AreEqual(graph.Nodes[0].TotalWeigth, 0);
            Assert.IsNull(graph.Nodes[1].TotalWeigth);
        }

        [Test]
        public void HasUnvisitedTest_ReturnFalseIf_AllIsVisitedIsTrue_Test()
        {
            // Arrange
            var graph = new DijkstraGraph()
            {
                Nodes = new[]
                {
                    Mock.Of<DijkstraNode>(t => t.IsVisited == true)
                }
            };

            // Assert
            Assert.False(graph.HasUnvisited());
        }

        [Test]
        public void HasUnvisitedTest_ReturnFalseIf_AllIsVisitedTrue_Or_MarkIsInfinity_Test()
        {
            // Arrange
            var graph = new DijkstraGraph()
            {
                Nodes = new[]
                {
                    Mock.Of<DijkstraNode>(t => t.IsVisited),
                    Mock.Of<DijkstraNode>(t => t.IsVisited == false && t.TotalWeigth == null)
                }
            };

            // Assert
            Assert.False(graph.HasUnvisited());
        }

        [Test]
        public void HasUnvisited_ReturnsTrue_Test()
        {
            // Arrange
            var graph = new DijkstraGraph()
            {
                Nodes = new[]
                {
                    Mock.Of<DijkstraNode>(t => t.IsVisited),
                    Mock.Of<DijkstraNode>(t => t.IsVisited == false && t.TotalWeigth == 1)
                }
            };

            // Assert
            Assert.True(graph.HasUnvisited());
        }

        [Test]
        public void NearestVisited_ThrowsIfHasNotUnvisited_Test()
        {
            // Arrange
            var graph = new DijkstraGraph()
            {
                Nodes = new[]
                {
                    Mock.Of<DijkstraNode>(t => t.IsVisited),
                    Mock.Of<DijkstraNode>(t => t.IsVisited == false && t.TotalWeigth == null)
                }
            };

            // Assert
            Assert.Throws<GraphHasNotUnvisitedNodesException>(() => graph.NearestUnvisited());
        }

        [Test]
        public void NearestUnvisited_ReturnsFirstUnvisitedWithMinimalMark_Test()
        {
            // Arrange
            var node1 = Mock.Of<DijkstraNode>(t => t.IsVisited == false && t.TotalWeigth == 3);
            var node2 = Mock.Of<DijkstraNode>(t => t.IsVisited == false && t.TotalWeigth == 3);
            var node3 = Mock.Of<DijkstraNode>(t => t.IsVisited == false && t.TotalWeigth == 10);
            var node4 = Mock.Of<DijkstraNode>(t => t.IsVisited == true && t.TotalWeigth == 1);
            var graph = new DijkstraGraph()
            {
                Nodes = new[]
                {
                    node1, node2, node3, node4
                }
            };

            // Act
            var nearest = graph.NearestUnvisited();

            // Assert
            Assert.AreEqual(nearest, node1);
        }

    }
}