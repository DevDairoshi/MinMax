using System;

namespace SztucznaZadanie2
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(21, Turn.Antagonist,4,5,6);
            //graph.PrintGraph(graph.RootNode);
            Console.ResetColor();

            graph.ProceedMinMaxGameSimulation();

            //Console.WriteLine("DRAWS: {0}\nATNWINS: {1}\nPROTWINS: {2}", Graph.draws, Graph.antwins, Graph.protwins);
        }
    }
}
