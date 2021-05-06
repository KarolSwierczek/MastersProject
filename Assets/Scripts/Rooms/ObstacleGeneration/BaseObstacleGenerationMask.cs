using UnityEngine;

namespace Rooms.ObstacleGeneration
{
    /// <summary>
    /// Base class for all obstacle generation masks.
    /// By default the heuristic function calculates distance from the center of the room
    /// and has max value at the corners of the room.
    /// For the purpose of distance calculations, the tile size is always 1.
    /// </summary>
    public abstract class BaseObstacleGenerationMask : IObstacleGenerationMask
    {
        ObstacleGenerationMaskType IObstacleGenerationMask.MaskType => MaskType;
        Vector2Int IObstacleGenerationMask.RoomSize => RoomSize;
        int IObstacleGenerationMask.MaxValue => MaxValue;
        
        protected Vector2Int RoomSize;
        protected int MaxValue;

        protected Vector2 ReferencePosition;
        protected Vector2 MaxHeuristicPosition;
        protected float MaxHeuresticValue;

        protected abstract ObstacleGenerationMaskType MaskType { get; }

        void IObstacleGenerationMask.Initialize(Vector2Int roomSize, int maxValue)
        {
            InitializeMask(roomSize, maxValue);
        }
        
        int IObstacleGenerationMask.GetRoomTileWeight(int tileIndex)
        {
            var tilePositionX = tileIndex % RoomSize.x + 0.5f;
            var tilePositionY = tileIndex / RoomSize.x + 0.5f;
            
            var heuresticValue = Heurestic(new Vector2(tilePositionX, tilePositionY));
            return Discretization(heuresticValue, MaxHeuresticValue, MaxValue);
        }
        
        protected virtual float Heurestic(Vector2 tilePosition)
        {
            return Vector2.Distance(tilePosition, ReferencePosition);
        }

        protected virtual int Discretization(float heurestic, float maxHeurestic, int maxValue)
        {
            return Mathf.RoundToInt(heurestic * maxValue / maxHeurestic);
        }

        protected virtual void InitializeMask(Vector2Int roomSize, int maxValue)
        {
            RoomSize = roomSize;
            MaxValue = maxValue;
            ReferencePosition = new Vector2(roomSize.x / 2f, roomSize.y / 2f);
            MaxHeuristicPosition = Vector2.one * 0.5f;
            MaxHeuresticValue = Heurestic(MaxHeuristicPosition);
        }
    }
}