using Moq;
using NUnit.Framework;
using ShortestWay.Dijkstra;
using ShortestWay.Exceptions;

namespace ShortestWay.Tests.Dijkstra
{
    [TestFixture]
    public class DijkstraTests
    {
        [Test]
        public void Compute_ReturnsItSelf_WhenGraphHasNotUnvisited_Test()
        {
            // Arrange
            var graph = Mock.Of<DijkstraGraph>(t => t.HasUnvisited() == false);
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();

            // Assert
            Assert.AreEqual(dijkstra, dijkstra.Compute(graph));
        }

        [Test]
        public void Compute_MarkCurrentNodeAsVisited_Test()
        {
            // Arrange
            var current = Mock.Of<DijkstraNode>();
            var graph = Mock.Of<DijkstraGraph>(t => t.HasUnvisited() == true
                                                 && t.Linked(current) == new DijkstraNode[0]);
            Mock.Get(graph).Setup(t => t.NearestUnvisited()).Returns(current).Callback(() =>
                Mock.Get(graph).Setup(t => t.HasUnvisited()).Returns(false));
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();

            // Act
            dijkstra.Compute(graph);

            // Assert
            Assert.True(current.IsVisited);
        }

        [Test]
        public void Compute_UpdateLinkedMark_IfThisWayShorter_Test()
        {
            // Arrange
            var linked = Mock.Of<DijkstraNode>(t => t.TotalWeigth == 30);
            var current = Mock.Of<DijkstraNode>(t => t.TotalWeigth == 10 && t.LinkWeight(linked) == 10);
            var graph = Mock.Of<DijkstraGraph>(t => t.HasUnvisited() == true
                                                 && t.Linked(current) == new[] { linked });
            Mock.Get(graph).Setup(t => t.NearestUnvisited()).Returns(current).Callback(() =>
                Mock.Get(graph).Setup(t => t.HasUnvisited()).Returns(false));
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();

            // Act
            dijkstra.Compute(graph);

            // Assert
            Assert.AreEqual(linked.TotalWeigth, 20);
        }
        
        [Test]
        public void Compute_UpdateLinkedMark_IfLinkedMarkIsinfinity_Test()
        {
            // Arrange
            var linked = Mock.Of<DijkstraNode>(t => t.TotalWeigth == null);
            var current = Mock.Of<DijkstraNode>(t => t.TotalWeigth == 10 && t.LinkWeight(linked) == 10);
            var graph = Mock.Of<DijkstraGraph>(t => t.HasUnvisited() == true
                                                 && t.Linked(current) == new[] { linked });
            Mock.Get(graph).Setup(t => t.NearestUnvisited()).Returns(current).Callback(() =>
                Mock.Get(graph).Setup(t => t.HasUnvisited()).Returns(false));
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();

            // Act
            dijkstra.Compute(graph);

            // Assert
            Assert.AreEqual(linked.TotalWeigth, 20);
        }

        [Test]
        public void Compute_NotUpdateLinkedMark_IfThisWayLonger_Test()
        {
            // Arrange
            var linked = Mock.Of<DijkstraNode>(t => t.TotalWeigth == 5);
            var current = Mock.Of<DijkstraNode>(t => t.TotalWeigth == 10 && t.LinkWeight(linked) == 10);
            var graph = Mock.Of<DijkstraGraph>(t => t.HasUnvisited() == true
                                                 && t.Linked(current) == new[] { linked });
            Mock.Get(graph).Setup(t => t.NearestUnvisited()).Returns(current).Callback(() =>
                Mock.Get(graph).Setup(t => t.HasUnvisited()).Returns(false));
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();

            // Act
            dijkstra.Compute(graph);

            // Assert
            Assert.AreEqual(linked.TotalWeigth, 5);
        }
        
        [Test]
        public void Compute_ChangeLinkedPreviousByCurrent_IfThisWayShorter_Test()
        {
            // Arrange
            var linked = Mock.Of<DijkstraNode>(t => t.TotalWeigth == 30);
            var current = Mock.Of<DijkstraNode>(t => t.TotalWeigth == 10 && t.LinkWeight(linked) == 10);
            var graph = Mock.Of<DijkstraGraph>(t => t.HasUnvisited() == true
                                                 && t.Linked(current) == new[] { linked });
            Mock.Get(graph).Setup(t => t.NearestUnvisited()).Returns(current).Callback(() =>
                Mock.Get(graph).Setup(t => t.HasUnvisited()).Returns(false));
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();

            // Act
            dijkstra.Compute(graph);

            // Assert
            Assert.AreEqual(linked.Previous, current);
        }

        [Test]
        public void Compute_ChangeLinkedPreviousByCurrent_IfLinkedMarkIsinfinity_Test()
        {
            // Arrange
            var linked = Mock.Of<DijkstraNode>(t => t.TotalWeigth == null);
            var current = Mock.Of<DijkstraNode>(t => t.TotalWeigth == 10 && t.LinkWeight(linked) == 10);
            var graph = Mock.Of<DijkstraGraph>(t => t.HasUnvisited() == true
                                                 && t.Linked(current) == new[] { linked });
            Mock.Get(graph).Setup(t => t.NearestUnvisited()).Returns(current).Callback(() =>
                Mock.Get(graph).Setup(t => t.HasUnvisited()).Returns(false));
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();

            // Act
            dijkstra.Compute(graph);

            // Assert
            Assert.AreEqual(linked.Previous, current);
        }

        [Test]
        public void Compute_NotChangeLinkedPreviousByCurrent_IfThisWayLonger_Test()
        {
            // Arrange
            var previous = Mock.Of<DijkstraNode>();
            var linked = Mock.Of<DijkstraNode>(t => t.TotalWeigth == 5 && t.Previous == previous);
            var current = Mock.Of<DijkstraNode>(t => t.TotalWeigth == 10 && t.LinkWeight(linked) == 10);
            var graph = Mock.Of<DijkstraGraph>(t => t.HasUnvisited() == true
                                                 && t.Linked(current) == new[] { linked });
            Mock.Get(graph).Setup(t => t.NearestUnvisited()).Returns(current).Callback(() =>
                Mock.Get(graph).Setup(t => t.HasUnvisited()).Returns(false));
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();

            // Act
            dijkstra.Compute(graph);

            // Assert
            Assert.AreEqual(linked.Previous, previous);
        }

        [Test]
        public void Find_ThrowsIfFinishNodeHasNoPrevious_Test()
        {
            // Arrange
            var finish = Mock.Of<DijkstraNode>(d => d.TotalWeigth == 1);
            var graph = Mock.Of<DijkstraGraph>(t => t.FinishNode() == finish);
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();
            
            // Assert
            Assert.Throws<NoRouteFromStartToFinishException>(() => dijkstra.GetShortestWay(graph));
        }

        [Test]
        public void Find_ThrowsIfFinishNodeMarkIsinfinity_Test()
        {
            // Arrange
            var finish = Mock.Of<DijkstraNode>(d => d.TotalWeigth == null && d.Previous == Mock.Of<DijkstraNode>());
            var graph = Mock.Of<DijkstraGraph>(t => t.FinishNode() == finish);
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();

            // Assert
            Assert.Throws<NoRouteFromStartToFinishException>(() => dijkstra.GetShortestWay(graph));
        }

        [Test]
        public void Find_ReturnsWayNodeListByPrevious_Test()
        {
            // Arrange
            var prevPrev = Mock.Of<DijkstraNode>();
            var prev = Mock.Of<DijkstraNode>(t => t.Previous == prevPrev);
            var finishNode = Mock.Of<DijkstraNode>(t => t.Previous == prev && t.TotalWeigth == 100500);
            var graph = Mock.Of<DijkstraGraph>(t => t.FinishNode() == finishNode);
            var dijkstra = new ShortestWay.Dijkstra.Dijkstra();

            // Act
            var way = dijkstra.GetShortestWay(graph);

            // Assert
            Assert.AreEqual(way[0], prevPrev);
            Assert.AreEqual(way[1], prev);
            Assert.AreEqual(way[2], finishNode);
        }
    }
}
