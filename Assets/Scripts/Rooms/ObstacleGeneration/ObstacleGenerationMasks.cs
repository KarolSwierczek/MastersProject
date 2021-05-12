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
            
            var edgePosition = roomSize.x > roomSize.y
                ? new Vector2(0.5f, roomSize.y / 2f)
                : new Vector2(roomSize.x / 2f, 0.5f);
            _maxDistanceFromCenter = Vector2.Distance(edgePosition, ReferencePosition);
            
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

            var edgePosition = new Vector2(0.5f, roomSize.y / 2f);
            _maxDistanceFromCenter = Vector2.Distance(edgePosition, ReferencePosition);
            
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
            _maxDistanceFromCross = Mathf.Min(
                Mathf.Abs(ReferencePosition.x - cornerPosition.x),
                Mathf.Abs(ReferencePosition.y - cornerPosition.y));
            
            MaxHeuresticValue = Heurestic(MaxHeuristicPosition);
        }
    }
    
    public class CornersMask : BaseObstacleGenerationMask
    {
        protected override ObstacleGenerationMaskType MaskType => ObstacleGenerationMaskType.Corners;

        private Vector2 _lowerLeftCorner;
        private Vector2 _lowerRightCorner;
        private Vector2 _upperLeftCorner;
        private Vector2 _upperRightCorner;
        private float _distanceToCenter;

        protected override float Heurestic(Vector2 tilePosition)
        {
            var distanceToNearestCorner  = Mathf.Min(
                Vector2.Distance(_lowerLeftCorner, tilePosition), 
                Vector2.Distance(_lowerRightCorner, tilePosition), 
                Vector2.Distance(_upperLeftCorner, tilePosition), 
                Vector2.Distance(_upperRightCorner, tilePosition));

            return 1 - (distanceToNearestCorner / _distanceToCenter);
        }

        protected override void InitializeMask(Vector2Int roomSize, int maxValue)
        {
            RoomSize = roomSize;
            MaxValue = maxValue;
            MaxHeuristicPosition = Vector2.one * 0.5f;

            _lowerLeftCorner = Vector2.one * 0.5f;
            _lowerRightCorner = new Vector2(RoomSize.x - 0.5f, 0.5f);
            _upperLeftCorner = new Vector2(0.5f, RoomSize.y - 0.5f);
            _upperRightCorner = new Vector2(RoomSize.x - 0.5f, RoomSize.y - 0.5f);

            _distanceToCenter = Vector2.Distance(_lowerLeftCorner, new Vector2(roomSize.x / 2f, roomSize.y / 2f));
            
            MaxHeuresticValue = Heurestic(MaxHeuristicPosition);
        }
    }
}