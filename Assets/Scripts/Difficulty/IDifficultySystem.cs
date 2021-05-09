using Rooms;

namespace Difficulty
{
    public interface IDifficultySystem
    {
        void UpdateDifficulty(float normalizedHeartRate, float normalizedProgress);
        int GetNumberOfTiles(TileType type, int roomSize);
        //todo: GetNumberOfEnemies(EnemyType type, int roomSize);
    }
}