using System.Linq;
using NUnit.Framework;
using ShortestWay.Exceptions;
using ShortestWay.Model;

namespace ShortestWay.Tests
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
            Graph graph = provider.Load(@"data\simple-valid.xml");

            // Assert
            Assert.AreEqual(graph.Nodes.Count(), 4);
        }

        [Test]
        public void Load_ThrowsIfFileNotExists_Test()
        {
            // Arrange
            var provider = new GraphProvider();
            
            // Assert
            var ex = Assert.Throws<SourceIsNotExistsException>(() => provider.Load(@"data\not_exists.xml"));
            Assert.AreEqual(ex.Message, "Source is not exists on path: [data\\not_exists.xml]");
        }

        [Test]
        public void Load_ThrowsIfXmlIsWrong_Test()
        {
            // Arrange
            var provider = new GraphProvider();

            // Assert
            Assert.Throws<SourceIsNotValidException>(() => provider.Load(@"data\wrong.xml"));
        }

    }
}
