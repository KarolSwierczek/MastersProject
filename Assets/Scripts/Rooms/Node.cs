using System;

namespace Rooms
{
    public class Node : IComparable
    {
        public int X { get; }
        public int Y { get; }
        public int Weight { get; }
        
        public Node CameFrom { get; set; }
        public int GScore { get; set; }
        public int FScore { get; set; }

        public Node(int x, int y, int weight)
        {
            X = x;
            Y = y;
            Weight = weight;
            GScore = int.MaxValue;
            FScore = int.MaxValue;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is Node other))
            {
                throw new ArgumentException("object is not of type Node!");
            }
            return FScore.CompareTo(other.FScore);
        }
    }
}