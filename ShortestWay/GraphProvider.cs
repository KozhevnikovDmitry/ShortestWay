using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ShortestWay.Exceptions;
using ShortestWay.Model;

namespace ShortestWay
{
    public class GraphProvider
    {
        public T Load<T>(string sourcePath) where T : IGraph
        {
            if (!File.Exists(sourcePath))
            {
                throw new SourceIsNotExistsException(sourcePath);
            }

            try
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                using (var xr = new XmlTextReader(sourcePath))
                {
                    var result = (T)xmlSerializer.Deserialize(xr);
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