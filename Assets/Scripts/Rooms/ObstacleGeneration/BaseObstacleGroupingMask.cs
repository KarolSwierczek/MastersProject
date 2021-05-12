using System.Collections.Generic;
using UnityEngine;

namespace Rooms.ObstacleGeneration
{
    /// <summary>
    /// Base class for all obstacle grouping masks
    /// </summary>
    public abstract class BaseObstacleGroupingMask : IObstacleGroupingMask
    {
        ObstacleGroupingMaskType IObstacleGroupingMask.MaskType => MaskType;

        Vector2Int IObstacleGroupingMask.RoomSize => RoomSize;
        
        protected abstract ObstacleGroupingMaskType MaskType { get; }
        protected abstract float[] AffectedTileWeightModifiers { get; }

        protected Vector2Int RoomSize;
        private int[] _affectedTileIndexDeltas; 

        void IObstacleGroupingMask.Initialize(Vector2Int roomSize)
        {
            RoomSize = roomSize;
            _affectedTileIndexDeltas = new [] {-RoomSize.x - 1, -RoomSize.x, -RoomSize.x + 1, 1, RoomSize.x + 1, RoomSize.x, RoomSize.x - 1, -1};
        }

        IEnumerable<KeyValuePair<int, float>> IObstacleGroupingMask.GetAffectedTileWeightModifiers(int centerTileIndex)
        {
            var result = new List<KeyValuePair<int, float>>();
            for (var i = 0; i < _affectedTileIndexDeltas.Length; i++)
            {
                var tileIndex = _affectedTileIndexDeltas[i] + centerTileIndex;
                if (IsTileIndexInRange(tileIndex))
                {
                    result.Add(new KeyValuePair<int, float>(tileIndex, AffectedTileWeightModifiers[i]));
                }
            }
            
            return result;
        }

        private bool IsTileIndexInRange(int tileIndex)
        {
            return tileIndex < RoomSize.x * RoomSize.y && tileIndex >= 0;
        }
    }
}