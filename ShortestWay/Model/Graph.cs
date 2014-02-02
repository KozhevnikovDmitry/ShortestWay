using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ShortestWay.Exceptions;

namespace ShortestWay.Model
{
    public interface IGraph
    {}

    [XmlRoot("graph")]
    public class Graph<T> : IGraph where T : Node
    {
        [XmlElement("node")]
        public virtual T[] Nodes { get; set; }

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

                    if (validated.IsLinked(node) && !node.IsLinked(validated))
                    {
                        throw new NodeLinkIsNotBidirectionalException(validated.Id, node.Id);
                    }

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

        public virtual IList<T> Linked(Node node)
        {
            if (Nodes == null || !Nodes.Any())
            {
                throw new NoNodesException();
            }

            return Nodes.Where(t => t.IsLinked(node)).ToList();
        }

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
