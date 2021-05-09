using System;
using Data;
using Rooms;
using UnityEngine;

namespace Difficulty
{
    public class DifficultySystem : IDifficultySystem
    {
        private readonly DifficultySettings _settings;
        private float _difficultyRating;

        private const float _difficultyChangeParameter = 1f;

        public DifficultySystem(DifficultySettings settings)
        {
            _settings = settings;
        }

        void IDifficultySystem.UpdateDifficulty(float normalizedHeartRate, float normalizedProgress)
        {
            var targetHeartRate = _settings.TargetNormalizedHeartRate.Evaluate(normalizedProgress);
            var heartRateRatio = targetHeartRate / normalizedHeartRate;
            UpdateDifficulty(heartRateRatio);
        }

        int IDifficultySystem.GetNumberOfTiles(TileType type, int roomSize)
        {
            switch (type)
            {
                case TileType.Spikes:
                    return Mathf.RoundToInt(_settings.SpikeSpawnRate.Evaluate(_difficultyRating) * roomSize);
                case TileType.Column:
                    return Mathf.RoundToInt(_settings.ColumnSpawnRate.Evaluate(_difficultyRating) * roomSize);
                case TileType.Box:
                    return Mathf.RoundToInt(_settings.BoxSpawnRate.Evaluate(_difficultyRating) * roomSize);
                default:
                    throw new ArgumentException($"Tile type {type} is not supported by difficulty system!");
            }
        }

        private void UpdateDifficulty(float heartRateRatio)
        {
            _difficultyRating = Mathf.Clamp01(_difficultyRating + (heartRateRatio - 1) * _difficultyChangeParameter);
            //todo: experiment and change this relation
        }
    }
}