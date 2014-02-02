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
            var result = 0;
            if (args.Any())
            {
                result= Process(args);
            }
            
            while (result == 0)
            {
                Console.WriteLine("Enter command");
                var command = Console.ReadLine();
                result = Process(new[] { command });
            }
            Console.WriteLine("Press any key to escape");
            Console.ReadKey();
        }

        private static int Process(string[] args)
        {
            try
            {
                if (args.Count() == 1)
                {
                    var arg = args.Single();
                    if (arg.ToUpper() == "EXIT")
                    {
                        return 1;
                    }
                    FindWay(arg);
                }
                else
                {
                    Console.WriteLine("Wrong number of arguments");
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex.Message);
                return 0;
            }
        }

        private static void FindWay(string path)
        {
            var way = Dijkstra(path);
            PrintResults(way);
            
        }

        private static List<Node> Dijkstra(string path)
        {
            var graph = new GraphProvider().Load<DijkstraGraph>(path);
            graph.Validate();
            graph.Markup();
            var dijkstra = new Dijkstra.Dijkstra();
            return dijkstra.Compute(graph).Find(graph);
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
