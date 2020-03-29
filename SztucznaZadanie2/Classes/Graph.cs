using System;
using System.Collections.Generic;

namespace SztucznaZadanie2
{
    public class Graph
    {
        public readonly List<int> EdgeValues;
        public Node RootNode { get; }
        public int WinValue { get; }
        public Graph(int winValue, Turn firstTurn, params int[] edgeValues)
        {
            WinValue = winValue;
            EdgeValues = new List<int>(edgeValues);
            RootNode = new Node(0, firstTurn);

            GenerateGraph(RootNode);
        }

        public static int draws = 0;
        public static int antwins = 0;
        public static int protwins = 0;

        private void GenerateGraph(Node node)
        {
            if (node.GameValue < WinValue)
            {
                node.Status = Result.GameContinues;
            }
            else if (node.GameValue == WinValue)
            {
                node.Status = Result.Draw;
                draws++;
            }
            else
            {
                if (node.TurnOf == Turn.Protagonist)
                {
                    node.Status = Result.ProtagonistWins;
                    protwins++;
                }
                else
                {
                    node.Status = Result.AntagonistWins;
                    antwins++;
                }
            }

            if (node.Status == Result.GameContinues)
            {
                foreach (var edgeValue in EdgeValues)
                {
                    node.Edges.Add(new Edge(edgeValue, node,
                        new Node(node.GameValue + edgeValue, node.TurnOf == Turn.Protagonist ? Turn.Antagonist : Turn.Protagonist)
                        ));
                }
            }

            foreach (var edge in node.Edges)
            {
                GenerateGraph(edge.Next);
            }
        }

        public void PrintGraph(Node node)
        {
            Console.WriteLine(node.ToString());
            foreach (var edge in node.Edges)
            {
                Console.WriteLine(edge.ToString());
            }
            Console.WriteLine();
            foreach (var edge in node.Edges)
            {
                switch (edge.Next.GameValue)
                {
                    case 4:
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                        }
                    case 5:
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        }
                    case 6:
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        }

                }
                PrintGraph(edge.Next);
            }
        }

        public void ProceedMinMaxGameSimulation()
        {
            Console.WriteLine("BlackJack Simulation with MinMax Algorithm");
            Console.WriteLine($"Ending Value: {WinValue}");
            Console.Write("Possible token values:\t\t");
            foreach (var val in EdgeValues)
            {
                Console.Write($"{val}\t");
            }
            Console.WriteLine("\n");

            //

            Node ptr = RootNode;
            while (true)
            {
                GenerateGraph(ptr);
                if (ptr.Status != Result.GameContinues)
                {
                    Console.WriteLine($"Game over, {ptr.Status}");
                    break;
                }

                var choice = MinMax(ptr);
                Console.Write($"Turn of: {ptr.TurnOf}, token choice: ");
                foreach (var edge in ptr.Edges)
                {
                    if ((int)edge.Next.Status == choice)
                    {
                        ptr = edge.Next;
                        Console.WriteLine($"{edge.Value}, game value: {edge.Next.GameValue}");
                        break;
                    }
                }
            }
        }

        private int MinMax(Node node)
        {
            if (node.Status != Result.GameContinues)
            {
                return (int)node.Status;
            }

            switch (node.TurnOf)
            {
                case Turn.Protagonist:
                    {
                        var max = -999;
                        foreach (var edge in node.Edges)
                        {
                            node.Status = (Result)MinMax(edge.Next);
                            max = (int)node.Status > max ? (int)node.Status : max;
                        }

                        node.Status = (Result)max;
                        return max;
                    }
                case Turn.Antagonist:
                    {
                        var min = 999;
                        foreach (var edge in node.Edges)
                        {
                            node.Status = (Result)MinMax(edge.Next);
                            min = (int)node.Status < min ? (int)node.Status : min;
                        }

                        node.Status = (Result)min;
                        return min;
                    }
            }

            return -999;
        }


    }

}