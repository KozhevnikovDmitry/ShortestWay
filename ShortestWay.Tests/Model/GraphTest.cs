using System.Linq;
using Moq;
using NUnit.Framework;
using ShortestWay.Exceptions;
using ShortestWay.Model;

namespace ShortestWay.Tests.Model
{
    [TestFixture]
    public class GraphTests
    {
        [Test]
        public void Markup_SetNodeInitialMark_Test()
        {
            // Arrange
            var graph = new Graph
            {
                Nodes = new[]
                {
                    Mock.Of<Node>(t => t.IsStart),
                    Mock.Of<Node>(t => t.IsStart == false)
                }
            };

            // Act
            graph.Markup();

            // Assert
            Assert.AreEqual(graph.Nodes[0].Mark, 0);
            Assert.IsNull(graph.Nodes[1].Mark);
        }

        [Test]
        public void Markup_SetNodeParentGraph_Test()
        {
            // Arrange
            var graph = new Graph
            {
                Nodes = new[]
                {
                    Mock.Of<Node>(),
                    Mock.Of<Node>()
                }
            };

            // Act
            graph.Markup();

            // Assert
            Assert.AreEqual(graph.Nodes[0].Graph, graph);
            Assert.AreEqual(graph.Nodes[1].Graph, graph);
        }

        [Test]
        public void Validate_ThrowsIfNoNodes_Test()
        {
            // Arrange
            var graph1 = new Graph();
            var graph2 = new Graph { Nodes = new Node[0] };

            // Assert
            Assert.Throws<NoNodesException>(graph1.Validate);
            Assert.Throws<NoNodesException>(graph2.Validate);
        }

        [Test]
        public void Validate_ThrowsIfNoSingleStartNode_Test()
        {
            // Arrange
            var graph1 = new Graph
            {
                Nodes = new[]
                {
                    Mock.Of<Node>()
                }
            };
            var graph2 = new Graph
            {
                Nodes = new[]
                {
                    Mock.Of<Node>(t => t.IsStart),
                    Mock.Of<Node>(t => t.IsStart)
                }
            };

            // Assert
            Assert.Throws<NoSingleStartNodeException>(graph1.Validate);
            Assert.Throws<NoSingleStartNodeException>(graph2.Validate);
        }

        [Test]
        public void Validate_ThrowsIfStartNodeIsCrashed_Test()
        {
            // Arrange
            var graph = new Graph
            {
                Nodes = new[]
                {
                    Mock.Of<Node>(t => t.IsStart && t.IsCrash && t.Id == 1)
                }
            };

            // Assert
            var ex = Assert.Throws<StartNodeIsCrashedException>(graph.Validate);
            Assert.AreEqual(ex.Message, "Start node is crashed [1]");
        }

        [Test]
        public void Validate_ThrowsIfNoSingleFinishNode_Test()
        {
            // Arrange
            var graph1 = new Graph
            {
                Nodes = new[]
                {
                    Mock.Of<Node>(t => t.IsStart),
                    Mock.Of<Node>()
                }
            };
            var graph2 = new Graph
            {
                Nodes = new[]
                {
                    Mock.Of<Node>(t => t.IsStart),
                    Mock.Of<Node>(t => t.IsFinish),
                    Mock.Of<Node>(t => t.IsFinish)
                }
            };

            // Assert
            Assert.Throws<NoSingleFinishNodeException>(graph1.Validate);
            Assert.Throws<NoSingleFinishNodeException>(graph2.Validate);
        }

        [Test]
        public void Validate_ThrowsIfFinishtNodeIsCrashed_Test()
        {
            // Arrange
            var graph = new Graph
            {
                Nodes = new[]
                {
                    Mock.Of<Node>(t => t.IsStart),
                    Mock.Of<Node>(t => t.IsFinish && t.IsCrash && t.Id == 1)
                }
            };

            // Assert
            var ex = Assert.Throws<FinishNodeIsCrashedException>(graph.Validate);
            Assert.AreEqual(ex.Message, "Finish node is crashed [1]");
        }

        [Test]
        public void Validate_ThrowsIfNotUniqueIds_Test()
        {
            // Arrange
            var graph = new Graph
            {
                Nodes = new[]
                {
                    Mock.Of<Node>(t => t.IsStart && t.Id == 1),
                    Mock.Of<Node>(t => t.IsFinish && t.Id == 1)
                }
            };

            // Assert
            var ex = Assert.Throws<NodesWithNotUniqueIdsException>(graph.Validate);
            Assert.AreEqual(ex.Message, "Some nodes have the same id [1]");
        }

        [Test]
        public void Validate_ThrowsIfNodeLinkIsNotBidirectionalTest()
        {
            // Arrange
            var node1 = Mock.Of<Node>(t => t.IsStart && t.Id == 1);
            var node2 = Mock.Of<Node>(t => t.IsFinish && t.Id == 2 && t.IsLinked(node1) == false);
            Mock.Get(node1).Setup(t => t.IsLinked(node2)).Returns(true);
            var graph = new Graph
            {
                Nodes = new[]
                {
                    node1,
                    node2
                }
            };

            // Assert
            var ex = Assert.Throws<NodeLinkIsNotBidirectionalException>(graph.Validate);
            Assert.AreEqual(ex.Message, "Link between nodes [1] and [2] is not bidirectional");
        }

        [Test]
        public void Validate_ThrowsIfNodeLinkHasDifferentWeightTest()
        {
            // Arrange
            var node1 = Mock.Of<Node>(t => t.IsStart && t.Id == 1);
            var node2 = Mock.Of<Node>(t => t.IsFinish && t.Id == 2 && t.IsLinked(node1) && t.LinkWeight(node1) == 20);
            Mock.Get(node1).Setup(t => t.IsLinked(node2)).Returns(true);
            Mock.Get(node1).Setup(t => t.LinkWeight(node2)).Returns(10);
            var graph = new Graph
            {
                Nodes = new[]
                {
                    node1,
                    node2
                }
            };

            // Assert
            var ex = Assert.Throws<NodeLinkHasInconsistentWeightException>(graph.Validate);
            Assert.AreEqual(ex.Message, "Link between nodes [1] and [2] has inconsistent weight");
        }

        [Test]
        public void Validate_ThrowsIfNodeLinkHasNonPositiveWeightTest()
        {
            // Arrange
            var node1 = Mock.Of<Node>(t => t.IsStart && t.Id == 1);
            var node2 = Mock.Of<Node>(t => t.IsFinish && t.Id == 2 && t.IsLinked(node1) && t.LinkWeight(node1) == -10);
            Mock.Get(node1).Setup(t => t.IsLinked(node2)).Returns(true);
            Mock.Get(node1).Setup(t => t.LinkWeight(node2)).Returns(-10);
            var graph = new Graph
            {
                Nodes = new[]
                {
                    node1,
                    node2
                }
            };

            // Assert
            var ex = Assert.Throws<NodeLinkHasNonPositiveWeightException>(graph.Validate);
            Assert.AreEqual(ex.Message, "Link between nodes [1] and [2] has non-positive weight");
        }

        [Test]
        public void Validate_NotThrows_Test()
        {
            // Arrange
            var node1 = Mock.Of<Node>(t => t.IsStart && t.Id == 1);
            var node2 = Mock.Of<Node>(t => t.IsFinish && t.Id == 2 && t.IsLinked(node1) && t.LinkWeight(node1) == 10);
            Mock.Get(node1).Setup(t => t.IsLinked(node2)).Returns(true);
            Mock.Get(node1).Setup(t => t.LinkWeight(node2)).Returns(10);
            var graph = new Graph
            {
                Nodes = new[]
                {
                    node1,
                    node2
                }
            };

            // Assert
            Assert.DoesNotThrow(graph.Validate);
        }

        [Test]
        public void Linked_ThrowsIfNoNodes_Test()
        {
            // Arrange
            var graph1 = new Graph();
            var graph2 = new Graph { Nodes = new Node[0] };

            // Assert
            Assert.Throws<NoNodesException>(() => graph1.Linked(Mock.Of<Node>()));
            Assert.Throws<NoNodesException>(() => graph2.Linked(Mock.Of<Node>()));
        }

        [Test]
        public void Linked_Test()
        {
            // Arrange
            var target = Mock.Of<Node>();
            var linked = Mock.Of<Node>(t => t.IsLinked(target));
            var graph = new Graph
            {
                Nodes = new[]
                {
                    linked,
                    Mock.Of<Node>()
                }
            };

            // Act
            var linkedList = graph.Linked(target);

            // Assert
            Assert.AreEqual(linkedList.Single(), linked);
        }
    }
}
