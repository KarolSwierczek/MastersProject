using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "RoomGenerationSettings", menuName = "MastersProject/RoomGenerationSettings")]
    public class RoomGenerationSettings : ScriptableObject
    {
        public int MaxTileWeight => _MaxTileWeight;
        public int MinRoomSizeX => _MinRoomSizeX;
        public int MaxRoomSizeX => _MaxRoomSizeX;
        public int MinRoomSizeY => _MinRoomSizeY;
        public int MaxRoomSizeY => _MaxRoomSizeY;
        public int RandomViaPointCount => _RandomViaPointCount;
        public float TileSize => _TileSize;

        //todo: add inspector labels to variables
        [SerializeField] private int _MaxTileWeight;
        [SerializeField] private int _MinRoomSizeX;
        [SerializeField] private int _MaxRoomSizeX;
        [SerializeField] private int _MinRoomSizeY;
        [SerializeField] private int _MaxRoomSizeY;
        [SerializeField] private int _RandomViaPointCount;
        [SerializeField] private float _TileSize;
    }
}
