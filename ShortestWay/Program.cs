using System;
using System.Collections.Generic;
using System.Linq;
using ShortestWay.Dijkstra;
using ShortestWay.Model;

namespace ShortestWay
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Shortest way by Kozhevnikov Dmitry");
            Process(args);
            Console.WriteLine("Press any key to escape");
            Console.ReadKey();
        }

        private static void Process(string[] args)
        {
            try
            {
                if (args.Count() > 1)
                {
                    Console.WriteLine("Wrong number of arguments");
                    return;
                }

                if (!args.Any())
                {
                    Console.WriteLine("Path was not specified");
                    return;
                }

                var arg = args.Single();
                FindWay(arg);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
        }

        private static void FindWay(string path)
        {
            var graph = new GraphProvider().Load<DijkstraGraph>(path);
            graph.Validate();
            graph.Markup();
            var way = new Dijkstra.Dijkstra()
                                  .Compute(graph)
                                  .GetShortestWay(graph);
            PrintResults(way);
            
        }

        private static void PrintResults(List<Node> way)
        {
            Console.Write("The shortest way from finish to start is: [");
            foreach (var node in way)
            {
                Console.Write(node.Id);
                if (node != way.Last())
                {
                    Console.Write(", ");
                }
            }

            Console.WriteLine(string.Format("]"));
        }
    }
}
