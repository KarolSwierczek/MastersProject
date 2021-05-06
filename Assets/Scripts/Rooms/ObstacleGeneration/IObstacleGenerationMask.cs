using UnityEngine;

namespace Rooms.ObstacleGeneration
{
    public interface IObstacleGenerationMask
    {
        ObstacleGenerationMaskType MaskType { get; }
        Vector2Int RoomSize { get; }
        int MaxValue { get; }
        
        void Initialize(Vector2Int roomSize, int maxValue);
        int GetRoomTileWeight(int tileIndex);
    }
}