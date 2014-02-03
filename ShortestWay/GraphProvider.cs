using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ShortestWay.Exceptions;
using ShortestWay.Model;

namespace ShortestWay
{
    /// <summary>
    /// Provides graph examples from external xml-files.
    /// </summary>
    public class GraphProvider
    {
        /// <summary>
        /// Load and returns graph <typeparam name="T"/> example from external source by path.
        /// </summary>
        /// <typeparam name="T">Particular graph type</typeparam>
        /// <param name="sourcePath">path to xml-source</param>
        /// <exception cref="SourceIsNotValidException"></exception>
        /// <exception cref="SourceIsNotExistsException"></exception>
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