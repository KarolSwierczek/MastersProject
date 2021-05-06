using System;

namespace Pathfinding
{
    public class Node : IComparable, IEquatable<Node>
    {
        public int X { get; }
        public int Y { get; }
        
        public int Weight { get; set; }
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

        public bool Equals(Node other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Node)) return false;
            return Equals((Node) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static bool operator ==(Node left, Node right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Node left, Node right)
        {
            return !Equals(left, right);
        }
    }
}