using System.Collections.Generic;
using UnityEngine;
using System;
using Utils;

namespace Rooms
{
    public class Pathfinding
    {
        private readonly int _roomSizeX;
        private readonly int _roomSizeY;
        private readonly int[] _room;
        private MinHeap<Node> _openSet;


        public Pathfinding(int[] room, int roomSizeX, int roomSizeY)
        {
            _room = room;
            _roomSizeX = roomSizeX;
            _roomSizeY = roomSizeY;

            _openSet = new MinHeap<Node>(_room.Length);
        }
        
        public List<Node> FindPath(int[][] graph, Node start, Node goal)
        {

            openSet[start] = true;
            gScore[start] = 0;
            fScore[start] = Heuristic(start, goal);

            while (openSet.Count > 0)
            {
                var current = nextBest();
                if (current.Equals(goal))
                {
                    return Reconstruct(current);
                }


                openSet.Remove(current);
                closedSet[current] = true;

                foreach (var neighbor in Neighbors(graph, current))
                {
                    if (closedSet.ContainsKey(neighbor))
                        continue;

                    var projectedG = getGScore(current) + 1;

                    if (!openSet.ContainsKey(neighbor))
                        openSet[neighbor] = true;
                    else if (projectedG >= getGScore(neighbor))
                        continue;

                    //record it
                    nodeLinks[neighbor] = current;
                    gScore[neighbor] = projectedG;
                    fScore[neighbor] = projectedG + Heuristic(neighbor, goal);

                }
            }


            return new List<Vector2>();
        }

        private Node GetNodeAtCoordinates(int x, int y)
        {
            throw new NotImplementedException();
        }

        private int Heuristic(Node start, Node goal)
        {
            var dx = goal.X - start.X;
            var dy = goal.Y - start.Y;
            return Math.Abs(dx) + Math.Abs(dy);
        }

        private IEnumerable<Node> GetNeighbors(Node node)
        {

            Vector2 pt = new Vector2(center.X - 1, center.Y - 1);
            if (IsValidNeighbor(graph, pt))
                yield return pt;

            pt = new Vector2(center.X, center.Y - 1);
            if (IsValidNeighbor(graph, pt))
                yield return pt;

            pt = new Vector2(center.X + 1, center.Y - 1);
            if (IsValidNeighbor(graph, pt))
                yield return pt;

            //middle row
            pt = new Vector2(center.X - 1, center.Y);
            if (IsValidNeighbor(graph, pt))
                yield return pt;

            pt = new Vector2(center.X + 1, center.Y);
            if (IsValidNeighbor(graph, pt))
                yield return pt;


            //bottom row
            pt = new Vector2(center.X - 1, center.Y + 1);
            if (IsValidNeighbor(graph, pt))
                yield return pt;

            pt = new Vector2(center.X, center.Y + 1);
            if (IsValidNeighbor(graph, pt))
                yield return pt;

            pt = new Vector2(center.X + 1, center.Y + 1);
            if (IsValidNeighbor(graph, pt))
                yield return pt;
            
        }

        private bool IsValidNeighbor(int x, int y)
        {
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

        private List<Node> ReconstructPath(Node target)
        {
            var current = target;
            var path = new List<Node>();
            while (current.CameFrom != null)
            {
                path.Add(current);
                current = current.CameFrom;
            }

            path.Reverse();
            return path;
        }
    }
}