using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "RoomGenerationSettings", menuName = "MastersProject/RoomGenerationSettings")]
    public class RoomGenerationSettings : ScriptableObject
    {
        public int MaxTileWeight => _MaxTileWeight;
        public int RandomViaPointCount => _RandomViaPointCount;
        public float TileSize => _TileSize;

        //todo: add inspector labels to variables
        [SerializeField] private int _MaxTileWeight;
        [SerializeField] private int _RandomViaPointCount;
        [SerializeField] private float _TileSize;
    }
}
