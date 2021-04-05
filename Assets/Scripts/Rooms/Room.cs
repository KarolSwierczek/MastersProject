using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Data;
using Pathfinding;
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

        private const float _lineOffset = 1f;

        [Button]
        private void GenerateRoomAndPath()
        {
            //Delete previous room
            Reset();
            
            //generate the room
            var roomSizeX = Random.Range(_Settings.MinRoomSizeX, _Settings.MaxRoomSizeX + 1);
            var roomSizeY = Random.Range(_Settings.MinRoomSizeY, _Settings.MaxRoomSizeY + 1);
            var room = new Node[roomSizeX * roomSizeY];
            
            for (var j = 0; j < roomSizeY; j++)
            {
                for (var i = 0; i < roomSizeX; i++)
                {
                    var weight = Random.Range(0, _Settings.MaxTileWeight + 1);
                    var node = new Node(i, j, weight);
                    room[i + roomSizeX * j] = node;
                    SpawnRoomTile(node);
                }
            }

            //generate path
            var pathfinding = new Pathfinding.Pathfinding(room, roomSizeX, roomSizeY);
            var startNodeIndex = roomSizeX * (roomSizeY / 2);
            var goalNodeIndex = startNodeIndex + roomSizeX - 1;

            var path = pathfinding.FindPath(room[startNodeIndex], room[goalNodeIndex]);
            var positions = path.Select(GetTilePosition).ToArray();
            _PathLine.positionCount = positions.Length;
            _PathLine.SetPositions(positions);
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

        private Vector3 GetTilePosition(Node node)
        {
            return new Vector3(node.X * _Settings.TileSize, _lineOffset, node.Y * _Settings.TileSize);
        }
    }
}