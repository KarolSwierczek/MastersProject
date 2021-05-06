using UnityEngine;

namespace Rooms.ObstacleGeneration
{
    public class CircleCenterMask : BaseObstacleGenerationMask
    {
        protected override ObstacleGenerationMaskType MaskType => ObstacleGenerationMaskType.CircleCenter;
        
        private float _maxDistanceFromCenter;

        protected override float Heurestic(Vector2 tilePosition)
        {
            var distanceFromCenter = Vector2.Distance(tilePosition, ReferencePosition);
            return _maxDistanceFromCenter - distanceFromCenter;
        }

        protected override void InitializeMask(Vector2Int roomSize, int maxValue)
        {
            RoomSize = roomSize;
            MaxValue = maxValue;
            ReferencePosition = new Vector2(roomSize.x / 2f, roomSize.y / 2f);
            MaxHeuristicPosition = ReferencePosition;

            var cornerPosition = Vector2.one * 0.5f;
            _maxDistanceFromCenter = Vector2.Distance(cornerPosition, ReferencePosition);
            
            MaxHeuresticValue = Heurestic(MaxHeuristicPosition);
        }
    }
    
    public class CircleEdgeMask : BaseObstacleGenerationMask
    {
        protected override ObstacleGenerationMaskType MaskType => ObstacleGenerationMaskType.CircleEdge;
    }
    
    public class RectangleCenterMask : BaseObstacleGenerationMask
    {
        protected override ObstacleGenerationMaskType MaskType => ObstacleGenerationMaskType.RectangleCenter;

        private float _maxDistanceFromCenter;
        
        protected override float Heurestic(Vector2 tilePosition)
        {
            var distanceFromCenter = Mathf.Max(
                Mathf.Abs(ReferencePosition.x - tilePosition.x),
                Mathf.Abs(ReferencePosition.y - tilePosition.y));
            
            return _maxDistanceFromCenter - distanceFromCenter;
        }

        protected override void InitializeMask(Vector2Int roomSize, int maxValue)
        {
            RoomSize = roomSize;
            MaxValue = maxValue;
            ReferencePosition = new Vector2(roomSize.x / 2f, roomSize.y / 2f);
            MaxHeuristicPosition = ReferencePosition;

            var cornerPosition = Vector2.one * 0.5f;
            _maxDistanceFromCenter = Vector2.Distance(cornerPosition, ReferencePosition);
            
            MaxHeuresticValue = Heurestic(MaxHeuristicPosition);
        }
    }
    
    public class RectangleEdgeMask : BaseObstacleGenerationMask
    {
        protected override ObstacleGenerationMaskType MaskType => ObstacleGenerationMaskType.RectangleEdge;

        protected override float Heurestic(Vector2 tilePosition)
        {
            return Mathf.Max(
                Mathf.Abs(ReferencePosition.x - tilePosition.x),
                Mathf.Abs(ReferencePosition.y - tilePosition.y));
        }
    }
    
    public class WallCenterMask : BaseObstacleGenerationMask
    {
        protected override ObstacleGenerationMaskType MaskType => ObstacleGenerationMaskType.WallCenter;

        private float _maxDistanceFromCenter;
        
        protected override float Heurestic(Vector2 tilePosition)
        {
            var distanceFromCenter = Mathf.Abs(ReferencePosition.y - tilePosition.y);
            return _maxDistanceFromCenter - distanceFromCenter;
        }

        protected override void InitializeMask(Vector2Int roomSize, int maxValue)
        {
            RoomSize = roomSize;
            MaxValue = maxValue;
            ReferencePosition = new Vector2(roomSize.x / 2f, roomSize.y / 2f);
            MaxHeuristicPosition = ReferencePosition;

            var cornerPosition = Vector2.one * 0.5f;
            _maxDistanceFromCenter = Vector2.Distance(cornerPosition, ReferencePosition);
            
            MaxHeuresticValue = Heurestic(MaxHeuristicPosition);
        }
    }
    
    public class GradualFarMask : BaseObstacleGenerationMask
    {
        protected override ObstacleGenerationMaskType MaskType => ObstacleGenerationMaskType.GradualFar;
        
        protected override float Heurestic(Vector2 tilePosition)
        {
            return Mathf.Abs(ReferencePosition.y - tilePosition.y);
        }

        protected override void InitializeMask(Vector2Int roomSize, int maxValue)
        {
            RoomSize = roomSize;
            MaxValue = maxValue;
            ReferencePosition = new Vector2(roomSize.x / 2f, 0.5f);
            MaxHeuristicPosition = new Vector2(roomSize.x / 2f, roomSize.y - 0.5f);
            
            MaxHeuresticValue = Heurestic(MaxHeuristicPosition);
        }
    }
    
    public class GradualNearMask : BaseObstacleGenerationMask
    {
        protected override ObstacleGenerationMaskType MaskType => ObstacleGenerationMaskType.GradualNear;
        
        
        protected override float Heurestic(Vector2 tilePosition)
        {
            return Mathf.Abs(ReferencePosition.y - tilePosition.y);
        }

        protected override void InitializeMask(Vector2Int roomSize, int maxValue)
        {
            RoomSize = roomSize;
            MaxValue = maxValue;
            ReferencePosition  = new Vector2(roomSize.x / 2f, roomSize.y - 0.5f);
            MaxHeuristicPosition = new Vector2(roomSize.x / 2f, 0.5f);
            
            MaxHeuresticValue = Heurestic(MaxHeuristicPosition);
        }
    }
    
    public class CrossMask : BaseObstacleGenerationMask
    {
        protected override ObstacleGenerationMaskType MaskType => ObstacleGenerationMaskType.Cross;
        
        private float _maxDistanceFromCross;
        
        protected override float Heurestic(Vector2 tilePosition)
        {
            var distanceFromCross = Mathf.Min(
                Mathf.Abs(ReferencePosition.x - tilePosition.x),
                Mathf.Abs(ReferencePosition.y - tilePosition.y));
            return _maxDistanceFromCross - distanceFromCross;
        }

        protected override void InitializeMask(Vector2Int roomSize, int maxValue)
        {
            RoomSize = roomSize;
            MaxValue = maxValue;
            ReferencePosition = new Vector2(roomSize.x / 2f, roomSize.y / 2f);
            MaxHeuristicPosition = ReferencePosition;

            var cornerPosition = Vector2.one * 0.5f;
            _maxDistanceFromCross = Vector2.Distance(cornerPosition, ReferencePosition);
            
            MaxHeuresticValue = Heurestic(MaxHeuristicPosition);
        }
    }
    
    public class CornersMask : BaseObstacleGenerationMask
    {
        protected override ObstacleGenerationMaskType MaskType => ObstacleGenerationMaskType.Corners;

        protected override float Heurestic(Vector2 tilePosition)
        {
            var lowerLeftCorner = Vector2.one * 0.5f;
            var lowerRightCorner = new Vector2(RoomSize.x - 1f, 0f) + lowerLeftCorner;
            var upperLeftCorner  = new Vector2(0f, RoomSize.y - 1f) + lowerLeftCorner;
            var upperRightCorner = new Vector2(RoomSize.x - 1f, 0f) + lowerRightCorner;

            return Mathf.Max(
                Vector2.Distance(lowerLeftCorner, tilePosition), 
                Vector2.Distance(lowerRightCorner, tilePosition), 
                Vector2.Distance(upperLeftCorner, tilePosition), 
                Vector2.Distance(upperRightCorner, tilePosition));
        }

        protected override void InitializeMask(Vector2Int roomSize, int maxValue)
        {
            RoomSize = roomSize;
            MaxValue = maxValue;
            MaxHeuristicPosition = Vector2.one * 0.5f;

            MaxHeuresticValue = Heurestic(MaxHeuristicPosition);
        }
    }
}