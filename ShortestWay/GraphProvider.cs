using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ShortestWay.Exceptions;
using ShortestWay.Model;

namespace ShortestWay.Tests
{
    public class GraphProvider
    {
        public Graph Load(string sourcePath)
        {
            if (!File.Exists(sourcePath))
            {
                throw new SourceIsNotExistsException(sourcePath);
            }

            try
            {
                var xmlSerializer = new XmlSerializer(typeof(Graph));
                using (var xr = new XmlTextReader(sourcePath))
                {
                    var result = (Graph)xmlSerializer.Deserialize(xr);
                    foreach (var node in result.Nodes)
                    {
                        node.Graph = result;
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new SourceIsNotValidException(ex);
            }
        }
    }
}