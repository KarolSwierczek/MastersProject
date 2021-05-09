using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "DifficultySettings", menuName = "MastersProject/DifficultySettings")]
    public class DifficultySettings : ScriptableObject
    {
        //todo: add inspector info
        //how many obstacles should be spawned at a given difficulty rating, as a percentage of current room's area
        public AnimationCurve ColumnSpawnRate => _ColumnSpawnRate;
        public AnimationCurve BoxSpawnRate => _BoxSpawnRate;
        public AnimationCurve SpikeSpawnRate => _SpikeSpawnRate;
        
        //target heart rate (relative to base heart rate) at current normalized level progress
        public AnimationCurve TargetNormalizedHeartRate => _TargetNormalizedHeartRate;

        [SerializeField] private AnimationCurve _ColumnSpawnRate;
        [SerializeField] private AnimationCurve _BoxSpawnRate;
        [SerializeField] private AnimationCurve _SpikeSpawnRate;
        
        [SerializeField] private AnimationCurve _TargetNormalizedHeartRate;
        //todo: enemy spawn rates
    }
}