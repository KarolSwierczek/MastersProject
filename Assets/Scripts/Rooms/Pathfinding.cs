using System.Collections.Generic;
using System;
using Utils;

namespace Rooms
{
    public class Pathfinding
    {
        private readonly int _roomSizeX;
        private readonly int _roomSizeY;
        private readonly Node[] _room;
        private readonly MinHeap<Node> _openSet;


        public Pathfinding(Node[] room, int roomSizeX, int roomSizeY)
        {
            _room = room;
            _roomSizeX = roomSizeX;
            _roomSizeY = roomSizeY;

            _openSet = new MinHeap<Node>(_room.Length);
        }
        
        public IEnumerable<Node> FindPath(Node start, Node goal)
        {
            _openSet.Add(start);
            start.GScore = 0;
            start.FScore = Heuristic(start, goal);

            while (!_openSet.IsEmpty())
            {
                var current = _openSet.Pop();
                if (current.Equals(goal))
                {
                    return ReconstructPath(current);
                }

                foreach (var neighbor in GetNeighbors(current))
                {
                    var tentativeGScore = current.GScore + neighbor.Weight;
                    if (tentativeGScore < neighbor.GScore)
                    {
                        neighbor.CameFrom = current;
                        neighbor.GScore = tentativeGScore;
                        neighbor.FScore = neighbor.GScore + Heuristic(neighbor, goal);
                        if (!_openSet.Contains(neighbor))
                        {
                            _openSet.Add(neighbor);
                        }
                    }
                }
            }

            throw new Exception("Cannot find a path from start to goal!");
        }

        private Node GetNodeAtCoordinates(int x, int y)
        {
            return _room[x + _roomSizeX * y];
        }

        private static int Heuristic(Node start, Node goal)
        {
            var dx = goal.X - start.X;
            var dy = goal.Y - start.Y;
            return Math.Abs(dx) + Math.Abs(dy);
        }

        private IEnumerable<Node> GetNeighbors(Node node)
        {
            var x = node.X;
            var y = node.Y;
            
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    if(i == 0 && j == 0){continue;}
                    if(!IsValidNeighbor(i + x, j + y)){continue;}

                    yield return GetNodeAtCoordinates(i + x, j + y);
                }
            }
        }

        private bool IsValidNeighbor(int x, int y)
        {            
            if (_room == null)
            {
                throw new NullReferenceException("Trying to get a node, but the room is null!");
            }
            if (x < 0 || x >= _room.Length)
            {
                return false;
            }
            if (y < 0 || y >= _room.Length)
            {
                return false;
            }
            return true;
        }

        private static IEnumerable<Node> ReconstructPath(Node target)
        {
            var current = target;
            while (current.CameFrom != null)
            {
                yield return current;
                current = current.CameFrom;
            }

            yield return current;
        }
    }
}