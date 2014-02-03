using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ShortestWay.Dijkstra;
using ShortestWay.Exceptions;

namespace ShortestWay.Model
{
    /// <summary>
    /// Road graph interface for constrainting <see cref="GraphProvider"/>
    /// </summary>
    public interface IGraph
    { }

    /// <summary>
    /// Road graph
    /// </summary>
    /// <typeparam name="T">Node type</typeparam>
    /// <remarks>
    /// Base road graph implements common rules of road net.
    /// Derived graphs may provide additional functionality for different way-search algorythms.
    /// For example <see cref="DijkstraGraph"/>
    /// </remarks>
    [XmlRoot("graph")]
    public class Graph<T> : IGraph where T : Node
    {
        [XmlElement("node")]
        public virtual T[] Nodes { get; set; }

        /// <summary>
        /// Validates road graph for consistence and compliance to common rules. See exception list.
        /// </summary>
        /// <exception cref="NodeLinkHasNonPositiveWeightException"></exception>
        /// <exception cref="NodeLinkHasInconsistentWeightException"></exception>
        /// <exception cref="NodeLinkIsNotBidirectionalException"></exception>
        /// <exception cref="NodesWithNotUniqueIdsException"></exception>
        /// <exception cref="FinishNodeIsCrashedException"></exception>
        /// <exception cref="StartNodeIsCrashedException"></exception>
        /// <exception cref="NoSingleStartNodeException"></exception>
        /// <exception cref="NoNodesException"></exception>
        public void Validate()
        {
            if (Nodes == null || !Nodes.Any())
            {
                throw new NoNodesException();
            }

            var startNode = Nodes.Where(t => t.IsStart).ToList();
            if (startNode.Count() != 1 || startNode.SingleOrDefault() == null)
            {
                throw new NoSingleStartNodeException();
            }

            if (startNode.Single().IsCrash)
            {
                throw new StartNodeIsCrashedException(startNode.Single().Id);
            }

            var finishNode = FinishNode();
            if (finishNode.IsCrash)
            {
                throw new FinishNodeIsCrashedException(finishNode.Id);
            }

            foreach (var validated in Nodes)
            {
                foreach (var node in Nodes.Where(t => !t.Equals(validated)))
                {
                    if (validated.Id == node.Id)
                        throw new NodesWithNotUniqueIdsException(validated.Id);

                    if (validated.IsLinked(node) != node.IsLinked(validated))
                    {
                        throw new NodeLinkIsNotBidirectionalException(validated.Id, node.Id);
                    }

                    if (validated.IsLinked(node) && node.IsLinked(validated))
                    {
                        if (validated.LinkWeight(node) != node.LinkWeight(validated))
                        {
                            throw new NodeLinkHasInconsistentWeightException(validated.Id, node.Id);
                        }

                        if (validated.LinkWeight(node) <= 0)
                        {
                            throw new NodeLinkHasNonPositiveWeightException(validated.Id, node.Id);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns list of nodes, thats are linked with provided <paramref name="node"/>
        /// </summary>
        /// <exception cref="NoNodesException"></exception>
        public virtual IList<T> Linked(Node node)
        {
            if (Nodes == null || !Nodes.Any())
            {
                throw new NoNodesException();
            }

            return Nodes.Where(t => t.IsLinked(node) && !t.IsCrash).ToList();
        }

        /// <summary>
        /// Returns finish node of graph
        /// </summary>
        /// <exception cref="NoSingleFinishNodeException"></exception>
        public virtual T FinishNode()
        {
            var finishNode = Nodes.Where(t => t.IsFinish).ToList();
            if (finishNode.Count() != 1 || finishNode.SingleOrDefault() == null)
            {
                throw new NoSingleFinishNodeException();
            }

            return finishNode.Single();
        }
    }
}
