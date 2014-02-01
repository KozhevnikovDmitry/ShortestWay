using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ShortestWay.Exceptions;

namespace ShortestWay.Model
{
    [XmlRoot("graph")]
    public class Graph
    {
        [XmlElement("node")]
        public virtual Node[] Nodes { get; set; }

        public void Markup()
        {
            foreach (var node in Nodes)
            {
                node.Graph = this;
                node.Mark = node.IsStart ? (int?) 0 : null;
            }
        }

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

            var finishNode = Nodes.Where(t => t.IsFinish).ToList();
            if (finishNode.Count() != 1 || finishNode.SingleOrDefault() == null)
            {
                throw new NoSingleFinishNodeException();
            }

            if (finishNode.Single().IsCrash)
            {
                throw new FinishNodeIsCrashedException(finishNode.Single().Id);
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

        public virtual IList<Node> Linked(Node node)
        {
            if (Nodes == null || !Nodes.Any())
            {
                throw new NoNodesException();
            }

            return Nodes.Where(t => t.IsLinked(node)).ToList();
        }
    }
}
