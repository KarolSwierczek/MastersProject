using System;
using System.Collections.Generic;
using Rooms.ObstacleGeneration;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data
{
    [Serializable]
    public class RoomSettings
    {
        public int MinRoomSizeX => _MinRoomSizeX;
        public int MaxRoomSizeX => _MaxRoomSizeX;
        public int MinRoomSizeY => _MinRoomSizeY;
        public int MaxRoomSizeY => _MaxRoomSizeY;
        public bool UseTextureAsMask => _UseTextureAsMask;
        public Texture2D TextureMask => GetTextureMask();
        public ObstacleGenerationMaskType MaskType => GetMaskType();
        public ObstacleGroupingMaskType GroupingMaskType => GetGroupingMaskType();

        [SerializeField, FoldoutGroup("Size")] private int _MinRoomSizeX;
        [SerializeField, FoldoutGroup("Size")] private int _MaxRoomSizeX;
        [SerializeField, FoldoutGroup("Size")] private int _MinRoomSizeY;
        [SerializeField, FoldoutGroup("Size")] private int _MaxRoomSizeY;

        [FoldoutGroup("Obstacles")]
        [SerializeField, BoxGroup("Obstacles/Generation")] private bool _UseTextureAsMask;
        [SerializeField, BoxGroup("Obstacles/Generation")] private bool _UseRandomMask;
        [SerializeField, BoxGroup("Obstacles/Generation"), ShowIf(nameof(RandomTexture))] private List<Texture2D> _ObstacleProbabilityMasks;
        [SerializeField, BoxGroup("Obstacles/Generation"), ShowIf(nameof(ExplicitTexture))] private Texture2D _ObstacleProbabilityMask;
        [SerializeField, BoxGroup("Obstacles/Generation"), ShowIf(nameof(ExplicitPredefined))] private ObstacleGenerationMaskType _ObstacleProbabilityMaskType;
        
        [SerializeField, BoxGroup("Obstacles/Grouping")] private bool _UseRandomGrouping;
        [SerializeField, BoxGroup("Obstacles/Grouping"), HideIf(nameof(_UseRandomGrouping))] private ObstacleGroupingMaskType _ObstacleGroupingMaskType;

        private bool RandomTexture => _UseTextureAsMask && _UseRandomMask;
        private bool ExplicitTexture => _UseTextureAsMask && !_UseRandomMask;
        private bool ExplicitPredefined => !_UseTextureAsMask && !_UseRandomMask;

        private Texture2D GetTextureMask()
        {
            if (!_UseRandomMask)
            {
                return _ObstacleProbabilityMask;
            }

            var randomIndex = Random.Range(0, _ObstacleProbabilityMasks.Count);
            return _ObstacleProbabilityMasks[randomIndex];
        }
        
        private ObstacleGenerationMaskType GetMaskType()
        {
            if (!_UseRandomMask)
            {
                return _ObstacleProbabilityMaskType;
            }

            var values = Enum.GetValues(typeof(ObstacleGenerationMaskType));
            return (ObstacleGenerationMaskType) Random.Range(0, values.Length);
        }
        
        private ObstacleGroupingMaskType GetGroupingMaskType()
        {
            if (!_UseRandomGrouping)
            {
                return _ObstacleGroupingMaskType;
            }

            var values = Enum.GetValues(typeof(ObstacleGroupingMaskType));
            return (ObstacleGroupingMaskType) Random.Range(0, values.Length);
        }
    }
}