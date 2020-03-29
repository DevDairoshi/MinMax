namespace SztucznaZadanie2
{

    public class Edge
    {
        public int Value { get; set; }
        public Node Previous { get; set; }
        public Node Next { get; set; }

        public Edge(int value, Node previous, Node next)
        {
            Value = value;
            Previous = previous;
            Next = next;
        }

        public override string ToString()
        {
            return $"Edge: value {Value}";
        }
    }

}