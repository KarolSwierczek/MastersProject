using System.Collections.Generic;
using UnityEngine;

namespace Rooms.ObstacleGeneration
{
    public interface IObstacleGroupingMask
    {
        ObstacleGroupingMaskType MaskType { get; }
        Vector2Int RoomSize { get; }

        void Initialize(Vector2Int roomSize);
        IEnumerable<KeyValuePair<int, float>> GetAffectedTileWeightModifiers(int centerTileIndex);
    }
}