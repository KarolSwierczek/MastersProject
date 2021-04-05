using System.Linq;
using Utils;

namespace Pathfinding
{
    public class OpenSet : MinHeap<Node>
    {
        public bool Contains(Node node)
        {
            return Elements.Contains(node);
        }

        public OpenSet(int size) : base(size)
        {
            
        }
    }
}

