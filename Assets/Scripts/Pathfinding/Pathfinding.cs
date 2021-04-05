using System.Collections.Generic;
using System;

namespace Pathfinding
{
    public class Pathfinding
    {
        private readonly int _roomSizeX;
        private readonly int _roomSizeY;
        private readonly Node[] _room;
        private readonly OpenSet _openSet;


        public Pathfinding(Node[] room, int roomSizeX, int roomSizeY)
        {
            _room = room;
            _roomSizeX = roomSizeX;
            _roomSizeY = roomSizeY;

            _openSet = new OpenSet(_room.Length);
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

            if (IsValidNeighbor(x + 1, y))
            {
                yield return GetNodeAtCoordinates(x + 1, y);
            }
            if (IsValidNeighbor(x -1, y))
            {
                yield return GetNodeAtCoordinates(x - 1, y);
            }            
            if (IsValidNeighbor(x, y + 1))
            {
                yield return GetNodeAtCoordinates(x, y + 1);
            }            
            if (IsValidNeighbor(x, y - 1))
            {
                yield return GetNodeAtCoordinates(x, y - 1);
            }
        }

        private bool IsValidNeighbor(int x, int y)
        {            
            if (_room == null)
            {
                throw new NullReferenceException("Trying to get a node, but the room is null!");
            }
            if (x < 0 || x >= _roomSizeX)
            {
                return false;
            }
            if (y < 0 || y >= _roomSizeY)
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