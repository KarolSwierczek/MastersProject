using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rooms.ObstacleGeneration
{
    public static class ObstacleGrouping
    {
        private static IObstacleGroupingMask _maskInstance;

        public static IEnumerable<KeyValuePair<int, float>> GetAffectedTileIndices(int centerTileIndex, Vector2Int roomSize, ObstacleGroupingMaskType maskType)
        {
            if (_maskInstance == null || _maskInstance.MaskType != maskType)
            {
                _maskInstance = GetMaskInstance(maskType, roomSize);
            }

            if (_maskInstance.RoomSize != roomSize)
            {
                _maskInstance.Initialize(roomSize);
            }

            return _maskInstance.GetAffectedTileWeightModifiers(centerTileIndex);
        }

        private static IObstacleGroupingMask GetMaskInstance(ObstacleGroupingMaskType maskType, Vector2Int roomSize)
        {
            IObstacleGroupingMask maskInstance;
            
            switch (maskType)
            {
                case ObstacleGroupingMaskType.Clump:
                    maskInstance = new ClumpMask();
                    break;
                case ObstacleGroupingMaskType.Scatter:
                    maskInstance = new ScatterMask();
                    break;
                case ObstacleGroupingMaskType.HorizontalLines:
                    maskInstance = new HorizontalLinesMask();
                    break;
                case ObstacleGroupingMaskType.VerticalLines:
                    maskInstance = new VerticalLinesMask();
                    break;
                default:
                    throw new ArgumentException($"Obstacle generation mask type {maskType} is not handled!");
            }
            
            maskInstance.Initialize(roomSize);
            return maskInstance;
        }
    }
}