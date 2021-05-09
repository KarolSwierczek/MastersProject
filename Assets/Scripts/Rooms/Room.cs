using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Data;
using Difficulty;
using Pathfinding;
using Rooms.ObstacleGeneration;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

namespace Rooms
{
    //todo: move some methods to room generation class
    public class Room : MonoBehaviour
    {
        [SerializeField] private RoomGenerationSettings _Settings;
        [SerializeField] private RoomTile _TilePrefab;
        [SerializeField] private Transform _TileParent;
        [FormerlySerializedAs("_PathLine")] 
        [SerializeField] private LineRenderer _DebugPathLine;

        [Inject] private readonly IDifficultySystem _difficultySystem;

        private readonly List<RoomTile> _spawnedTiles = new List<RoomTile>();
        private Node[] _room;
        private IEnumerable<Node> _path;
        private Vector2Int _roomSize;

        private const float _lineOffset = 1f;

        [Button]
        private void GenerateRoomAndPath()
        {
            //Delete previous room
            Reset();
            
            //generate the room
            var roomSizeX = Random.Range(_Settings.MinRoomSizeX, _Settings.MaxRoomSizeX + 1);
            var roomSizeY = Random.Range(_Settings.MinRoomSizeY, _Settings.MaxRoomSizeY + 1);
            _room = new Node[roomSizeX * roomSizeY];
            _roomSize = new Vector2Int(roomSizeX, roomSizeY);
            
            for (var j = 0; j < roomSizeY; j++)
            {
                for (var i = 0; i < roomSizeX; i++)
                {
                    var weight = Random.Range(0, _Settings.MaxTileWeight + 1);
                    var node = new Node(i, j, weight);
                    _room[i + roomSizeX * j] = node;
                    SpawnDebugRoomTile(node);
                }
            }

            //generate path
            var pathfinding = new Pathfinding.Pathfinding(_room, roomSizeX, roomSizeY);
            
            var startNodeIndex = roomSizeX * (roomSizeY / 2);
            var goalNodeIndex = startNodeIndex + roomSizeX - 1;
            var nodeIndices = GetIndicesWithViaPoints(_Settings.RandomViaPointCount, 0, _room.Length,
                startNodeIndex, goalNodeIndex);

            _path = pathfinding.FindPath(nodeIndices.ToArray());
            var positions = _path.Select(GetTilePosition).ToArray();
            _DebugPathLine.positionCount = positions.Length;
            _DebugPathLine.SetPositions(positions);
            _DebugPathLine.enabled = true;
        }

        [Button]
        private void GenerateObstacleWeights(ObstacleGenerationMaskType maskType)
        {
            _DebugPathLine.enabled = false;
            
            for(var i = 0; i < _room.Length; i++)
            {
                _room[i].Weight = ObstacleProbabilityGeneration.GetObstacleProbabilityOnTile(i, _roomSize, maskType, _Settings.MaxTileWeight);
            }

            ZeroTilesOnPath();
            UpdateDebugTileWeights();
        }
        
        [Button]
        private void GenerateObstacleWeights(Texture2D texture)
        {
            _DebugPathLine.enabled = false;
            
            for(var i = 0; i < _room.Length; i++)
            {
                _room[i].Weight = ObstacleProbabilityGeneration.GetObstacleProbabilityOnTile(i, _roomSize, texture, _Settings.MaxTileWeight);
            }
            
            ZeroTilesOnPath();
            UpdateDebugTileWeights();
        }

        [Button]
        private void GenerateObstacles()
        {
            //todo: apply scattered/clumped mask after each element is spawned
            //todo: reset changes from masks after each tile type (to avoid clumping between different tile types)
            var tileWeights = new WeightArray(_room);

            GenerateObstaclesOfType(TileType.Column, tileWeights);
            GenerateObstaclesOfType(TileType.Box, tileWeights);
            GenerateObstaclesOfType(TileType.Spikes, tileWeights);
        }

        private void GenerateObstaclesOfType(TileType type, WeightArray tileWeights)
        {
            var numOfObstacles = _difficultySystem.GetNumberOfTiles(type, _room.Length);
            for (var i = 0; i < numOfObstacles; i++)
            {
                var obstacleIndex = tileWeights.GetRandomWeightedIndex();
                _spawnedTiles[obstacleIndex].Weight = 0;
                _spawnedTiles[obstacleIndex].Type = type;
                tileWeights.UpdateWeight(obstacleIndex, 0);
            }
        }

        private void Reset()
        {
            foreach (var tile in _spawnedTiles)
            {
                Destroy(tile.gameObject);
            }
            _spawnedTiles.Clear();
        }

        private void SpawnDebugRoomTile(Node node)
        {
            var position = new Vector3(node.X * _Settings.TileSize, 0f, node.Y * _Settings.TileSize);
            var tile = Instantiate(_TilePrefab, position, Quaternion.identity, _TileParent);
            _spawnedTiles.Add(tile);
            tile.Weight = node.Weight;
        }

        private void UpdateDebugTileWeights()
        {
            for (var i = 0; i < _spawnedTiles.Count; i++)
            {
                _spawnedTiles[i].Weight = _room[i].Weight;
            }
        }

        private void ZeroTilesOnPath()
        {
            foreach (var pathTile in _path)
            {
                pathTile.Weight = 0;
            }
        }

        private Vector3 GetTilePosition(Node node)
        {
            return new Vector3(node.X * _Settings.TileSize, _lineOffset, node.Y * _Settings.TileSize);
        }
        
        //todo: move to utils
        private static IEnumerable<int> GetIndicesWithViaPoints(int numOfViaPoints, int rangeStart, int rangeCount, int startIndex, int goalIndex)
        {
            var result = new List<int>{startIndex};
            var availableIndices =
                Enumerable.Range(rangeStart, rangeCount).Except(new[] {startIndex, goalIndex}).ToList();
            
            //shuffle availableIndices
            for (var i = 0; i < availableIndices.Count; i++) {
                var temp = availableIndices[i];
                var randomIndex = Random.Range(i, availableIndices.Count);
                availableIndices[i] = availableIndices[randomIndex];
                availableIndices[randomIndex] = temp;
            }
            
            return availableIndices.Take(numOfViaPoints).Prepend(startIndex).Append(goalIndex);
        }
    }
}