using System.Linq;
using NUnit.Framework;
using ShortestWay.Dijkstra;
using ShortestWay.Exceptions;
using ShortestWay.Model;

namespace ShortestWay.Tests.Integration
{
    [TestFixture]
    public class GraphProviderTests
    {
        [Test]
        public void Load_Test()
        {
            // Arrange
            var provider = new GraphProvider();

            // Act
            var graph = provider.Load<Graph<Node>>(@"xml\simple-valid.xml");

            // Assert
            Assert.AreEqual(graph.Nodes.Count(), 5);
        }
        
        [Test]
        public void Load_Dijkstra_Test()
        {
            // Arrange
            var provider = new GraphProvider();

            // Act
            var graph = provider.Load<DijkstraGraph>(@"xml\simple-valid.xml");

            // Assert
            Assert.AreEqual(graph.Nodes.Count(), 5);
        }

        [Test]
        public void Load_ThrowsIfFileNotExists_Test()
        {
            // Arrange
            var provider = new GraphProvider();
            
            // Assert
            var ex = Assert.Throws<SourceIsNotExistsException>(() => provider.Load<Graph<Node>>(@"xml\not_exists.xml"));
            Assert.AreEqual(ex.Message, "Source is not exists on path: [xml\\not_exists.xml]");
        }

        [Test]
        public void Load_ThrowsIfXmlIsWrong_Test()
        {
            // Arrange
            var provider = new GraphProvider();

            // Assert
            Assert.Throws<SourceIsNotValidException>(() => provider.Load<Graph<Node>>(@"xml\wrong.xml"));
        }

    }
}
