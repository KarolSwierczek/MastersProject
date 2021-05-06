using System;
using UnityEngine;

namespace Rooms.ObstacleGeneration
{
    public static class ObstacleGeneration
    {
        private static IObstacleGenerationMask _maskInstance;

        public static int GetObstacleProbabilityOnTile(int tileIndex, ObstacleGenerationMaskType maskType, Vector2Int roomSize, int maxValue)
        {
            if (_maskInstance == null || _maskInstance.MaskType != maskType)
            {
                _maskInstance = GetMaskInstance(maskType, roomSize, maxValue);
            }

            if (_maskInstance.RoomSize != roomSize || _maskInstance.MaxValue != maxValue)
            {
                _maskInstance.Initialize(roomSize, maxValue);
            }

            return _maskInstance.GetRoomTileWeight(tileIndex);
        }

        private static IObstacleGenerationMask GetMaskInstance(ObstacleGenerationMaskType maskType, Vector2Int roomSize, int maxValue)
        {
            IObstacleGenerationMask maskInstance;
            
            switch (maskType)
            {
                case ObstacleGenerationMaskType.None:
                    throw new ArgumentException("Cannot instantiate a obstacle generation mask of type None!");
                case ObstacleGenerationMaskType.CircleCenter:
                    maskInstance = new CircleCenterMask();
                    break;
                case ObstacleGenerationMaskType.CircleEdge:
                    maskInstance = new CircleEdgeMask();
                    break;
                case ObstacleGenerationMaskType.RectangleCenter:
                    maskInstance = new RectangleCenterMask();
                    break;
                case ObstacleGenerationMaskType.RectangleEdge:
                    maskInstance = new RectangleEdgeMask();
                    break;
                case ObstacleGenerationMaskType.WallCenter:
                    maskInstance = new WallCenterMask();
                    break;
                case ObstacleGenerationMaskType.GradualFar:
                    maskInstance = new GradualFarMask();
                    break;
                case ObstacleGenerationMaskType.GradualNear:
                    maskInstance = new GradualNearMask();
                    break;
                case ObstacleGenerationMaskType.Cross:
                    maskInstance = new CrossMask();
                    break;
                case ObstacleGenerationMaskType.Corners:
                    maskInstance = new CornersMask();
                    break;
                default:
                    throw new ArgumentException($"Obstacle generation mask type {maskType} is not handled!");
            }
            
            maskInstance.Initialize(roomSize, maxValue);
            return maskInstance;
        }
    }
}