using System.Linq;
using System.Xml.Serialization;
using ShortestWay.Exceptions;

namespace ShortestWay.Model
{
    /// <summary>
    /// Road node - top of the graph
    /// </summary>
    [XmlRoot("node")]
    public class Node
    {
        [XmlAttribute("id")]
        public virtual int Id { get; set; }

        [XmlAttribute("role")]
        public virtual string Role { get; set; }

        [XmlAttribute("status")]
        public virtual string Status { get; set; }

        [XmlElement("link")]
        public virtual Link[] Links { get; set; }

        public virtual bool IsStart
        {
            get { return Role == "start"; }
        }

        public virtual bool IsFinish
        {
            get { return Role == "finish"; }
        }

        public virtual bool IsCrash
        {
            get { return Status == "crash"; }
        }

        /// <summary>
        /// Returns true if current node is linked provided <paramref name="node"/>
        /// </summary>
        /// <exception cref="LinksAreNotSetupException"></exception>
        public virtual bool IsLinked(Node node)
        {
            if (node.Equals(this))
            {
                return false;
            }

            if(Links == null)
                throw new LinksAreNotSetupException(Id); 

            return Links.Any(t => t.Ref == node.Id);
        }

        /// <summary>
        /// Returns weight of link between current and provided <paramref name="node"/>
        /// </summary>
        /// <exception cref="NodesAreNotLinkedToGetWeghtException"></exception>
        public virtual int LinkWeight(Node node)
        {
            if (!IsLinked(node))
            {
                throw new NodesAreNotLinkedToGetWeghtException(Id, node.Id);
            }

            return Links.Where(t => t.Ref == node.Id).Select(t => t.Weight).Min();
        }
    }
}