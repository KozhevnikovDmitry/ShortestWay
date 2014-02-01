using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShortestWay.Exceptions;
using ShortestWay.Model;

namespace ShortestWay.Tests.Model
{
    [TestFixture]
    public class NodeTests
    {
        [Test]
        public void IsStart_True_Test()
        {
            // Arrange
            var node = new Node
            {
                Role = "start"
            };

            // Assert
            Assert.True(node.IsStart);
        }

        [Test]
        public void IsStart_False_Test()
        {
            // Arrange
            var node = new Node
            {
                Role = "somerole"
            };

            // Assert
            Assert.False(node.IsStart);
        }

        [Test]
        public void IsFinish_True_Test()
        {
            // Arrange
            var node = new Node
            {
                Role = "finish"
            };

            // Assert
            Assert.True(node.IsFinish);
        }

        [Test]
        public void IsFinish_False_Test()
        {
            // Arrange
            var node = new Node
            {
                Role = "somerole"
            };

            // Assert
            Assert.False(node.IsFinish);
        }

        [Test]
        public void IsCrash_True_Test()
        {
            // Arrange
            var node = new Node
            {
                Status = "crash"
            };

            // Assert
            Assert.True(node.IsCrash);
        }

        [Test]
        public void IsCrash_False_Test()
        {
            // Arrange
            var node = new Node
            {
                Status = "somestatus"
            };

            // Assert
            Assert.False(node.IsCrash);
        }

        [Test]
        public void IsLinked_ReturnsFalseIfCrash_Test()
        {
            // Arrange
            var node = new Node
            {
                Status = "crash"
            };

            // Assert
            Assert.False(node.IsLinked(Mock.Of<Node>()));
        }

        [Test]
        public void IsLinked_ReturnsFalseIfItSelfTest()
        {
            // Arrange
            var node = new Node();

            // Assert
            Assert.False(node.IsLinked(node));
        }

        [Test]
        public void IsLinked_ThrowsIfLinksIsNull_Test()
        {
            // Arrange
            var node = new Node { Id = 1 };

            // Assert
            var ex = Assert.Throws<LinksAreNotSetupException>(() => node.IsLinked(Mock.Of<Node>()));
            Assert.AreEqual(ex.Message, "Links are not setup for node [1]");
        }

        [Test]
        public void IsLinked_ReturnsFalseIfThereIsNoLink_Test()
        {
            // Arrange
            var target = Mock.Of<Node>(t => t.Id == 1);
            var node = new Node
            {
                Links = new[]
                {
                    Mock.Of<Link>(t => t.Ref == 2)
                }
            };

            // Assert
            Assert.False(node.IsLinked(target));
        }

        [Test]
        public void IsLinked_Test()
        {
            // Arrange
            var target = Mock.Of<Node>(t => t.Id == 1);
            var node = new Node
            {
                Links = new[]
                {
                    Mock.Of<Link>(t => t.Ref == 1)
                }
            };

            // Assert
            Assert.True(node.IsLinked(target));
        }

        [Test]
        public void Linked_ThrowsIfGraphIsNotSetup_Test()
        {
            // Arrange
            var node = new Node { Id = 1 };

            // Assert
            var ex = Assert.Throws<ParentGraphIsNotSetupException>(() => node.Linked());
            Assert.AreEqual(ex.Message, "Parent graph is not setup for node [1]");
        }

        [Test]
        public void Linked_Test()
        {
            // Arrange
            var linked = new List<Node>();
            var graph = Mock.Of<Graph>();
            var node = new Node { Graph = graph };
            Mock.Get(graph).Setup(t => t.Linked(node)).Returns(linked);

            // Act
            var result = node.Linked();

            // Assert
            Assert.AreEqual(result, linked);
        }

        [Test]
        public void LinkWeight_ThrowsIfNotLinked_Test()
        {
            // Arrange
            var target = Mock.Of<Node>(t => t.Id == 2);
            var node = new Mock<Node> { CallBase = true };
            node.Setup(t => t.IsLinked(target)).Returns(false);
            node.Setup(t => t.Id).Returns(1);
            
            // Assert
            var ex = Assert.Throws<NodesAreNotLinkedToGetWeghtException>(() => node.Object.LinkWeight(target));
            Assert.AreEqual(ex.Message, "Nodes [1] and [2] are not linked, cannot get weight");
        }

        [Test]
        public void LinkWeight_ReturnsMinimalWeight_Test()
        {
            // Arrange
            var target = Mock.Of<Node>(t => t.Id == 1);
            var node = new Mock<Node> { CallBase = true };
            var links = new[]
            {
                Mock.Of<Link>(t => t.Ref == 2),
                Mock.Of<Link>(t => t.Ref == 1 && t.Weight == 20),
                Mock.Of<Link>(t => t.Ref == 1 && t.Weight == 10)
            };
            node.Setup(t => t.IsLinked(target)).Returns(true);
            node.Setup(t => t.Links).Returns(links);

            // Act
            var result = node.Object.LinkWeight(target);

            // Assert
            Assert.AreEqual(result, 10);
        }
    }
}
