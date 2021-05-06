using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Data;
using Pathfinding;
using Rooms.ObstacleGeneration;
using Random = UnityEngine.Random;

namespace Rooms
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private RoomGenerationSettings _Settings;
        [SerializeField] private RoomTile _TilePrefab;
        [SerializeField] private Transform _TileParent;
        [SerializeField] private LineRenderer _PathLine;

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
                    SpawnRoomTile(node);
                }
            }

            //generate path
            var pathfinding = new Pathfinding.Pathfinding(_room, roomSizeX, roomSizeY);
            var startNodeIndex = roomSizeX * (roomSizeY / 2);
            var goalNodeIndex = startNodeIndex + roomSizeX - 1;

            _path = pathfinding.FindPath(_room[startNodeIndex], _room[goalNodeIndex]);
            var positions = _path.Select(GetTilePosition).ToArray();
            _PathLine.positionCount = positions.Length;
            _PathLine.SetPositions(positions);
            _PathLine.enabled = true;
        }

        [Button]
        private void GenerateObstacleWeights(ObstacleGenerationMaskType maskType)
        {
            _PathLine.enabled = false;
            
            for(var i = 0; i < _room.Length; i++)
            {
                _room[i].Weight =
                    ObstacleGeneration.ObstacleGeneration.GetObstacleProbabilityOnTile(i, _roomSize, maskType,
                        _Settings.MaxTileWeight);
            }

            ZeroTilesOnPath();
            UpdateTiles();
        }
        
        [Button]
        private void GenerateObstacleWeights(Texture2D texture)
        {
            _PathLine.enabled = false;
            
            for(var i = 0; i < _room.Length; i++)
            {
                _room[i].Weight =
                    ObstacleGeneration.ObstacleGeneration.GetObstacleProbabilityOnTile(i, _roomSize, texture,
                        _Settings.MaxTileWeight);
            }
            
            ZeroTilesOnPath();
            UpdateTiles();
        }

        private void Reset()
        {
            foreach (var tile in _spawnedTiles)
            {
                Destroy(tile.gameObject);
            }
            _spawnedTiles.Clear();
        }

        private void SpawnRoomTile(Node node)
        {
            var position = new Vector3(node.X * _Settings.TileSize, 0f, node.Y * _Settings.TileSize);
            var tile = Instantiate(_TilePrefab, position, Quaternion.identity, _TileParent);
            _spawnedTiles.Add(tile);
            tile.Weight = node.Weight;
        }

        private void UpdateTiles()
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
    }
}