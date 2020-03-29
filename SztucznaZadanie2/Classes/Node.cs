using System.Collections.Generic;

namespace SztucznaZadanie2
{

    public class Node
    {
        public Result Status { get; set; } //wartość wezła ze wzgledu na minmax (0 dla kontynuacji, 1 atanagonista, 2 remis, 3 protagonista)
        public int GameValue { get; set; } //suma wartości zetonow w grze oczko
        public Turn TurnOf { get; set; } //kto wykonuje nastepny ruch
        public List<Edge> Edges { get;  } //lista krawedzi wychodzacych z wezla

        public Node(int gameValue, Turn turnOf)
        {
            GameValue = gameValue;
            TurnOf = turnOf;
            Status = Result.GameContinues;
            Edges = new List<Edge>();
        }

        public override string ToString()
        {
            if (GameValue < 21)
            {
                return $"Node: value: {GameValue}, turn of: {TurnOf}";
            }
            return $"Node: value {GameValue}, {Status.ToString()}";
        }
    }

}