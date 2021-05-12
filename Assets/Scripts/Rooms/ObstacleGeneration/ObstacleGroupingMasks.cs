namespace Rooms.ObstacleGeneration
{
    public class ClumpMask : BaseObstacleGroupingMask
    {
        protected override ObstacleGroupingMaskType MaskType => ObstacleGroupingMaskType.Clump;

        protected override float[] AffectedTileWeightModifiers => new[]
            {
                3f, 3f, 3f, 
                3f,     3f, 
                3f, 3f, 3f
            };
    }
    
    public class ScatterMask : BaseObstacleGroupingMask
    {
        protected override ObstacleGroupingMaskType MaskType => ObstacleGroupingMaskType.Scatter;

        protected override float[] AffectedTileWeightModifiers => new[]
        {
            0.3f, 0.3f, 0.3f, 
            0.3f,       0.3f, 
            0.3f, 0.3f, 0.3f
        };
    }
    
    public class HorizontalLinesMask : BaseObstacleGroupingMask
    {
        protected override ObstacleGroupingMaskType MaskType => ObstacleGroupingMaskType.HorizontalLines;

        protected override float[] AffectedTileWeightModifiers => new[]
        {
            0.3f, 0.3f, 0.3f, 
            3f,         0.3f, 
            0.3f, 0.3f, 3f
        };
    }
    
    public class VerticalLinesMask : BaseObstacleGroupingMask
    {
        protected override ObstacleGroupingMaskType MaskType => ObstacleGroupingMaskType.VerticalLines;

        protected override float[] AffectedTileWeightModifiers => new[]
        {
            0.3f, 3f, 0.3f, 
            0.3f,     0.3f, 
            3f, 0.3f, 0.3f
        };
    }
    
}